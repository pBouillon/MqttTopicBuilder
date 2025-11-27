using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.Validators.Rules.RawTopicRules;
using Shouldly;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.RawTopicRules;

/// <summary>
/// Unit test suite for <see cref="MustNotHaveNullChar"/>
/// </summary>
public class MustNotHaveNullCharUnitTests
{
    private readonly MustNotHaveNullChar _rule = new();

    /// <summary>
    /// Ensure that a topic that does not use any null char will pass the rule
    /// </summary>
    [Fact]
    public void Validate_OnNoNullChar()
    {
        var validatingRawTopic = () => _rule.Validate("sensors");

        validatingRawTopic.ShouldNotThrow();
    }

    /// <summary>
    /// Ensure that a topic made of a null char will break the rule
    /// </summary>
    [Fact]
    public void Validate_OnNullChar()
    {
        var rawTopic = Mqtt.Topic.NullCharacter.ToString();

        var validatingRawTopic = () => new MustNotHaveNullChar().Validate(rawTopic);

        validatingRawTopic.ShouldThrow<IllegalTopicConstructionException>();
    }

    /// <summary>
    /// Ensure that a topic containing a null char will break the rule
    /// </summary>
    [Fact]
    public void Validate_OnNullCharAmongTopic()
    {
        var rawTopic = $"sensors/{Mqtt.Topic.NullCharacter}/temperature";

        var validatingRawTopic = () => new MustNotHaveNullChar().Validate(rawTopic);

        validatingRawTopic.ShouldThrow<IllegalTopicConstructionException>();
    }
}
