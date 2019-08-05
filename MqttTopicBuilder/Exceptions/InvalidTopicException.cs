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
            : base(ExceptionMessages.InvalidTopicException) { }

        /// <summary>
        /// Custom constructor providing context specificities
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        public InvalidTopicException(string message)
            : base($"{ExceptionMessages.InvalidTopicException}: {message}") { }

        /// <summary>
        /// Custom constructor providing more precise context specificities
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        /// <param name="inner">Inner exception thrown</param>
        public InvalidTopicException(string message, Exception inner)
            : base($"{ExceptionMessages.InvalidTopicException}: {message}", inner) { }
    }
}
