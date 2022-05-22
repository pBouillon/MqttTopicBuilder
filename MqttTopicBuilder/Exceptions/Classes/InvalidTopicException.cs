﻿namespace MqttTopicBuilder.Exceptions.Classes
{
    /// <summary>
    /// Exception thrown on an operation on an invalid topic
    /// </summary>
    public class InvalidTopicException : MqttBaseException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public InvalidTopicException()
            : base(ExceptionMessages.InvalidTopic) { }

        /// <summary>
        /// Custom constructor providing context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        public InvalidTopicException(string message)
            : base($"{ExceptionMessages.InvalidTopic}: {message}") { }

        /// <summary>
        /// Custom constructor providing more precise context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        /// <param name="inner">Inner exception thrown</param>
        public InvalidTopicException(string message, System.Exception inner)
            : base($"{ExceptionMessages.InvalidTopic}: {message}", inner) { }
    }
}
