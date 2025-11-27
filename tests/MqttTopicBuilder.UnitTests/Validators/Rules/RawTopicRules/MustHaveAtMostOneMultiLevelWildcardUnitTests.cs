using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.Validators.Rules.RawTopicRules;
using Shouldly;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.RawTopicRules;

/// <summary>
/// Unit test suite for <see cref="MustHaveAtMostOneMultiLevelWildcard"/>
/// </summary>
public class MustHaveAtMostOneMultiLevelWildcardUnitTests
{
    private readonly MustHaveAtMostOneMultiLevelWildcard _rule = new();

    /// <summary>
    /// Ensure that a topic containing two or more wildcard will break the rule
    /// </summary>
    [Fact]
    public void Validate_OnMoreThanOneWildcard()
    {
        var validatingRawTopic = () => _rule.Validate("##");

        validatingRawTopic.ShouldThrow<IllegalTopicConstructionException>();
    }

    /// <summary>
    /// Ensure that a topic that does not contain any wildcard does not break the rule
    /// </summary>
    [Fact]
    public void Validate_OnNoWildcards()
    {
        var validatingRawTopic = () => _rule.Validate("sensors");

        validatingRawTopic.ShouldNotThrow();
    }

    /// <summary>
    /// Ensure that a topic containing exactly one wildcard will not break the rule
    /// </summary>
    [Fact]
    public void Validate_OnOneWildcard()
    {
        var validatingRawTopic = () => _rule.Validate("#");

        validatingRawTopic.ShouldNotThrow();
    }
}
