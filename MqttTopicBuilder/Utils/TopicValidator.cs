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

using MqttTopicBuilder.Core.Constants;
using MqttTopicBuilder.Exceptions.Classes;

namespace MqttTopicBuilder.Utils
{
    /// <summary>
    /// Validates MQTT topic rules
    /// </summary>
    internal static class TopicValidator
    {
        /// <summary>
        /// Validate a topic to be appended to a longer one
        /// </summary>
        /// <param name="topic">Topic value to be checked</param>
        /// <exception cref="EmptyTopicException">If the topic is blank or empty</exception>
        /// <exception cref="InvalidTopicException">If the topic is malformed</exception>
        public static void ValidateTopic(this string topic)
        {
            // A topic can't be blank
            if (string.IsNullOrEmpty(topic)
                || string.IsNullOrWhiteSpace(topic)
                || topic.Contains(Topics.NullCharacter))
            {
                throw new EmptyTopicException();
            }

            // Manually adding separators is forbidden
            if (topic.Contains(Topics.Separator))
            {
                throw new InvalidTopicException(
                    $"A topic should not contains the MQTT separator \"{Topics.Separator}\"");
            }

            // Wildcard must only be used to denote a level and shouldn't be used to denote multiple
            // characters
            if (topic.Length > 1
                && (topic.Contains(Wildcards.MultiLevel)
                    || topic.Contains(Wildcards.SingleLevel)))
            {
                throw new InvalidTopicException(
                    $"A topic value should not hold any wildcard " +
                    $"(\"{Wildcards.MultiLevel}\", \"{Wildcards.SingleLevel}\")");
            }
        }
    }
}
