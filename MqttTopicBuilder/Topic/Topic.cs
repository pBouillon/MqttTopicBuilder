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

namespace MqttTopicBuilder.Topic
{
    /// <summary>
    /// Represent an MQTT topic
    /// </summary>
    public class Topic
    {
        /// <summary>
        /// Number of levels of this topic
        /// </summary>
        public int Levels
            => Value.Split(Mqtt.Topic.Separator)
                .Length;

        /// <summary>
        /// Value of the MQTT topic (e.g "a/b/c")
        /// </summary>
        public readonly string Value;

        /// <summary>
        /// Create a new MQTT Topic from a raw string
        /// </summary>
        /// <param name="rawTopic">Raw MQTT topic</param>
        /// <remarks>
        /// The raw string will be validated beforehand using <see cref="TopicValidator"/> methods
        /// </remarks>
        public Topic(string rawTopic)
        {
            TopicValidator.ValidateTopic(rawTopic);

            Value = rawTopic;
        }

        /// <summary>
        /// Static method to create a new MQTT Topic from a raw string
        /// </summary>
        /// <param name="rawTopic">Raw MQTT topic</param>
        /// <remarks>
        /// The raw string will be validated beforehand using <see cref="TopicValidator"/> methods
        /// </remarks>
        /// <returns>A new instance of the Topic</returns>
        public static Topic FromString(string rawTopic)
            => new Topic(rawTopic);

        /// <summary>
        /// Returns the topic's value as a string, same as as <see cref="Value"/>
        /// </summary>
        /// <returns>The topic's value</returns>
        public override string ToString()
            => Value;
    }
}
