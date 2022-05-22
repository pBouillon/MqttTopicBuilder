using MqttTopicBuilder.Exceptions;

namespace MqttTopicBuilder.Validators.Rules.RawTopicRules;

/// <summary>
/// Rule to ensure that a topic is neither null, empty or blank
/// </summary>
public class MustNotBeBlank : BaseRawTopicRule
{
    /// <inheritdoc cref="Rule{T}.IsValid"/>
    protected override bool IsValid(string value)
        => ! string.IsNullOrWhiteSpace(value);

    /// <inheritdoc cref="Rule{T}.OnError"/>
    protected override void OnError()
        => throw new EmptyTopicException();
}
