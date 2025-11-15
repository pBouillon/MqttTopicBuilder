using FluentAssertions;

using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.UnitTests.Utils;
using MqttTopicBuilder.Validators.Rules.RawTopicRules;

using Xunit;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.RawTopicRules;

/// <summary>
/// Unit test suite for <see cref="MustNotHaveNullChar"/>
/// </summary>
public class MustNotHaveNullCharUnitTests
{
    /// <summary>
    /// Ensure that a topic that does not use any null char will pass the rule
    /// </summary>
    [Fact]
    public void Validate_OnNoNullChar()
    {
        var rawTopic = TestUtils.GenerateSingleValidTopic();
        var rule = new MustNotHaveNullChar();

        var validatingRawTopic = () => rule.Validate(rawTopic);

        validatingRawTopic
            .Should()
            .NotThrow<IllegalTopicConstructionException>("because the raw topic is correctly formed");
    }

    /// <summary>
    /// Ensure that a topic made of a null char will break the rule
    /// </summary>
    [Fact]
    public void Validate_OnNullChar()
    {
        var rawTopic = Mqtt.Topic.NullCharacter.ToString();

        var validatingRawTopic = () => new MustNotHaveNullChar().Validate(rawTopic);

        validatingRawTopic
            .Should()
            .Throw<IllegalTopicConstructionException>("because a topic using the null char is not valid");
    }

    /// <summary>
    /// Ensure that a topic containing a null char will break the rule
    /// </summary>
    [Fact]
    public void Validate_OnNullCharAmongTopic()
    {
        var rawTopic = TestUtils.GenerateSingleValidTopic()
                       + Mqtt.Topic.NullCharacter
                       + TestUtils.GenerateSingleValidTopic();

        var validatingRawTopic = () => new MustNotHaveNullChar().Validate(rawTopic);

        validatingRawTopic
            .Should()
            .Throw<IllegalTopicConstructionException>("because a topic using the null char is not valid");
    }
}
