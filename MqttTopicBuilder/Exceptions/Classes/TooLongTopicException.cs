using MqttTopicBuilder.Constants;

namespace MqttTopicBuilder.Exceptions.Classes
{
    /// <summary>
    /// Exception thrown on a topic slice longer than the allowed size
    /// </summary>
    /// <see cref="Mqtt.Topic.MaxSubTopicLength"/>
    public class TooLongTopicException : MqttBaseException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TooLongTopicException()
            : base(ExceptionMessages.EmptyTopic) { }

        /// <summary>
        /// Custom constructor providing context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        public TooLongTopicException(string message)
            : base($"{ExceptionMessages.TooLongTopic}: {message}") { }

        /// <summary>
        /// Custom constructor providing more precise context specificity
        /// </summary>
        /// <param name="message">Message to provide along with the exception</param>
        /// <param name="inner">Inner exception thrown</param>
        public TooLongTopicException(string message, System.Exception inner)
            : base($"{ExceptionMessages.TooLongTopic}: {message}", inner) { }
    }
}
