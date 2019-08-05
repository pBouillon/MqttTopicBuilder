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
    /// Exception thwon on the attempt to add an illegal topic operation
    /// </summary>
    public class IllegalTopicConstructionException : BaseException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public IllegalTopicConstructionException()
            : base(ExceptionMessages.IllegalTopicConstructionException) { }

        /// <summary>
        /// Custom constructor providing context specificities
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        public IllegalTopicConstructionException(string message)
            : base($"{ExceptionMessages.IllegalTopicConstructionException}: {message}") { }

        /// <summary>
        /// Custom constructor providing more precise context specificities
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        /// <param name="inner">Inner exception thrown</param>
        public IllegalTopicConstructionException(string message, Exception inner)
            : base($"{ExceptionMessages.IllegalTopicConstructionException}: {message}", inner) { }
    }
}
