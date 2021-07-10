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

namespace MqttTopicBuilder.Exceptions.Classes
{
    /// <summary>
    /// Exception thrown on an operation on an invalid topic
    /// </summary>
    public class InvalidTopicException : MqttBaseException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public InvalidTopicException()
            : base("Invalid topic name") { }

        /// <summary>
        /// Custom constructor providing context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        public InvalidTopicException(string message)
            : base($"Invalid topic name: {message}") { }

        /// <summary>
        /// Custom constructor providing more precise context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        /// <param name="inner">Inner exception thrown</param>
        public InvalidTopicException(string message, System.Exception inner)
            : base($"Invalid topic name: {message}", inner) { }
    }
}
