using System.Linq;

using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;

namespace MqttTopicBuilder.Validators.Rules.RawTopicRules;

/// <summary>
/// Rule to ensure that if any wildcard is used in a single topic, then no value other than the wildcard itself
/// should have been provided
/// </summary>
public class MustRespectWildcardsExclusivity : BaseRawTopicRule
{
    /// <inheritdoc cref="Rule{T}.IsValid"/>
    protected override bool IsValid(string value)
    {
        // If there is less than 2 chars, no conflict can ever happen
        if (value.Length < 2)
        {
            return true;
        }

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
        => throw new InvalidTopicException(
            $"A topic value should not hold any wildcard (\"{Mqtt.Wildcard.MultiLevel}\", \"{Mqtt.Wildcard.SingleLevel}\")");
}
