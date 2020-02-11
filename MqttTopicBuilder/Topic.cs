/**
 * Author
 *      Pierre Bouillon - https://github.com/pBouillon
 *
 * Repository
 *      MqttTopicBuilder - https://github.com/pBouillon/MqttTopicBuilder
 *
 * License
 *      MIT - https://github.com/pBouillon/MqttTopicBuilder/blob/master/LICENSE
 */

namespace MqttTopicBuilder
{
    using MqttUtils;
    
    /// <summary>
    /// Stores data and logic belonging to MQTT topics
    /// </summary>
    public class Topic
    {
        /// <summary>
        /// Check if the topic is empty
        /// </summary>
        public bool IsPathEmpty
            => Path.Length == 0;

        /// <summary>
        /// Get the path level (0 if empty)
        /// </summary>
        public int Level
            => IsPathEmpty
                ? 0
                : Path.Split(Topics.Separator).Length;

        /// <summary>
        /// Get the MQTT topic path
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="rawMqttTopic">The MQTT topic's path</param>
        public Topic(string rawMqttTopic)
        {
            Path = rawMqttTopic;
        }

        /// <summary>
        /// Remove all illegal characters in a topic
        /// </summary>
        /// <param name="toNormalize">Topic to format</param>
        /// <returns>The sanitized topic</returns>
        public static string Normalize(string toNormalize)
        {
            return toNormalize.Replace(" ", string.Empty)
                .Replace(Topics.Separator.ToString(), string.Empty)
                .Replace(Wildcards.MultiLevel.ToString(), string.Empty)
                .Replace(Wildcards.SingleLevel.ToString(), string.Empty);
        }

        /// <summary>
        /// Returns the topic's path
        /// </summary>
        /// <returns>The topic's path</returns>
        public override string ToString()
        {
            return Path;
        }
    }
}
