using System.Linq;

using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;

namespace MqttTopicBuilder.Validators.Rules.RawTopicRules;

/// <summary>
/// Rule to ensure that a topic does not contains more than one <see cref="Mqtt.Wildcard.MultiLevel"/>
/// </summary>
public class MustHaveAtMostOneMultiLevelWildcard : BaseRawTopicRule
{
    /// <inheritdoc cref="Rule{T}.IsValid"/>
    protected override bool IsValid(string value)
        => value.Count(@char => @char == Mqtt.Wildcard.MultiLevel) <= 1;

    /// <inheritdoc cref="Rule{T}.IsValid"/>
    protected override void OnError()
        => throw new IllegalTopicConstructionException("A topic can contain at most one multi-level wildcard");
}
