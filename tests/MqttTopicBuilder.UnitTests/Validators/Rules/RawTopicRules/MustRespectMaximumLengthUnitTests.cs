using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.Validators.Rules.RawTopicRules;
using Shouldly;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.RawTopicRules;

/// <summary>
/// Unit test suite for <see cref="MustRespectMaximumLength"/>
/// </summary>
public class MustRespectMaximumLengthUnitTests
{
    private readonly MustRespectMaximumLength _rule = new();

    /// <summary>
    /// Ensure that a topic which length is longer than the maximum
    /// allowed limit will break the rule
    /// </summary>
    [Fact]
    public void Validate_OnTopicLongerThanMaximumLimit()
    {
        var rawTopic = new string('x', Mqtt.Topic.MaxSubTopicLength + 1);

        var validatingRawTopic = () => _rule.Validate(rawTopic);

        validatingRawTopic.ShouldThrow<TooLongTopicException>();
    }

    /// <summary>
    /// Ensure that a topic which length is shorter than the maximum
    /// allowed limit will not break the rule
    /// </summary>
    [Fact]
    public void Validate_OnTopicShorterThanMaximumLimit()
    {
        var rawTopic = new string('x', Mqtt.Topic.MaxSubTopicLength - 1);

        var validatingRawTopic = () => _rule.Validate(rawTopic);

        validatingRawTopic.ShouldNotThrow();
    }

    /// <summary>
    /// Ensure that a topic which length is exactly of the same length as the maximum
    /// allowed limit will not break the rule
    /// </summary>
    [Fact]
    public void Validate_OnTopicWithExactSameLengthAsMaximumLimit()
    {
        var rawTopic = new string('x', Mqtt.Topic.MaxSubTopicLength);

        var validatingRawTopic = () => _rule.Validate(rawTopic);

        validatingRawTopic.ShouldNotThrow();
    }
}
