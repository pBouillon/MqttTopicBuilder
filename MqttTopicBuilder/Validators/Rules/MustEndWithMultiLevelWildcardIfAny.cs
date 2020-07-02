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

namespace MqttTopicBuilder.Validators.Rules
{
    /// <summary>
    /// Rule to ensure that a topic using a <see cref="Mqtt.Wildcard.MultiLevel"/>
    /// does not have any value after it
    /// </summary>
    public class MustEndWithMultiLevelWildcardIfAny : RawTopicRule
    {
        /// <inheritdoc cref="Rule{T}.IsValid"/>
        protected override bool IsValid(string value)
        {
            // Remove trailing "/" if any
            if (value.Last() == Mqtt.Topic.Separator)
            {
                value = value.Remove(value.Length - 1);
            }

            var multiLevelWildcardsCount = value.Count(_ =>
                _ == Mqtt.Wildcard.MultiLevel);

            // If the topic is using the wildcard last char is not the wildcard
            // Then the multi level wildcard usage is violated
            return ! (multiLevelWildcardsCount == 1
                && value.Last() != Mqtt.Wildcard.MultiLevel);
        }

        /// <inheritdoc cref="Rule{T}.OnError"/>
        protected override void OnError()
            => throw new IllegalTopicConstructionException(
                ExceptionMessages.TopicAfterWildcard);
    }
}
