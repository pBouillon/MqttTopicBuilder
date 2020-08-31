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
using System.Linq;
using TinyValidator.Abstractions;

namespace MqttTopicBuilder.Validators.Rules.RawTopicRules
{
    /// <summary>
    /// Rule to ensure that if any wildcard is used in a single topic,
    /// then no other value than the wildcard itself should have been provided
    /// </summary>
    public class MustRespectWildcardsExclusivity : BaseRawTopicRule
    {
        /// <inheritdoc cref="Rule{T}.IsValidWhen"/>
        protected override bool IsValidWhen(string value)
        {
            // If there is less than 2 chars, no conflict can ever happen
            if (value.Length < 2)
            {
                return true;
            }

            // Otherwise, a longer topic can not hold any wildcard
            // in addition to another value
            return ! (value.Contains(Mqtt.Wildcard.MultiLevel)
                   || value.Contains(Mqtt.Wildcard.SingleLevel));
        }

        /// <inheritdoc cref="Rule{T}.OnInvalid"/>
        protected override void OnInvalid()
            => throw new InvalidTopicException(
                $"A topic value should not hold any wildcard " + 
                $"(\"{Mqtt.Wildcard.MultiLevel}\", \"{Mqtt.Wildcard.SingleLevel}\")");
    }
}
