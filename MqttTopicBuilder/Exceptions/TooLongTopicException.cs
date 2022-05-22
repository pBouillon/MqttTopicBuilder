using MqttTopicBuilder.Constants;

namespace MqttTopicBuilder.Exceptions;

/// <summary>
/// Exception thrown on a topic slice longer than the allowed size
/// </summary>
/// <see cref="Mqtt.Topic.MaxSubTopicLength"/>
public class TooLongTopicException : MqttBaseException
{
    /// <summary>
    /// The exception's default message
    /// </summary>
    private const string DefaultMessage = "MQTT clients can only handle up to 10 240 characters per level";

    /// <summary>
    /// Default constructor
    /// </summary>
    public TooLongTopicException()
        : base(DefaultMessage) { }

    /// <summary>
    /// Custom constructor providing context specificity
    /// </summary>
    /// <param name="message">Message to provide along with the exception</param>
    public TooLongTopicException(string message)
        : base($"{DefaultMessage}: {message}") { }

    /// <summary>
    /// Custom constructor providing more precise context specificity
    /// </summary>
    /// <param name="message">Message to provide along with the exception</param>
    /// <param name="inner">Inner exception thrown</param>
    public TooLongTopicException(string message, System.Exception inner)
        : base($"{DefaultMessage}: {message}", inner) { }
}