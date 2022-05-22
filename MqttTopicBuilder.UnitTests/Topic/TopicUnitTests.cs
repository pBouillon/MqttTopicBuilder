using FluentAssertions;

using MqttTopicBuilder.Constants;
using MqttTopicBuilder.UnitTests.Utils;

using System;
using MqttTopicBuilder.Exceptions;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Topic;

/// <summary>
/// Unit test suite for <see cref="Topic"/>
/// </summary>
public class TopicUnitTests
{
    /// <summary>
    /// Ensure that the implicit conversion of a string to a topic is valid
    /// </summary>
    [Fact]
    public void Conversion_ExplicitFromTopic()
    {
        // Arrange
        var rawTopic = TestUtils.GenerateValidTopic();
        var topic = new MqttTopicBuilder.Builder.Topic(rawTopic);

        // Act
        var rawTopicValue = (string) topic;

        // Assert
        rawTopicValue.Should()
            .Be(rawTopic,
                "because a topic should have been created using the initial raw value");
    }

    /// <summary>
    /// Ensure that the implicit conversion of a string to a topic is valid
    /// </summary>
    [Fact]
    public void Conversion_ImplicitFromString()
    {
        // Arrange
        var rawTopic = TestUtils.GenerateValidTopic();

        // Act
        var topic = (MqttTopicBuilder.Builder.Topic) rawTopic;

        // Assert
        topic.Value
            .Should()
            .Be(rawTopic,
                "because a topic should have been created using the initial raw value");
    }

    /// <summary>
    /// Ensure that the minimal topic is safely built from an empty string
    /// </summary>
    [Fact]
    public void FromString_FromEmptyTopic()
    {
        // Arrange
        var emptyTopic = string.Empty;

        // Act
        var topicBuilt = new MqttTopicBuilder.Builder.Topic(emptyTopic);

        // Assert
        topicBuilt.Value
            .Should()
            .Be(Mqtt.Topic.Separator.ToString(),
                "because an empty string will result in the smallest valid topic");

        topicBuilt.Levels
            .Should()
            .Be(1, "because there is only one level, the smallest one");
    }

    /// <summary>
    /// Ensure that an invalid string is not successfully converted to a topic
    /// </summary>
    [Fact]
    public void FromString_FromInvalidRawTopic()
    {
        // Arrange
        var invalidRawTopic = $"{Mqtt.Topic.Separator}{Mqtt.Topic.Separator}";

        // Act
        Action creatingInvalidTopic = () =>
            _ = MqttTopicBuilder.Builder.Topic.FromString(invalidRawTopic);

        // Assert
        creatingInvalidTopic.Should()
            .Throw<EmptyTopicException>("because two consecutive separators will result in an empty topic");
    }

    /// <summary>
    /// Ensure that the topic is safely built from the smallest valid value
    /// </summary>
    [Fact]
    public void FromString_FromMinimalTopic()
    {
        // Arrange
        var minimalRawTopic = Mqtt.Topic.Separator.ToString();

        // Act
        var minimalTopic = MqttTopicBuilder.Builder.Topic.FromString(minimalRawTopic);

        // Assert
        minimalTopic.Value
            .Should()
            .Be(Mqtt.Topic.Separator.ToString());
    }

    /// <summary>
    /// Ensure that the topic is safely built from a valid raw string
    /// </summary>
    [Fact]
    public void FromString_FromValidRawTopic()
    {
        // Arrange
        var validRawTopic = TestUtils.GenerateValidTopic();

        // Act
        var validTopic = MqttTopicBuilder.Builder.Topic.FromString(validRawTopic);

        // Assert
        validTopic.Value.Should()
            .Be(validRawTopic, "because a valid value will not be altered");

        validTopic.Levels.Should()
            .Be(
                validRawTopic.Split(Mqtt.Topic.Separator).Length,
                "because the same number of level is conserved");
    }

