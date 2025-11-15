using System;

namespace MqttTopicBuilder.Exceptions;

/// <summary>
/// Custom exception base with default constructors
/// </summary>
public abstract class MqttBaseException : Exception
{
    /// <summary>
    /// Custom constructor providing context specificity
    /// </summary>
    /// <param name="message">Message to provide along with the exception</param>
    protected MqttBaseException(string message)
        : base(message) { }

    /// <summary>
    /// Custom constructor providing more precise context specificity
    /// </summary>
    /// <param name="message">Message to provide along with the exception</param>
    /// <param name="inner">Inner exception thrown</param>
    protected MqttBaseException(string message, Exception inner)
        : base(message, inner) { }
}
