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
using MqttTopicBuilder.Builder.BuilderState;
using MqttTopicBuilder.Exceptions.Classes;
using MqttTopicBuilder.UnitTests.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using MqttTopicBuilder.Constants;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Builder.BuilderState
{
    /// <summary>
    /// Unit test suite for <see cref="PublisherState"/>
    /// </summary>
    public class PublisherStateUnitTests
    {
        /// <summary>
        /// Private instance of <see cref="IFixture"/> for test data generation purposes
        /// </summary>
        private static readonly IFixture Fixture = new Fixture();

        /// <summary>
        /// Ensure that the multi-level wildcard addition is prevented
        /// </summary>
        [Fact]
        public void AddMultiLevelWildcard()
        {
            // Arrange
            var topicBuilder = Fixture.Create<TopicBuilder>();
            var subscriberState = new PublisherState(topicBuilder);

            // Act
            Action addingMultiLevelWildcard = () =>
                subscriberState.AddMultiLevelWildcard();

            // Assert
            addingMultiLevelWildcard.Should()
                .Throw<IllegalStateOperationException>(
                    "because adding a wildcard is not allowed when publishing");
        }

        /// <summary>
        /// Ensure that the topic addition is allowed
        /// </summary>
        [Fact]
        public void AddTopic()
        {
            // Arrange
            var topicBuilder = Fixture.Create<TopicBuilder>();
            var subscriberState = new PublisherState(topicBuilder);

            var topic = TestUtils.GenerateSingleValidTopic();


            // Act
            Action addingMultiLevelWildcard = () =>
                subscriberState.AddTopic(topic);

            // Assert
            addingMultiLevelWildcard.Should()
                .NotThrow<MqttBaseException>(
                    "because adding a topic should be allowed on subscribe");
        }

        /// <summary>
        /// Ensure that topics addition is allowed
        /// </summary>
        [Fact]
        public void AddTopics()
        {
            // Arrange
            var topicBuilder = Fixture.Create<TopicBuilder>();
            var subscriberState = new PublisherState(topicBuilder);

            var count = Fixture.Create<int>() % Mqtt.Topic.MaximumAllowedLevels;
            var topics = new Queue<string>();
            
            for (var i = 0; i < count; ++i)
            {
                topics.Enqueue(TestUtils.GenerateSingleValidTopic());
            }

            // Act
            Action addingMultiLevelWildcard = () =>
                subscriberState.AddTopics(topics);

            // Assert
            addingMultiLevelWildcard.Should()
                .NotThrow<MqttBaseException>(
                    "because adding a topic should be allowed on subscribe");
        }

        /// <summary>
        /// Ensure that the single-level wildcard addition is forbidden
        /// </summary>
        [Fact]
        public void AddSingleLevelWildcard()
        {
            // Arrange
            var topicBuilder = Fixture.Create<TopicBuilder>();
            var subscriberState = new PublisherState(topicBuilder);

            // Act
            Action addingMultiLevelWildcard = () =>
                subscriberState.AddSingleLevelWildcard();

            // Assert
            addingMultiLevelWildcard.Should()
                .Throw<MqttBaseException>(
                    "because adding a topic should not be allowed on subscribe");
        }
    }
}
