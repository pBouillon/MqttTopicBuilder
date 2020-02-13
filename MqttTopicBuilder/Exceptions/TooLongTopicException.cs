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
    using MqttUtils;
    using System;

    /// <summary>
    /// Exception thrown on a topic slice longer than the allowed size
    /// </summary>
    /// <see cref="Topics.MaxSliceLength"/>
    public class TooLongTopicException : BaseException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TooLongTopicException()
            : base(ExceptionMessages.EmptyTopic) { }

        /// <summary>
        /// Custom constructor providing context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        public TooLongTopicException(string message)
            : base($"{ExceptionMessages.TooLongTopic}: {message}") { }

        /// <summary>
        /// Custom constructor providing more precise context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        /// <param name="inner">Inner exception thrown</param>
        public TooLongTopicException(string message, Exception inner)
            : base($"{ExceptionMessages.TooLongTopic}: {message}", inner) { }
    }
}
