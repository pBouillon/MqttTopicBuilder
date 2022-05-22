using FluentAssertions;

using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.UnitTests.Utils;
using MqttTopicBuilder.Validators.Rules.RawTopicRules;

using Xunit;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.RawTopicRules;

/// <summary>
/// Unit test suite for <see cref="MustEndWithMultiLevelWildcardIfAny"/>
/// </summary>
public class MustEndWithMultiLevelWildcardIfAnyUnitTests
{
    [Fact]
    public void Validate_RawTopicWithSeveralWildcards()
    {
        var topic = TestUtils.GenerateSingleValidTopic() + Mqtt.Topic.Separator
                                                         + Mqtt.Wildcard.MultiLevel + Mqtt.Topic.Separator
                                                         + Mqtt.Wildcard.MultiLevel;

        var rule = new MustEndWithMultiLevelWildcardIfAny();

        var validatingRawTopic = () => rule.Validate(topic);

        validatingRawTopic
            .Should()
            .Throw<IllegalTopicConstructionException>("because at most one multi-level wildcard is allowed");
    }

    [Fact]
    public void Validate_RawTopicWithWildcardAndSlashAsLastChars()
    {
        var topic = TestUtils.GenerateSingleValidTopic() + Mqtt.Topic.Separator
                                                         + Mqtt.Wildcard.MultiLevel + Mqtt.Topic.Separator;

        var rule = new MustEndWithMultiLevelWildcardIfAny();

        var validatingRawTopic = () => rule.Validate(topic);

        validatingRawTopic
            .Should()
            .NotThrow<IllegalTopicConstructionException>("because a final separator should not make the test fail");
    }

    [Fact]
    public void Validate_RawTopicWithWildcardAsLastChar()
    {
        var topic = TestUtils.GenerateSingleValidTopic() + Mqtt.Topic.Separator
                                                         + Mqtt.Wildcard.MultiLevel;

        var rule = new MustEndWithMultiLevelWildcardIfAny();

        var validatingRawTopic = () => rule.Validate(topic);

        validatingRawTopic
            .Should()
            .NotThrow<IllegalTopicConstructionException>(
                "because ending a topic with a multi-level wildcard is a valid behavior");
    }

    [Fact]
    public void Validate_RawTopicWithWildcardInItsMiddle()
    {
        var topic = TestUtils.GenerateSingleValidTopic() + Mqtt.Topic.Separator
                                                         + Mqtt.Wildcard.MultiLevel + Mqtt.Topic.Separator
                                                         + TestUtils.GenerateSingleValidTopic();

        var rule = new MustEndWithMultiLevelWildcardIfAny();

        var validatingRawTopic = () => rule.Validate(topic);

        validatingRawTopic
            .Should()
            .Throw<IllegalTopicConstructionException>(
                "because a multi-level wildcard is allowed only at the end of the topic");
    }
}
