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

namespace MqttTopicBuilder.Exceptions.Classes
{
    /// <summary>
    /// Exception thrown on a topic slice longer than the allowed size
    /// </summary>
    /// <see cref="Mqtt.Topic.MaxSubTopicLength"/>
    public class TooLongTopicException : MqttBaseException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TooLongTopicException()
            : base("Null topic level is not allowed") { }

        /// <summary>
        /// Custom constructor providing context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        public TooLongTopicException(string message)
            : base($"Null topic level is not allowed: {message}") { }

        /// <summary>
        /// Custom constructor providing more precise context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        /// <param name="inner">Inner exception thrown</param>
        public TooLongTopicException(string message, System.Exception inner)
            : base($"Null topic level is not allowed: {message}", inner) { }
    }
}
