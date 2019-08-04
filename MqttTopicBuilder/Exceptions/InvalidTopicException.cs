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
    /// 
    /// </summary>
    public class InvalidTopicException : BaseException
    {
        /// <summary>
        /// TODO
        /// </summary>
        public InvalidTopicException()
            : base(ExceptionMessages.InvalidTopicException) { }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="message"></param>
        public InvalidTopicException(string message)
            : base($"{ExceptionMessages.InvalidTopicException}: {message}") { }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public InvalidTopicException(string message, Exception inner)
            : base($"{ExceptionMessages.InvalidTopicException}: {message}", inner) { }
    }
}
