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

using AutoFixture;
using FluentAssertions;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.Exceptions.Classes;
using MqttTopicBuilder.Topic;
using MqttTopicBuilder.UnitTests.Utils;
using System;
using System.Linq;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Topic
{
    /// <summary>
    /// Unit test suite for <see cref="TopicValidator"/>
    /// </summary>
    public class TopicValidatorUnitTests
    {
        [Fact]
        public void ValidateTopic_OnBlankTopic()
        {
            // Arrange
            var blankTopic = string.Empty;

            // Act
            Action validatingBlankTopic = () =>
                TopicValidator.ValidateTopic(blankTopic);

            // Assert
            validatingBlankTopic.Should()
                .Throw<EmptyTopicException>("because a topic can not be empty");
        }

        /// <summary>
        /// Ensure that no exceptions are thrown when validating a minimal topic
        /// </summary>
        [Fact]
        public void ValidateTopic_OnMinimalTopic()
        {
            // Arrange
            var minimalValidTopic = Mqtt.Topic.Separator.ToString();

            // Act
            Action validatingTopic = () =>
                TopicValidator.ValidateTopic(minimalValidTopic);

            // Assert
            validatingTopic.Should()
                .NotThrow<MqttBaseException>("because the topic is minimal yet allowed");
        }

        /// <summary>
        /// Ensure that the multi-level wildcard can not be used at the beginning or at the end
        /// of a topic
        /// </summary>
        [Fact]
        public void ValidateTopic_OnMultiLevelWildcardsBeforeTopicEnd()
        {
            // Arrange
            var topicWithMultiLevelWildcardBeforeEnd = $"{Mqtt.Topic.Separator}{Mqtt.Wildcard.MultiLevel}";
            topicWithMultiLevelWildcardBeforeEnd += $"{Mqtt.Topic.Separator}{TestUtils.GenerateValidTopic()}";

            // Act
            Action validatingTopicWithMultiLevelWildcardsBeforeEnd = () =>
                TopicValidator.ValidateTopic(topicWithMultiLevelWildcardBeforeEnd);

            // Assert
            validatingTopicWithMultiLevelWildcardsBeforeEnd.Should()
                .Throw<IllegalTopicConstructionException>()
                .Where(_ =>
                        _.Message.Contains(ExceptionMessages.TopicAfterWildcard),
                    "because a multi-level wildcard may only be used anywhere but at the end of a topic");
        }

        /// <summary>
        /// Ensure that the multi-level wildcard can't be used more than once
        /// </summary>
        [Fact]
        public void ValidateTopic_OnMultiLevelWildcardsTopic()
        {
            // Arrange
            var multiLevelWildcardsTopic = TestUtils.GenerateValidTopic(
                Mqtt.Topic.MaximumAllowedLevels - 2);

            // Appending '/#/#'
            multiLevelWildcardsTopic += string.Concat(
                Enumerable.Repeat(
                    $"{Mqtt.Topic.Separator}{Mqtt.Wildcard.MultiLevel}", 2));

            // Act
            Action validatingMultiLevelWildcardTopic = () =>
                TopicValidator.ValidateTopic(multiLevelWildcardsTopic);

            // Assert
            validatingMultiLevelWildcardTopic.Should()
                .Throw<IllegalTopicConstructionException>()
                .Where(_ => 
                    _.Message.Contains(ExceptionMessages.IllegalMultiLevelWildcardUsage),
                    "because a multi-level wildcard may be used only once");
        }

        /// <summary>
        /// Ensure that no exceptions are thrown when validating a valid topic
        /// </summary>
        [Fact]
        public void ValidateTopic_OnValidTopic()
        {
            // Arrange
            var validRawTopic = TestUtils.GenerateValidTopic();

            // Act
            Action validatingMinimalTopic = () =>
                TopicValidator.ValidateTopic(validRawTopic);

            // Assert
            validatingMinimalTopic.Should()
                .NotThrow<MqttBaseException>("because the topic is correctly built");
        }
    }
}
