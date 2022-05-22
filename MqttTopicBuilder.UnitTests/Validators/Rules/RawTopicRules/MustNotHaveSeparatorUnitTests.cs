﻿using FluentAssertions;

using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions.Classes;
using MqttTopicBuilder.UnitTests.Utils;
using MqttTopicBuilder.Validators.Rules.RawTopicRules;

using System;

using Xunit;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.RawTopicRules;

/// <summary>
/// Unit test suite for <see cref="MustNotHaveSeparator"/>
/// </summary>
public class MustNotHaveSeparatorUnitTests
{
    /// <summary>
    /// Ensure that a topic that does not contain a separator will not
    /// break the rule
    /// </summary>
    [Fact]
    public void Validate_OnNoSeparator()
    {
        // Arrange
        var rawTopic = TestUtils.GenerateSingleValidTopic();
        var rule = new MustNotHaveSeparator();

        // Act
        Action validatingRawTopic = () =>
            rule.Validate(rawTopic);

        // Assert
        validatingRawTopic.Should()
            .NotThrow<InvalidTopicException>(
                "because the topic does not contains a separator");
    }

    /// <summary>
    /// Ensure that a topic made of a single separator will break the rule
    /// </summary>
    [Fact]
    public void Validate_OnSeparatorOnly()
    {
        // Arrange
        var rawTopic = Mqtt.Topic.Separator.ToString();
        var rule = new MustNotHaveSeparator();

        // Act
        Action validatingRawTopic = () =>
            rule.Validate(rawTopic);

        // Assert
        validatingRawTopic.Should()
            .Throw<InvalidTopicException>(
                "because a unique separator is as invalid as one within a longer string");
    }

    /// <summary>
    /// Ensure that a separator within a string will break the rule
    /// </summary>
    [Fact]
    public void Validate_OnSeveralSeparators()
    {
        // Arrange
        var rawTopic = TestUtils.GenerateSingleValidTopic() 
                       + Mqtt.Topic.Separator + TestUtils.GenerateSingleValidTopic();

        var rule = new MustNotHaveSeparator();

        // Act
        Action validatingRawTopic = () =>
            rule.Validate(rawTopic);

        // Assert
        validatingRawTopic.Should()
            .Throw<InvalidTopicException>(
                "because a separator within a topic is not allowed");
    }
}