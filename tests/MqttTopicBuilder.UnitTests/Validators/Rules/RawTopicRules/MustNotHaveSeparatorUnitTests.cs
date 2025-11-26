using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.Validators.Rules.RawTopicRules;
using Shouldly;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.RawTopicRules;

/// <summary>
/// Unit test suite for <see cref="MustNotHaveSeparator"/>
/// </summary>
public class MustNotHaveSeparatorUnitTests
{
    private readonly MustNotHaveSeparator _rule = new();

    /// <summary>
    /// Ensure that a topic that does not contain a separator will not
    /// break the rule
    /// </summary>
    [Fact]
    public void Validate_OnNoSeparator()
    {
        var validatingRawTopic = () => _rule.Validate("sensors");

        validatingRawTopic.ShouldNotThrow();
    }

    /// <summary>
    /// Ensure that a topic made of a single separator will break the rule
    /// </summary>
    [Fact]
    public void Validate_OnSeparatorOnly()
    {
        var validatingRawTopic = () => _rule.Validate("/");

        validatingRawTopic.ShouldThrow<InvalidTopicException>();
    }

    /// <summary>
    /// Ensure that a separator within a string will break the rule
    /// </summary>
    [Fact]
    public void Validate_OnSeveralSeparators()
    {
        var validatingRawTopic = () => _rule.Validate("sens/ors");

        validatingRawTopic.ShouldThrow<InvalidTopicException>();
    }
}
