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

namespace MqttTopicBuilder.Validators.Rules
{
    /// <summary>
    /// Rule to ensure that if any wildcard is used in a single topic,
    /// then no other value than the wildcard itself should have been provided
    /// </summary>
    public class MustRespectWildcardsExclusivity : RawTopicRule
    {
        /// <inheritdoc cref="Rule{T}.IsValid"/>
        protected override bool IsValid(string value)
        => ! (value.Length > 1 
              && (value.Contains(Mqtt.Wildcard.MultiLevel)
                  || value.Contains(Mqtt.Wildcard.SingleLevel)));

        /// <inheritdoc cref="Rule{T}.OnError"/>
        protected override void OnError()
            => throw new InvalidTopicException(
                $"A topic value should not hold any wildcard " + 
                $"(\"{Mqtt.Wildcard.MultiLevel}\", \"{Mqtt.Wildcard.SingleLevel}\")");
    }
}
