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
    /// TODO
    /// </summary>
    public class IllegalTopicConstructionException : BaseException
    {
        /// <summary>
        /// TODO
        /// </summary>
        public IllegalTopicConstructionException()
            : base(ExceptionMessages.IllegalTopicConstructionException) { }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="message"></param>
        public IllegalTopicConstructionException(string message)
            : base($"{ExceptionMessages.IllegalTopicConstructionException}: {message}") { }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public IllegalTopicConstructionException(string message, Exception inner)
            : base($"{ExceptionMessages.IllegalTopicConstructionException}: {message}", inner) { }
    }
}
