using AutoFixture;

using FluentAssertions;

using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.UnitTests.Utils;
using MqttTopicBuilder.Validators.Rules.RawTopicRules;

using Xunit;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.RawTopicRules;

/// <summary>
/// Unit test suite for <see cref="MustNotBeBlank"/>
/// </summary>
public class MustNotBeBlankUnitTests
{
    /// <summary>
    /// Private instance of <see cref="IFixture"/> for test data generation purposes
    /// </summary>
    private static readonly IFixture Fixture = new Fixture();

    /// <summary>
    /// Ensure that a topic containing only whitespaces will break the rule
    /// </summary>
    [Fact]
    public void Validate_OnBlankString()
    {
        var whitespaceCount = Fixture.Create<int>() + 1;
        var rawTopic = new string(' ', whitespaceCount);

        var rule = new MustNotBeBlank();

        var validatingRawTopic = () => rule.Validate(rawTopic);

        validatingRawTopic
            .Should()
            .Throw<EmptyTopicException>("because a blank topic should break the rule");
    }

    /// <summary>
    /// Ensure that an empty topic will break the rule
    /// </summary>
    [Fact]
    public void Validate_OnEmptyString()
    {
        var rawTopic = string.Empty;

        var rule = new MustNotBeBlank();

        var validatingRawTopic = () => rule.Validate(rawTopic);

        validatingRawTopic
            .Should()
            .Throw<EmptyTopicException>("because an empty topic should break the rule");
    }

    /// <summary>
    /// Ensure that a well formed topic will not break the rule
    /// </summary>
    [Fact]
    public void Validate_OnNonEmptyString()
    {
        var rawTopic = TestUtils.GenerateSingleValidTopic();
        var rule = new MustNotBeBlank();

        var validatingRawTopic = () => rule.Validate(rawTopic);

        validatingRawTopic
            .Should()
            .NotThrow<EmptyTopicException>("because a topic with a valid value should not break the rule");
    }

    /// <summary>
    /// Ensure that a null value will break the rule
    /// </summary>
    [Fact]
    public void Validate_OnNullString()
    {
        string rawTopic = null;

        var rule = new MustNotBeBlank();

        var validatingRawTopic = () => rule.Validate(rawTopic);

        validatingRawTopic
            .Should()
            .Throw<EmptyTopicException>("because an null topic should break the rule");
    }
}
