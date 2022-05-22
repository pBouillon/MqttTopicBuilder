namespace MqttTopicBuilder.Exceptions;

/// <summary>
/// Exception thrown on an operation on an empty Topic
/// </summary>
public class EmptyTopicException : MqttBaseException
{
    /// <summary>
    /// The exception's default message
    /// </summary>
    private const string DefaultMessage = "A topic must have at least one level";

    /// <summary>
    /// Default constructor
    /// </summary>
    public EmptyTopicException()
        : base(DefaultMessage) { }

    /// <summary>
    /// Custom constructor providing context specificity
    /// </summary>
    /// <param name="message">Message to provide along with the exception</param>
    public EmptyTopicException(string message)
        : base($"{DefaultMessage}: {message}") { }

    /// <summary>
    /// Custom constructor providing more precise context specificity
    /// </summary>
    /// <param name="message">Message to provide along with the exception</param>
    /// <param name="inner">Inner exception thrown</param>
    public EmptyTopicException(string message, System.Exception inner)
        : base($"{DefaultMessage}: {message}", inner) { }
}