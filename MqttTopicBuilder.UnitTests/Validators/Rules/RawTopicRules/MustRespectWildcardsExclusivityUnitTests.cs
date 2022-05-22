using AutoFixture;

using FluentAssertions;

using MqttTopicBuilder.Constants;
using MqttTopicBuilder.UnitTests.Utils;
using MqttTopicBuilder.Validators.Rules.RawTopicRules;

using System;
using MqttTopicBuilder.Exceptions;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.RawTopicRules;

/// <summary>
/// Unit test suite for <see cref="MustRespectWildcardsExclusivity"/>
/// </summary>
public class MustRespectWildcardsExclusivityUnitTests
{
    /// <summary>
    /// Private instance of <see cref="IFixture"/> for test data generation purposes
    /// </summary>
    private static readonly IFixture Fixture = new Fixture();

    public void Validate_OnEmptyTopic()
    {
        // Arrange
        var rawTopic = string.Empty;
        var rule = new MustRespectWildcardsExclusivity();

        // Act
        Action validatingRawTopic = () =>
            rule.Validate(rawTopic);

        // Assert
        validatingRawTopic.Should()
            .NotThrow<InvalidTopicException>(
                "because an empty conflict does not result in a conflict between wildcards");
    }
        
    public void Validate_OnMixedWildcards()
    {
        // Arrange
        var rawTopic = Mqtt.Wildcard.MultiLevel.ToString() + Mqtt.Wildcard.SingleLevel;
        var rule = new MustRespectWildcardsExclusivity();

        // Act
        Action validatingRawTopic = () =>
            rule.Validate(rawTopic);

        // Assert
        validatingRawTopic.Should()
            .Throw<InvalidTopicException>(
                "because mixing wildcards is not allowed");
    }

    public void Validate_OnMultiLevelWildcardOnly()
    {
        // Arrange
        var rawTopic = TestUtils.GenerateValidTopic(Fixture.Create<int>())
                       + Mqtt.Topic.Separator + Mqtt.Wildcard.MultiLevel;
        var rule = new MustRespectWildcardsExclusivity();

        // Act
        Action validatingRawTopic = () =>
            rule.Validate(rawTopic);

        // Assert
        validatingRawTopic.Should()
            .NotThrow<InvalidTopicException>(
                "because the usage of this wildcard is exclusive");
    }

    public void Validate_OnSingleLevelWildcardOnly()
    {
        // Arrange
        var rawTopic = TestUtils.GenerateValidTopic(Fixture.Create<int>())
                       + Mqtt.Topic.Separator + Mqtt.Wildcard.SingleLevel;
        var rule = new MustRespectWildcardsExclusivity();

        // Act
        Action validatingRawTopic = () =>
            rule.Validate(rawTopic);

        // Assert
        validatingRawTopic.Should()
            .NotThrow<InvalidTopicException>(
                "because the usage of this wildcard is exclusive");
    }
}