namespace MqttTopicBuilder.Exceptions;

/// <summary>
/// Exception thrown on the attempt to add an illegal topic operation
/// </summary>
public class IllegalTopicConstructionException : MqttBaseException
{
    /// <summary>
    /// The exception's default message
    /// </summary>
    private const string DefaultMessage = "Impossible to add a topic at this place";

    /// <summary>
    /// Default constructor
    /// </summary>
    public IllegalTopicConstructionException()
        : base(DefaultMessage) { }

    /// <summary>
    /// Custom constructor providing context specificity
    /// </summary>
    /// <param name="message">Message to provide along with the exception</param>
    public IllegalTopicConstructionException(string message)
        : base($"{DefaultMessage}: {message}") { }

    /// <summary>
    /// Custom constructor providing more precise context specificity
    /// </summary>
    /// <param name="message">Message to provide along with the exception</param>
    /// <param name="inner">Inner exception thrown</param>
    public IllegalTopicConstructionException(string message, System.Exception inner)
        : base($"{DefaultMessage}: {message}", inner) { }
}