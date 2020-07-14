/*
 * Author
 *      Pierre Bouillon - https://github.com/pBouillon
 *
 * Repository
 *      MqttTopicBuilder - https://github.com/pBouillon/MqttTopicBuilder
 *
 * License
 *      MIT - https://github.com/pBouillon/MqttTopicBuilder/blob/master/LICENSE
 */

using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions.Classes;

namespace MqttTopicBuilder.Validators.Rules.RawTopicRules
{
    /// <summary>
    /// Rule to ensure that a topic does not contains any wildcard
    /// </summary>
    public class MustNotContainWildcard : BaseRawTopicRule
    {
        /// <inheritdoc cref="Rule{T}.IsValid"/>
        protected override bool IsValid(string value)
            => ! (value.Contains(Mqtt.Wildcard.MultiLevel.ToString())
               || value.Contains(Mqtt.Wildcard.SingleLevel.ToString()));

        /// <inheritdoc cref="Rule{T}.OnError"/>
        protected override void OnError()
            => throw new IllegalTopicConstructionException(
                "This topic should not contain any wildcard");
    }
}
