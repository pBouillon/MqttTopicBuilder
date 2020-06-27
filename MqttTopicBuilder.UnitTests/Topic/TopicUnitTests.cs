/*
 * Author
 *      Pierre Bouillon - https://github.com/pBouillon
 *
 * Repository
 *      MqttTopicBuilder - https://github.com/pBouillon/MqttTopicBuilder
 *
 * License
 *      MIT - https://github.com/pBouillon/MqttTopicBuilder/blob/master/LICENSE
 */

using System;
using FluentAssertions;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions.Classes;
using MqttTopicBuilder.UnitTests.Utils;
using Xunit;
using MqttTopic = MqttTopicBuilder.Topic;

namespace MqttTopicBuilder.UnitTests.Topic
{
    /// <summary>
    /// Unit test suite for <see cref="MqttTopic.Topic"/>
    /// </summary>
    public class TopicUnitTests
    {
        /// <summary>
        /// Ensure that the minimal topic is safely built from an empty string
        /// </summary>
        [Fact]
        public void FromString_FromEmptyTopic()
        {
            // Arrange
            var emptyTopic = string.Empty;

            // Act
            var topicBuilt = new MqttTopic.Topic(emptyTopic);

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
                _ = MqttTopic.Topic.FromString(invalidRawTopic);

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
            var minimalTopic = MqttTopic.Topic.FromString(minimalRawTopic);

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
            var validTopic = MqttTopic.Topic.FromString(validRawTopic);

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
            var validTopic = MqttTopic.Topic.FromString(validRawTopicWithTrailingSeparator);

            // Assert
            validTopic.Value.Should()
                .Be(validRawTopic, "because the separator has been trimmed");

            validTopic.Levels.Should()
                .Be(
                    validRawTopicWithTrailingSeparator.Split(Mqtt.Topic.Separator).Length - 1,
                    "because the trailing separator has been removed");
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
            var topicBuilt = new MqttTopic.Topic(emptyTopic);

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
                _ = new MqttTopic.Topic(invalidRawTopic);

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
            var minimalTopic = new MqttTopic.Topic(minimalRawTopic);

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
            var validTopic = new MqttTopic.Topic(validRawTopic);

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
            var validTopic = new MqttTopic.Topic(validRawTopicWithTrailingSeparator);

            // Assert
            validTopic.Value.Should()
                .Be(validRawTopic, "because the separator has been trimmed");

            validTopic.Levels.Should()
                .Be(
                    validRawTopicWithTrailingSeparator.Split(Mqtt.Topic.Separator).Length - 1,
                    "because the trailing separator has been removed");
        }
    }
}
