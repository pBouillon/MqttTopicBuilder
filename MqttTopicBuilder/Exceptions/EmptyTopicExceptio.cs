namespace MqttTopicBuilder.Exceptions
{
    using System;

    public class EmptyTopicException : BaseException
    {
        public EmptyTopicException()
            : base(ExceptionMessages.EmptyTopicException) { }

        public EmptyTopicException(string message)
            : base($"{ExceptionMessages.EmptyTopicException}: {message}") { }

        public EmptyTopicException(string message, Exception inner)
            : base($"{ExceptionMessages.EmptyTopicException}: {message}", inner) { }
    }
}
