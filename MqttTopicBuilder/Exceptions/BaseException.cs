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

    public class BaseException : Exception
    {
        public BaseException() { }

        public BaseException(string message)
            : base(message) { }

        public BaseException(string message, Exception inner)
            : base(message, inner) { }
    }
}
