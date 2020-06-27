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
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using MqttTopicBuilder.Collection;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions.Classes;
using MqttTopicBuilder.UnitTests.Utils;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Collection
{
    /// <summary>
    /// Unit test suite for <see cref="ITopicCollection"/>
    /// </summary>
    public class TopicCollectionUnitTests
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
            ITopicCollection collection = new TopicCollection(Fixture.Create<int>());

            // Act
            collection = collection.AddMultiLevelWildcard();

            // Assert
            collection.Levels
                .Should()
                .Be(1, "because the wildcard consist of one level");

            collection.IsAppendingAllowed
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
            ITopicCollection collection = new TopicCollection(Fixture.Create<int>());

            // Act
            collection = collection.AddSingleLevelWildcard();

            // Assert
            collection.Levels
                .Should()
                .Be(1, "because the wildcard consist of one level");

            collection.IsAppendingAllowed
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
            ITopicCollection collection = new TopicCollection(Fixture.Create<int>());

            // Act
            Action appendingEmptyTopic = () =>
                collection.AddTopic(string.Empty);

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
            ITopicCollection collection = new TopicCollection(Fixture.Create<int>());

            // Act
            collection = collection.AddTopic(
                Mqtt.Wildcard.MultiLevel.ToString());

            // Assert
            collection.Levels
                .Should()
                .Be(1, "because the wildcard consist of one level");

            collection.IsAppendingAllowed
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
            ITopicCollection collection = new TopicCollection(Fixture.Create<int>());

            // Act
            collection = collection.AddTopic(
                Mqtt.Wildcard.SingleLevel.ToString());

            // Assert
            collection.Levels
                .Should()
                .Be(1, "because the wildcard consist of one level");

            collection.IsAppendingAllowed
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
            ITopicCollection collection = new TopicCollection(Fixture.Create<int>());

            // Act
            Action appendingTopic = () =>
                collection.AddTopic(
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
            ITopicCollection collection = new TopicCollection(addCount + 1);

            // Act
            for (var i = 0; i < addCount; ++i)
            {
                collection = collection.AddTopic(Fixture.Create<string>());
            }

            // Assert
            collection.Levels
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
            ITopicCollection collection = new TopicCollection(topics.Count + 1);

            // Act
            Action addTopicsWithAMultiLevelWildcard = () =>
                collection.AddTopics(topics);

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
            ITopicCollection collection = new TopicCollection(topics.Length + 1);

            // Act
            collection = collection.AddTopics(topics);

            // Assert
            collection.Levels
                .Should()
                .Be(topics.Length,
                    "because all topics should have been added");

            collection.ToArray()
                .Should()
                .Contain(topics,
                    "because the same topics as the ones provided should have been added");
        }

        /// <summary>
        /// Ensure that the clone is successful
        /// </summary>
        [Fact]
        public void Clone()
        {
            // Arrange
            var initial = Fixture.Create<TopicCollection>();

            // Act
            var clone = initial.Clone();

            // Assert
            clone.MaxLevel.Should()
                .Be(initial.MaxLevel, 
                    "because the same max level should have been copied");

            clone.Levels.Should()
                .Be(initial.Levels, 
                    "because the content level count should also have been cloned");

            clone.ToArray().Should()
                .BeEquivalentTo(initial.ToArray(), 
                    "because all elements should also have been clones");
        }

        /// <summary>
        /// Ensure that the clone is not altered when modifying the original copy
        /// </summary>
        [Fact]
        public void Clone_OnAlteredOriginalInstance()
        {
            // Arrange
            var initial = Fixture.Create<TopicCollection>();
            var clone = initial.Clone();
            var initialCount = initial.Levels;

            // Act
            initial.AddTopic(TestUtils.GenerateSingleValidTopic());

            // Assert
            clone.Levels.Should()
                .Be(initialCount,
                    "because altering the origin should not alter the cloned instance");
        }

        /// <summary>
        /// Ensure that the maximum level is successfully set
        /// </summary>
        [Fact]
        public void TopicCollection_MaxLevel()
        {
            // Arrange
            var maxLevel = Fixture.Create<int>();

            // Act
            var collection = new TopicCollection(maxLevel);

            // Assert
            collection.MaxLevel.Should()
                .Be(maxLevel, "because the provided value should be the upper bound");
        }
    }
}
