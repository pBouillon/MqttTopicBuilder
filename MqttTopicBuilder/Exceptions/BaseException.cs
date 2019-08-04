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
    public abstract class BaseException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        protected BaseException() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        protected BaseException(string message)
            : base($"{message}.") { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        protected BaseException(string message, Exception inner)
            : base($"{message}.", inner) { }
    }
}
