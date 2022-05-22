using System.Linq;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;

namespace MqttTopicBuilder.Validators.Rules.RawTopicRules;

/// <summary>
/// Rule to ensure that a topic does not contains any wildcard
/// </summary>
public class MustNotContainWildcard : BaseRawTopicRule
{
    /// <inheritdoc cref="Rule{T}.IsValid"/>
    protected override bool IsValid( string value )
    {
        var wildcards = new[]
        {
            Mqtt.Wildcard.MultiLevel,
            Mqtt.Wildcard.SingleLevel,
        };

        var hasAnyWildcard = value.Any(wildcards.Contains);
        return !hasAnyWildcard;
    }

    /// <inheritdoc cref="Rule{T}.OnError"/>
    protected override void OnError()
        => throw new IllegalTopicConstructionException("This topic should not contain any wildcard");
}
