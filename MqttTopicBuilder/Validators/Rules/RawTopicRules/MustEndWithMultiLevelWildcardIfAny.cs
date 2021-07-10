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

using System.Linq;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.Exceptions.Classes;

namespace MqttTopicBuilder.Validators.Rules.RawTopicRules
{
    /// <summary>
    /// Rule to ensure that a topic using a <see cref="Mqtt.Wildcard.MultiLevel"/>
    /// does not have any value after it
    /// </summary>
    public class MustEndWithMultiLevelWildcardIfAny : BaseRawTopicRule
    {
        /// <inheritdoc cref="Rule{T}.IsValid"/>
        protected override bool IsValid(string value)
        {
            // Remove trailing "/" if any
            if (value.Last() == Mqtt.Topic.Separator)
            {
                value = value.Remove(value.Length - 1);
            }

            var multiLevelWildcardsCount = value.Count(_ => _ == Mqtt.Wildcard.MultiLevel);

            // If there is more than one multi-level wildcard, then its usage
            // is violated and not valid
            if (multiLevelWildcardsCount >= 2)
            {
                return false;
            }

            // If there is a wildcard, it should be the last character
            if (multiLevelWildcardsCount == 1)
            {
                return value.Last() == Mqtt.Wildcard.MultiLevel;
            }

            // Last case is no multi-level wildcard, which is allowed
            return true;
        }

        /// <inheritdoc cref="Rule{T}.OnError"/>
        protected override void OnError()
            => throw new IllegalTopicConstructionException(
                "Impossible to add a topic after a multilevel wildcard");
    }
}
