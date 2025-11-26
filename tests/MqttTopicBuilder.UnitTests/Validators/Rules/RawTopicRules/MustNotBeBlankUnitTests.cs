using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.Validators.Rules.RawTopicRules;
using Shouldly;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.RawTopicRules;

/// <summary>
/// Unit test suite for <see cref="MustNotBeBlank"/>
/// </summary>
public class MustNotBeBlankUnitTests
{
    private readonly MustNotBeBlank _rule = new();

    /// <summary>
    /// Ensure that a topic containing only whitespaces will break the rule
    /// </summary>
    [Fact]
    public void Validate_OnBlankString()
    {
        var validatingRawTopic = () => _rule.Validate("   ");

        validatingRawTopic.ShouldThrow<EmptyTopicException>();
    }

    /// <summary>
    /// Ensure that an empty topic will break the rule
    /// </summary>
    [Fact]
    public void Validate_OnEmptyString()
    {
        var validatingRawTopic = () => _rule.Validate(string.Empty);

        validatingRawTopic.ShouldThrow<EmptyTopicException>();
    }

    /// <summary>
    /// Ensure that a well formed topic will not break the rule
    /// </summary>
    [Fact]
    public void Validate_OnNonEmptyString()
    {
        var validatingRawTopic = () => _rule.Validate("sensors");

        validatingRawTopic.ShouldNotThrow();
    }

    /// <summary>
    /// Ensure that a null value will break the rule
    /// </summary>
    [Fact]
    public void Validate_OnNullString()
    {
        var validatingRawTopic = () => _rule.Validate(null);

        validatingRawTopic.ShouldThrow<EmptyTopicException>();
    }
}
