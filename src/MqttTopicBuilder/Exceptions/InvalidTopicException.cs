namespace MqttTopicBuilder.Exceptions;

/// <summary>
/// Exception thrown on an operation on an invalid topic
/// </summary>
public class InvalidTopicException : MqttBaseException
{
    /// <summary>
    /// The exception's default message
    /// </summary>
    private const string DefaultMessage = "Invalid topic name";

    /// <summary>
    /// Default constructor
    /// </summary>
    public InvalidTopicException()
        : base(DefaultMessage) { }

    /// <summary>
    /// Custom constructor providing context specificity
    /// </summary>
    /// <param name="message">Message to provide along with the exception</param>
    public InvalidTopicException(string message)
        : base($"{DefaultMessage}: {message}") { }

    /// <summary>
    /// Custom constructor providing more precise context specificity
    /// </summary>
    /// <param name="message">Message to provide along with the exception</param>
    /// <param name="inner">Inner exception thrown</param>
    public InvalidTopicException(string message, System.Exception inner)
        : base($"{DefaultMessage}: {message}", inner) { }
}
