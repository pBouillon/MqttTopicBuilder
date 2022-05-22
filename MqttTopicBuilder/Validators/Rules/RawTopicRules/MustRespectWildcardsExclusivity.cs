using MqttTopicBuilder.Constants;
using System.Linq;
using MqttTopicBuilder.Exceptions;

namespace MqttTopicBuilder.Validators.Rules.RawTopicRules;

/// <summary>
/// Rule to ensure that if any wildcard is used in a single topic,
/// then no other value than the wildcard itself should have been provided
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

        // Otherwise, a longer topic can not hold any wildcard
        // in addition to another value
        return ! (value.Contains(Mqtt.Wildcard.MultiLevel)
                  || value.Contains(Mqtt.Wildcard.SingleLevel));
    }

    /// <inheritdoc cref="Rule{T}.OnError"/>
    protected override void OnError()
        => throw new InvalidTopicException(
            $"A topic value should not hold any wildcard " + 
            $"(\"{Mqtt.Wildcard.MultiLevel}\", \"{Mqtt.Wildcard.SingleLevel}\")");
}
