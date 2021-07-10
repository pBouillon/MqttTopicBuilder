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
    /// Exception thrown on an operation on an empty Topic
    /// </summary>
    public class EmptyTopicException : MqttBaseException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public EmptyTopicException()
            : base("Null topic level is not allowed") { }

        /// <summary>
        /// Custom constructor providing context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        public EmptyTopicException(string message)
            : base($"Null topic level is not allowed: {message}") { }

        /// <summary>
        /// Custom constructor providing more precise context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        /// <param name="inner">Inner exception thrown</param>
        public EmptyTopicException(string message, System.Exception inner)
            : base($"Null topic level is not allowed: {message}", inner) { }
    }
}
