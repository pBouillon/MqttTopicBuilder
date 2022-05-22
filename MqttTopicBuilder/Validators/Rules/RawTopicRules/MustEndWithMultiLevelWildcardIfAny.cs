using System.Linq;

using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;

namespace MqttTopicBuilder.Validators.Rules.RawTopicRules;

/// <summary>
/// Rule to ensure that a topic using a <see cref="Mqtt.Wildcard.MultiLevel"/>
/// does not have any value after it
/// </summary>
public class MustEndWithMultiLevelWildcardIfAny : BaseRawTopicRule
{
    /// <inheritdoc cref="Rule{T}.IsValid"/>
    protected override bool IsValid(string value)
    {
        // Remove trailing "/" if any
        if (value.Last() == Mqtt.Topic.Separator)
        {
            value = value.Remove(value.Length - 1);
        }

        var multiLevelWildcardsCount = value.Count(@char => @char == Mqtt.Wildcard.MultiLevel);

        return multiLevelWildcardsCount switch
        {
            0 => true,
            // If there is a wildcard, it should be the last character
            1 => value.Last() == Mqtt.Wildcard.MultiLevel,
            _ => false,
        };
    }

    /// <inheritdoc cref="Rule{T}.OnError"/>
    protected override void OnError()
        => throw new IllegalTopicConstructionException("Impossible to add a topic after a multilevel wildcard");
}
