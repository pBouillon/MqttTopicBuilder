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
    /// Exception thrown when adding a topic to a filled TopicBuilder
    /// </summary>
    public class TooManyTopicsAppendingException : MqttBaseException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TooManyTopicsAppendingException()
            : base(ExceptionMessages.TooManyTopicsAppending) { }

        /// <summary>
        /// Custom constructor providing context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        public TooManyTopicsAppendingException(string message)
            : base($"{ExceptionMessages.TooManyTopicsAppending}: {message}") { }

        /// <summary>
        /// Custom constructor providing more precise context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        /// <param name="inner">Inner exception thrown</param>
        public TooManyTopicsAppendingException(string message, System.Exception inner)
            : base($"{ExceptionMessages.TooManyTopicsAppending}: {message}", inner) { }
    }
}
