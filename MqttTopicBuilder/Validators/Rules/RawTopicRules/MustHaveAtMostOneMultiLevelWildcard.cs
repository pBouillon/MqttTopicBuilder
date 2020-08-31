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
using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.Exceptions.Classes;
using System.Linq;
using TinyValidator.Abstractions;

namespace MqttTopicBuilder.Validators.Rules.RawTopicRules
{
    /// <summary>
    /// Rule to ensure that a topic does not contains more than
    /// one <see cref="Mqtt.Wildcard.MultiLevel"/>
    /// </summary>
    public class MustHaveAtMostOneMultiLevelWildcard : BaseRawTopicRule
    {
        /// <inheritdoc cref="Rule{T}.IsValidWhen"/>
        protected override bool IsValidWhen(string value)
            => value.Count(_ => 
                _ == Mqtt.Wildcard.MultiLevel) <= 1;

        /// <inheritdoc cref="Rule{T}.OnInvalid"/>
        protected override void OnInvalid()
            => throw new IllegalTopicConstructionException(
                ExceptionMessages.IllegalMultiLevelWildcardUsage);
    }
}
