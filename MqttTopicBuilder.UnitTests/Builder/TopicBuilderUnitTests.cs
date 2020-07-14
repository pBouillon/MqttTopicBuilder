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
using MqttTopicBuilder.Builder;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions.Classes;
using MqttTopicBuilder.UnitTests.Utils;
using System;
using System.Collections.Generic;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Builder
{
    /// <summary>
    /// Unit test suite for <see cref="ITopicBuilder"/>
    /// </summary>
    public class TopicBuilderUnitTests
    {
        /// <summary>
        /// Private instance of <see cref="IFixture"/> for test data generation purposes
        /// </summary>
        private static readonly IFixture Fixture = new Fixture();

        /// <summary>
        /// Ensure that the appending of a multi-level wildcard is correctly made
        /// </summary>
        [Fact]
        public void AddMultiLevelWildcard()
        {
            // Arrange
            ITopicBuilder builder = new TopicBuilder(TopicConsumer.Subscriber);

            // Act
            builder = builder.AddMultiLevelWildcard();

            // Assert
            builder.Levels
                .Should()
                .Be(1, "because the wildcard consist of one level");

            builder.IsAppendingAllowed
                .Should()
                .BeFalse("because no addition should be allowed after a multi-level wildcard");
        }

        /// <summary>
        /// Ensure that the appending of a single-level wildcard is correctly made
        /// </summary>
        [Fact]
        public void AddSingleLevelWildcard()
        {
            // Arrange
            ITopicBuilder builder = new TopicBuilder(TopicConsumer.Subscriber);

            // Act
            builder = builder.AddSingleLevelWildcard();

            // Assert
            builder.Levels
                .Should()
                .Be(1, "because the wildcard consist of one level");

            builder.IsAppendingAllowed
                .Should()
                .BeTrue("because a single level wildcard should not block topic appending");
        }

        /// <summary>
        /// Ensure that a blank topic can not be added
        /// </summary>
        [Fact]
        public void AddTopic_OnBlankTopic()
        {
            // Arrange
            ITopicBuilder builder = new TopicBuilder(TopicConsumer.Subscriber);

            // Act
            Action appendingEmptyTopic = () =>
                builder.AddTopic(string.Empty);

            // Assert
            appendingEmptyTopic.Should()
                .Throw<EmptyTopicException>("because an empty topic is not a valid one to be added");
        }

        /// <summary>
        /// Ensure that a multi-level wildcard manually added has the same effects as the
        /// method to perform the same addition
        /// </summary>
        [Fact]
        public void AddTopic_OnMultiLevelWildcard()
        {
            // Arrange
            ITopicBuilder builder = new TopicBuilder(TopicConsumer.Subscriber);

            // Act
            builder = builder.AddTopic(
                Mqtt.Wildcard.MultiLevel.ToString());

            // Assert
            builder.Levels
                .Should()
                .Be(1, "because the wildcard consist of one level");

            builder.IsAppendingAllowed
                .Should()
                .BeFalse("because no addition should be allowed after a multi-level wildcard");
        }

        /// <summary>
        /// Ensure that a single-level wildcard manually added has the same effects as the
        /// method to perform the same addition
        /// </summary>
        [Fact]
        public void AddTopic_OnSingleLevelWildcard()
        {
            // Arrange
            ITopicBuilder builder = new TopicBuilder(TopicConsumer.Subscriber);

            // Act
            builder = builder.AddTopic(
                Mqtt.Wildcard.SingleLevel.ToString());

            // Assert
            builder.Levels
                .Should()
                .Be(1, "because the wildcard consist of one level");

            builder.IsAppendingAllowed
                .Should()
                .BeTrue("because a topic appending is allowed after a single-level wildcard");
        }

        /// <summary>
        /// Ensure that the appending of a topic separator is forbidden
        /// </summary>
        [Fact]
        public void AddTopic_OnTopicSeparator()
        {
            // Arrange
            ITopicBuilder builder = new TopicBuilder(TopicConsumer.Subscriber);

            // Act
            Action appendingTopic = () =>
                builder.AddTopic(
                    Mqtt.Topic.Separator.ToString());

            // Assert
            appendingTopic.Should()
                .Throw<InvalidTopicException>("because the topic separator is not a valid topic to be appended");
        }

        /// <summary>
        /// Ensure that the regular behaviour is valid
        /// </summary>
        [Fact]
        public void AddTopic_OnValidTopic()
        {
            // Arrange
            var addCount = Fixture.Create<int>();
            ITopicBuilder builder = new TopicBuilder(addCount + 1, TopicConsumer.Subscriber);

            // Act
            for (var i = 0; i < addCount; ++i)
            {
                builder = builder.AddTopic(Fixture.Create<string>());
            }

            // Assert
            builder.Levels
                .Should()
                .Be(addCount,
                    "because there should be as many levels as topics added");
        }

        /// <summary>
        /// Ensure that multiple topics can successfully be added at once
        /// </summary>
        [Fact]
        public void AddTopics_OnTopicsWithMultiLevelWildcard()
        {
            // Arrange
            var topics = Fixture.Create<List<string>>();
            topics.Add(Mqtt.Wildcard.MultiLevel.ToString());
            topics.AddRange(Fixture.Create<List<string>>());

            ITopicBuilder builder = new TopicBuilder(topics.Count + 1, TopicConsumer.Subscriber);

            // Act
            Action addTopicsWithAMultiLevelWildcard = () =>
                builder.AddTopics(topics);

            // Assert
            addTopicsWithAMultiLevelWildcard.Should()
                .Throw<IllegalTopicConstructionException>(
                    "because adding a multi-level wildcard among other topics is not valid");
        }

        /// <summary>
        /// Ensure that multiple topics can successfully be added at once
        /// </summary>
        [Fact]
        public void AddTopics_OnValidTopics()
        {
            // Arrange
            var topics = Fixture.Create<string[]>();
            ITopicBuilder builder = new TopicBuilder(topics.Length + 1, TopicConsumer.Subscriber);

            // Act
            builder = builder.AddTopics(topics);

            // Assert
            builder.Levels
                .Should()
                .Be(topics.Length,
                    "because all topics should have been added");
        }

        /// <summary>
        /// Ensure that the building behaviour is valid
        /// </summary>
        [Fact]
        public void Build()
        {
            // Arrange
            ITopicBuilder builder = Fixture.Create<TopicBuilder>();

            var upperBound = builder.MaxLevel - 1;
            var topicCount = (Fixture.Create<int>() % upperBound) + 1;
            for (var i = 0; i < topicCount; ++i)
            { 
                builder = builder.AddTopic(Fixture.Create<string>());
            }

            // Act
            var topic = builder.Build();

            // Assert
            topic.Levels
                .Should()
                .Be(builder.Levels,
                    "because the content of the topic should not be altered");
        }

        /// <summary>
        /// Ensure that the clone is successful
        /// </summary>
        [Fact]
        public void Clone()
        {
            // Arrange
            var initial = Fixture.Create<TopicBuilder>();

            // Act
            var clone = initial.Clone();

            // Assert
            clone.MaxLevel.Should()
                .Be(initial.MaxLevel, 
                    "because the same max level should have been copied");

            clone.Levels.Should()
                .Be(initial.Levels, 
                    "because the content level count should also have been cloned");
        }

        /// <summary>
        /// Ensure that the clone is not altered when modifying the original copy
        /// </summary>
        [Fact]
        public void Clone_OnAlteredOriginalInstance()
        {
            // Arrange
            var initial = Fixture.Create<TopicBuilder>();
            var clone = initial.Clone();
            var initialCount = initial.Levels;

            // Act
            initial.AddTopic(TestUtils.GenerateSingleValidTopic());

            // Assert
            clone.Levels.Should()
                .Be(initialCount,
                    "because altering the origin should not alter the cloned instance");
        }
    }
}
