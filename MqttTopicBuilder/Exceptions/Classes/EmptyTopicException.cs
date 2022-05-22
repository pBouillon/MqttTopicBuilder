namespace MqttTopicBuilder.Exceptions.Classes
{
    /// <summary>
    /// Exception thrown on an operation on an empty Topic
    /// </summary>
    public class EmptyTopicException : MqttBaseException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public EmptyTopicException()
            : base(ExceptionMessages.EmptyTopic) { }

        /// <summary>
        /// Custom constructor providing context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        public EmptyTopicException(string message)
            : base($"{ExceptionMessages.EmptyTopic}: {message}") { }

        /// <summary>
        /// Custom constructor providing more precise context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        /// <param name="inner">Inner exception thrown</param>
        public EmptyTopicException(string message, System.Exception inner)
            : base($"{ExceptionMessages.EmptyTopic}: {message}", inner) { }
    }
}
