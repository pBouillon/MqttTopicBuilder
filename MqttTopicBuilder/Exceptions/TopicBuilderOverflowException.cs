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
    /// Exception thrown when adding a topic to a filled TopicBuilder
    /// </summary>
    public class TopicBuilderOverflowException : BaseException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TopicBuilderOverflowException()
            : base(ExceptionMessages.InvalidTopic) { }

        /// <summary>
        /// Custom constructor providing context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        public TopicBuilderOverflowException(string message)
            : base($"{ExceptionMessages.TopicBuilderOverflow}: {message}") { }

        /// <summary>
        /// Custom constructor providing more precise context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        /// <param name="inner">Inner exception thrown</param>
        public TopicBuilderOverflowException(string message, Exception inner)
            : base($"{ExceptionMessages.TopicBuilderOverflow}: {message}", inner) { }
    }
}
