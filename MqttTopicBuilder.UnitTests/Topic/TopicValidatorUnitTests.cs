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
        /// <summary>
        /// Private instance of <see cref="IFixture"/> for test data generation purposes
        /// </summary>
        private static readonly IFixture Fixture = new Fixture();

        /// <summary>
        /// Ensure that a blank topic is not allowed
        /// </summary>
        [Fact]
        public void ValidateForTopicAppending_OnBlankTopic()
        {
            // Arrange
            var blankTopic = string.Empty;

            // Act
            Action validatingBlankTopic = () =>
                blankTopic.ValidateForTopicAppending();

            // Assert
            validatingBlankTopic.Should()
                .Throw<EmptyTopicException>("because a topic can not be empty");
        }

        /// <summary>
        /// Ensure that appending a multi-level wildcard is allowed
        /// </summary>
        [Fact]
        public void ValidateForTopicAppending_OnMultiLevelWildcard()
        {
            // Arrange
            var multiLevelWildcard = Mqtt.Wildcard.MultiLevel.ToString();

            // Act
            Action validatingTopic = () =>
                multiLevelWildcard.ValidateForTopicAppending();

            // Assert
            validatingTopic.Should()
                .NotThrow<MqttBaseException>("because a single wildcard is allowed to be appended");
        }

        /// <summary>
        /// Ensure that a single separator is not a valid topic to be appended
        /// </summary>
        [Fact]
        public void ValidateForTopicAppending_OnSeparator()
        {
            // Arrange
            var separatorTopic = Mqtt.Topic.Separator.ToString();

            // Act
            Action validatingSeparator = () =>
                separatorTopic.ValidateForTopicAppending();

            // Assert
            validatingSeparator.Should()
                .Throw<InvalidTopicException>("because appending the separator will result in an empty level");
        }

        /// <summary>
        /// Ensure that no more than one wildcard can be used at the time
        /// </summary>
        [Fact]
        public void ValidateForTopicAppending_OnSeveralWildcards()
        {
            // Arrange
            var mixedWildcards = $"{Mqtt.Wildcard.SingleLevel}{Mqtt.Wildcard.SingleLevel}";

            // Act
            Action validatingTopic = () =>
                mixedWildcards.ValidateForTopicAppending();

            // Assert
            validatingTopic.Should()
                .Throw<InvalidTopicException>("because only one wildcard may be used on a single level");
        }

        /// <summary>
        /// Ensure that a topic can not exceed the size limit
        /// </summary>
        [Fact]
        public void ValidateForTopicAppending_OnTooLongTopic()
        {
            // Arrange
            var tooLongTopic = new string(Fixture.Create<char>(), Mqtt.Topic.MaxSubTopicLength + 1);

            // Act
            Action validatingTooLongTopic = () =>
                tooLongTopic.ValidateForTopicAppending();

            // Assert
            validatingTooLongTopic.Should()
                .Throw<TooLongTopicException>("because a topic must not exceed the size limit");
        }

        /// <summary>
        /// Ensure that appending a single-level wildcard is allowed
        /// </summary>
        [Fact]
        public void ValidateForTopicAppending_OnSingleLevelWildcard()
        {
            // Arrange
            var singleLevelWildcard = Mqtt.Wildcard.SingleLevel.ToString();

            // Act
            Action validatingTopic = () =>
                singleLevelWildcard.ValidateForTopicAppending();

            // Assert
            validatingTopic.Should()
                .NotThrow<MqttBaseException>("because a single wildcard is allowed to be appended");
        }

        /// <summary>
        /// Ensure that a blank topic is not allowed
        /// </summary>
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
