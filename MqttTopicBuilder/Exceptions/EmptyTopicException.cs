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
    public class EmptyTopicException : BaseException
    {
        /// <summary>
        /// TODO
        /// </summary>
        public EmptyTopicException()
            : base(ExceptionMessages.EmptyTopicException) { }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="message"></param>
        public EmptyTopicException(string message)
            : base($"{ExceptionMessages.EmptyTopicException}: {message}") { }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public EmptyTopicException(string message, Exception inner)
            : base($"{ExceptionMessages.EmptyTopicException}: {message}", inner) { }
    }
}