    /// <summary>
    /// Ensure that the topic is safely built from a valid raw string
    /// </summary>
    [Fact]
    public void FromString_FromValidRawTopicWithTrailingSeparator()
    {
        // Arrange
        var validRawTopic = TestUtils.GenerateValidTopic();
        var validRawTopicWithTrailingSeparator = validRawTopic + Mqtt.Topic.Separator;

        // Act
        var validTopic = MqttTopicBuilder.Builder.Topic.FromString(validRawTopicWithTrailingSeparator);

        // Assert
        validTopic.Value.Should()
            .Be(validRawTopic, "because the separator has been trimmed");

        validTopic.Levels.Should()
            .Be(
                validRawTopicWithTrailingSeparator.Split(Mqtt.Topic.Separator).Length - 1,
                "because the trailing separator has been removed");
    }

    /// <summary>
    /// Ensure that both topics are treated as value object for equality checks
    /// </summary>
    [Fact]
    public void Topic_EqualityCheckBasedOnValue()
    {
        // Arrange
        var rawTopic = TestUtils.GenerateValidTopic();
        var firstTopic = new MqttTopicBuilder.Builder.Topic(rawTopic);
        var secondTopic = new MqttTopicBuilder.Builder.Topic(rawTopic);

        // Act
        var result = firstTopic == secondTopic;

        // Assert
        result.Should()
            .BeTrue("because both topics are holding the same values");
    }
        
    /// <summary>
    /// Ensure that the minimal topic is safely built from an empty string
    /// </summary>
    [Fact]
    public void Topic_FromEmptyTopic()
    {
        // Arrange
        var emptyTopic = string.Empty;

        // Act
        var topicBuilt = new MqttTopicBuilder.Builder.Topic(emptyTopic);

        // Assert
        topicBuilt.Value
            .Should()
            .Be(Mqtt.Topic.Separator.ToString(),
                "because an empty string will result in the smallest valid topic");

        topicBuilt.Levels
            .Should()
            .Be(1, "because there is only one level, the smallest one");
    }

    /// <summary>
    /// Ensure that an invalid string is not successfully converted to a topic
    /// </summary>
    [Fact]
    public void Topic_FromInvalidRawTopic()
    {
        // Arrange
        var invalidRawTopic = $"{Mqtt.Topic.Separator}{Mqtt.Topic.Separator}";

        // Act
        Action creatingInvalidTopic = () =>
            _ = new MqttTopicBuilder.Builder.Topic(invalidRawTopic);

        // Assert
        creatingInvalidTopic.Should()
            .Throw<EmptyTopicException>("because two consecutive separators will result in an empty topic");
    }

    /// <summary>
    /// Ensure that the topic is safely built from the smallest valid value
    /// </summary>
    [Fact]
    public void Topic_FromMinimalTopic()
    {
        // Arrange
        var minimalRawTopic = Mqtt.Topic.Separator.ToString();

        // Act
        var minimalTopic = new MqttTopicBuilder.Builder.Topic(minimalRawTopic);

        // Assert
        minimalTopic.Value
            .Should()
            .Be(Mqtt.Topic.Separator.ToString());
    }

    /// <summary>
    /// Ensure that the topic is safely built from a valid raw string
    /// </summary>
    [Fact]
    public void Topic_FromValidRawTopic()
    {
        // Arrange
        var validRawTopic = TestUtils.GenerateValidTopic();

        // Act
        var validTopic = new MqttTopicBuilder.Builder.Topic(validRawTopic);

        // Assert
        validTopic.Value.Should()
            .Be(validRawTopic, "because a valid value will not be altered");

        validTopic.Levels.Should()
            .Be(
                validRawTopic.Split(Mqtt.Topic.Separator).Length,
                "because the same number of level is conserved");
    }

    /// <summary>
    /// Ensure that the topic is safely built from a valid raw string
    /// </summary>
    [Fact]
    public void Topic_FromValidRawTopicWithTrailingSeparator()
    {
        // Arrange
        var validRawTopic = TestUtils.GenerateValidTopic();
        var validRawTopicWithTrailingSeparator = validRawTopic + Mqtt.Topic.Separator;

        // Act
        var validTopic = new MqttTopicBuilder.Builder.Topic(validRawTopicWithTrailingSeparator);

        // Assert
        validTopic.Value.Should()
            .Be(validRawTopic, "because the separator has been trimmed");

        validTopic.Levels.Should()
            .Be(
                validRawTopicWithTrailingSeparator.Split(Mqtt.Topic.Separator).Length - 1,
                "because the trailing separator has been removed");
    }
}