using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.Validators.Rules.RawTopicRules;
using Shouldly;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.RawTopicRules;

/// <summary>
/// Unit test suite for <see cref="MustEndWithMultiLevelWildcardIfAny"/>
/// </summary>
public class MustEndWithMultiLevelWildcardIfAnyUnitTests
{
    private readonly MustEndWithMultiLevelWildcardIfAny _rule = new();

    [Fact]
    public void Validate_RawTopicWithSeveralWildcards()
    {
        var validatingRawTopic = () => _rule.Validate("sensors/#/#");

        validatingRawTopic.ShouldThrow<IllegalTopicConstructionException>();
    }

    [Fact]
    public void Validate_RawTopicWithWildcardAndSlashAsLastChars()
    {
        var validatingRawTopic = () => _rule.Validate("sensors/#/");

        validatingRawTopic.ShouldNotThrow();
    }

    [Fact]
    public void Validate_RawTopicWithWildcardAsLastChar()
    {
        var validatingRawTopic = () => _rule.Validate("sensors/#");

        validatingRawTopic.ShouldNotThrow();
    }

    [Fact]
    public void Validate_RawTopicWithWildcardInItsMiddle()
    {
        var validatingRawTopic = () => _rule.Validate("sensors/#/temperature");

        validatingRawTopic.ShouldThrow<IllegalTopicConstructionException>();
    }
}
