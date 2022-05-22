namespace MqttTopicBuilder.Exceptions;

/// <summary>
/// Exception thrown when adding a topic to a filled TopicBuilder
/// </summary>
public class TooManyTopicsAppendingException : MqttBaseException
{
    /// <summary>
    /// The exception's default message
    /// </summary>
    private const string DefaultMessage = "The current TopicBuilder instance if full and can not add any other element";

    /// <summary>
    /// Default constructor
    /// </summary>
    public TooManyTopicsAppendingException()
        : base(DefaultMessage) { }

    /// <summary>
    /// Custom constructor providing context specificity
    /// </summary>
    /// <param name="message">Message to provide along with the exception</param>
    public TooManyTopicsAppendingException(string message)
        : base($"{DefaultMessage}: {message}") { }

    /// <summary>
    /// Custom constructor providing more precise context specificity
    /// </summary>
    /// <param name="message">Message to provide along with the exception</param>
    /// <param name="inner">Inner exception thrown</param>
    public TooManyTopicsAppendingException(string message, System.Exception inner)
        : base($"{DefaultMessage}: {message}", inner) { }
}
