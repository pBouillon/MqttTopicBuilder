using System.Linq;

using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;

namespace MqttTopicBuilder.Validators.Rules.RawTopicRules;

/// <summary>
/// Rule to ensure that a topic does not contains the forbidden null char
/// <see cref="Mqtt.Topic.NullCharacter"/>
/// </summary>
public class MustNotHaveNullChar : BaseRawTopicRule
{
    /// <inheritdoc cref="Rule{T}.IsValid"/>
    protected override bool IsValid(string value)
        => ! value.Contains(Mqtt.Topic.NullCharacter);

    /// <inheritdoc cref="Rule{T}.OnError"/>
    protected override void OnError()
        => throw new IllegalTopicConstructionException("The null character is not allowed in a topic");
}
