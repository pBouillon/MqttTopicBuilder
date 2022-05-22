using MqttTopicBuilder.Constants;
using System.Linq;
using MqttTopicBuilder.Exceptions;

namespace MqttTopicBuilder.Validators.Rules.RawTopicRules;

/// <summary>
/// Rule to ensure that a single raw topic does not contains any separator
/// <see cref="Mqtt.Topic.Separator"/>
/// </summary>
public class MustNotHaveSeparator : BaseRawTopicRule
{
    /// <inheritdoc cref="Rule{T}.IsValid"/>
    protected override bool IsValid(string value)
        => ! value.Contains(Mqtt.Topic.Separator);

    /// <inheritdoc cref="Rule{T}.OnError"/>
    protected override void OnError()
        => throw new InvalidTopicException(
            $"A topic should not contains the MQTT separator \"{Mqtt.Topic.Separator}\"");
}