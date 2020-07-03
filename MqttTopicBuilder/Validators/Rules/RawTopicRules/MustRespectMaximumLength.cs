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
    /// Rule to ensure that a single raw topic does not exceed the maximum
    /// limit allowed of <see cref="Mqtt.Topic.MaxSubTopicLength"/> chars
    /// </summary>
    public class MustRespectMaximumLength : BaseRawTopicRule
    {
        /// <inheritdoc cref="Rule{T}.IsValid"/>
        protected override bool IsValid(string value)
            => value.Length <= Mqtt.Topic.MaxSubTopicLength;

        /// <inheritdoc cref="Rule{T}.OnError"/>
        protected override void OnError()
            => throw new TooLongTopicException(
                $"Topics must not exceed {Mqtt.Topic.MaxSubTopicLength} characters");
    }
}
