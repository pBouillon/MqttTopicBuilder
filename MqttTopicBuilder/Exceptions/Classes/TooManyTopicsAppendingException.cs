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
            : base("The current TopicBuilder instance if full and can not add any other element") { }

        /// <summary>
        /// Custom constructor providing context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        public TooManyTopicsAppendingException(string message)
            : base($"The current TopicBuilder instance if full and can not add any other element: {message}") { }

        /// <summary>
        /// Custom constructor providing more precise context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        /// <param name="inner">Inner exception thrown</param>
        public TooManyTopicsAppendingException(string message, System.Exception inner)
            : base($"The current TopicBuilder instance if full and can not add any other element: {message}", inner) { }
    }
}
