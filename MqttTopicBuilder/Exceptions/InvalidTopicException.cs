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

namespace MqttTopicBuilder.Exceptions
{
    using System;
    
    /// <summary>
    /// Exception thrown on an operation on an invalid topic
    /// </summary>
    public class InvalidTopicException : BaseException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public InvalidTopicException()
            : base(ExceptionMessages.InvalidTopic) { }

        /// <summary>
        /// Custom constructor providing context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        public InvalidTopicException(string message)
            : base($"{ExceptionMessages.InvalidTopic}: {message}") { }

        /// <summary>
        /// Custom constructor providing more precise context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        /// <param name="inner">Inner exception thrown</param>
        public InvalidTopicException(string message, Exception inner)
            : base($"{ExceptionMessages.InvalidTopic}: {message}", inner) { }
    }
}
