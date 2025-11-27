namespace MqttTopicBuilder.Exceptions;

/// <summary>
/// Exception thrown on an operation on a builder that is not allowed in its current state
/// </summary>
public class IllegalStateOperationException : MqttBaseException
{
    /// <summary>
    /// The exception's default message
    /// </summary>
    private const string DefaultMessage = "Impossible to perform this operation in the current state";

    /// <summary>
    /// Default constructor
    /// </summary>
    public IllegalStateOperationException()
        : base(DefaultMessage) { }

    /// <summary>
    /// Custom constructor providing context specificity
    /// </summary>
    /// <param name="message">Message to provide along with the exception</param>
    public IllegalStateOperationException(string message)
        : base($"{DefaultMessage}: {message}") { }

    /// <summary>
    /// Custom constructor providing more precise context specificity
    /// </summary>
    /// <param name="message">Message to provide along with the exception</param>
    /// <param name="inner">Inner exception thrown</param>
    public IllegalStateOperationException(string message, System.Exception inner)
        : base($"{DefaultMessage}: {message}", inner) { }
}
