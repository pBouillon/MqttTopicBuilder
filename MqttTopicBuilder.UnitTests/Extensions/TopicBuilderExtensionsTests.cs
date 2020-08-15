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
using MqttTopicBuilder.Builder;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions.Classes;
using MqttTopicBuilder.Extensions;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Extensions
{
    /// <summary>
    /// Unit test suite for <see cref="TopicBuilderExtensions"/>
    /// </summary>
    public class TopicBuilderExtensionsTests
    {
        /// <summary>
        /// Private instance of <see cref="IFixture"/> for test data generation purposes
        /// </summary>
        private static readonly IFixture Fixture = new Fixture();

        /// <summary>
        /// Ensure the behavior of a conversion of a <see cref="ITopicBuilder"/> whose consumer is
        /// <see cref="TopicConsumer.Publisher"/>
        /// </summary>
        [Fact]
        public void ToSubscriberBuilder_FromPublisherBuilder()
        {
            // Arrange
            var topics = Fixture.Create<List<string>>();

            var publisherBuilder = new TopicBuilder(topics.Count + 1, TopicConsumer.Publisher)
                .AddTopics(topics);

            // Act
            var subscriberBuilder = publisherBuilder.ToSubscriberBuilder();

            // Assert
            subscriberBuilder.Levels
                .Should()
                .Be(publisherBuilder.Levels,
                    "because the content of the builder should remain the same");

            subscriberBuilder.TopicCollection.ToArray()
                .Should()
                .Contain(publisherBuilder.TopicCollection.ToArray(),
                    "because the content of the builder should remain the same");

            subscriberBuilder.Consumer
                .Should()
                .Be(TopicConsumer.Subscriber,
                    "because the consumer should have changed for the converted builder");
        }

        /// <summary>
        /// Ensure the behavior of a conversion of a <see cref="ITopicBuilder"/> whose consumer is
        /// <see cref="TopicConsumer.Subscriber"/>
        /// </summary>
        [Fact]
        public void ToSubscriberBuilder_FromSubscriberBuilder()
        {
            // Arrange
            var topics = Fixture.Create<List<string>>();

            var builder = new TopicBuilder(topics.Count + 1, TopicConsumer.Subscriber)
                .AddTopics(topics);

            // Act
            var subscriberBuilder = builder.ToSubscriberBuilder();

            // Assert
            subscriberBuilder.Levels
                .Should()
                .Be(builder.Levels,
                    "because the content of the builder should remain the same");

            subscriberBuilder.TopicCollection.ToArray()
                .Should()
                .Contain(builder.TopicCollection.ToArray(),
                    "because the content of the builder should remain the same");

            subscriberBuilder.Consumer
                .Should()
                .Be(TopicConsumer.Subscriber,
                    "because the consumer should have changed for the converted builder");
        }
    }
}
