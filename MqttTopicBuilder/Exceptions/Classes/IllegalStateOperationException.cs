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
    /// Exception thrown on an operation on a builder that is not allowed in its current
    /// state
    /// </summary>
    public class IllegalStateOperationException : MqttBaseException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public IllegalStateOperationException()
            : base(ExceptionMessages.IllegalStateOperation) { }

        /// <summary>
        /// Custom constructor providing context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        public IllegalStateOperationException(string message)
            : base($"{ExceptionMessages.IllegalStateOperation}: {message}") { }

        /// <summary>
        /// Custom constructor providing more precise context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        /// <param name="inner">Inner exception thrown</param>
        public IllegalStateOperationException(string message, System.Exception inner)
            : base($"{ExceptionMessages.IllegalStateOperation}: {message}", inner) { }
    }
}