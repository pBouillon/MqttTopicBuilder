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
    /// Exception thrown on an operation on an empty Topic
    /// </summary>
    public class EmptyTopicException : BaseException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public EmptyTopicException()
            : base(ExceptionMessages.EmptyTopic) { }

        /// <summary>
        /// Custom constructor providing context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        public EmptyTopicException(string message)
            : base($"{ExceptionMessages.EmptyTopic}: {message}") { }

        /// <summary>
        /// Custom constructor providing more precise context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        /// <param name="inner">Inner exception thrown</param>
        public EmptyTopicException(string message, Exception inner)
            : base($"{ExceptionMessages.EmptyTopic}: {message}", inner) { }
    }
}
