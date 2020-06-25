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

using System;

namespace MqttTopicBuilder.Exceptions.Classes
{
    /// <summary>
    /// Custom exception base with default constructors
    /// </summary>
    public abstract class MqttBaseException : Exception
    {
        /// <summary>
        /// Custom constructor providing context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        protected MqttBaseException(string message)
            : base(message) { }

        /// <summary>
        /// Custom constructor providing more precise context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        /// <param name="inner">Inner exception thrown</param>
        protected MqttBaseException(string message, System.Exception inner)
            : base(message, inner) { }
    }
}
