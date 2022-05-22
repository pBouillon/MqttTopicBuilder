using AutoFixture;

using FluentAssertions;

using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.Validators.Rules.RawTopicRules;

using Xunit;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.RawTopicRules;

/// <summary>
/// Unit test suite for <see cref="MustRespectMaximumLength"/>
/// </summary>
public class MustRespectMaximumLengthUnitTests
{
    /// <summary>
    /// Private instance of <see cref="IFixture"/> for test data generation purposes
    /// </summary>
    private static readonly IFixture Fixture = new Fixture();

    /// <summary>
    /// Ensure that a topic which length is longer than the maximum
    /// allowed limit will break the rule
    /// </summary>
    [Fact]
    public void Validate_OnTopicLongerThanMaximumLimit()
    {
        var length = Fixture.Create<int>() + Mqtt.Topic.MaxSubTopicLength;
        var rawTopic = new string(Fixture.Create<char>(), length);
        var rule = new MustRespectMaximumLength();

        var validatingRawTopic = () => rule.Validate(rawTopic);

        validatingRawTopic
            .Should()
            .Throw<TooLongTopicException>("because the topic is exceeding the maximum allowed size");
    }

    /// <summary>
    /// Ensure that a topic which length is shorter than the maximum
    /// allowed limit will not break the rule
    /// </summary>
    [Fact]
    public void Validate_OnTopicShorterThanMaximumLimit()
    {
        const int length = Mqtt.Topic.MaxSubTopicLength / 2;
        var rawTopic = new string(Fixture.Create<char>(), length);
        var rule = new MustRespectMaximumLength();

        var validatingRawTopic = () => rule.Validate(rawTopic);

        validatingRawTopic
            .Should()
            .NotThrow<TooLongTopicException>("because the topic is under the defined bound");
    }

    /// <summary>
    /// Ensure that a topic which length is exactly of the same length as the maximum
    /// allowed limit will not break the rule
    /// </summary>
    [Fact]
    public void Validate_OnTopicWithExactSameLengthAsMaximumLimit()
    {
        const int length = Mqtt.Topic.MaxSubTopicLength;
        var rawTopic = new string(Fixture.Create<char>(), length);
        var rule = new MustRespectMaximumLength();

        var validatingRawTopic = () => rule.Validate(rawTopic);

        validatingRawTopic
            .Should()
            .NotThrow<TooLongTopicException>("because the topic as reached the maximum limit but does not exceed it");
    }
}
