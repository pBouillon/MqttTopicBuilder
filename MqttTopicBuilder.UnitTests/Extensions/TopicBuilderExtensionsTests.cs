using AutoFixture;

using FluentAssertions;

using MqttTopicBuilder.Builder;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Extensions;

using System;
using System.Collections.Generic;
using MqttTopicBuilder.Exceptions;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Extensions;

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
    public void ToPublisherBuilder_FromPublisherBuilder()
    {
        // Arrange
        var topics = Fixture.Create<List<string>>();

        var builder = new TopicBuilder(topics.Count + 1, TopicConsumer.Publisher)
            .AddTopics(topics);

        // Act
        var publisher = builder.ToPublisherBuilder();

        // Assert
        publisher.Levels
            .Should()
            .Be(builder.Levels,
                "because the content of the builder should remain the same");

        publisher.TopicCollection.ToArray()
            .Should()
            .Contain(builder.TopicCollection.ToArray(),
                "because the content of the builder should remain the same");

        publisher.Consumer
            .Should()
            .Be(TopicConsumer.Publisher,
                "because the consumer should have changed for the converted builder");
    }

    /// <summary>
    /// Ensure the behavior of a conversion of a <see cref="ITopicBuilder"/> whose consumer is
    ///
    /// <see cref="TopicConsumer.Subscriber"/> and contains no wildcard
    /// </summary>
    [Fact]
    public void ToPublisherBuilder_FromSubscriberWithoutWildcards()
    {
        // Arrange
        var topics = Fixture.Create<List<string>>();

        var builder = new TopicBuilder(topics.Count + 1, TopicConsumer.Subscriber)
            .AddTopics(topics);

        // Act
        var publisherBuilder = builder.ToPublisherBuilder();

        // Assert
        publisherBuilder.Levels
            .Should()
            .Be(builder.Levels,
                "because the content of the builder should remain the same");

        publisherBuilder.TopicCollection.ToArray()
            .Should()
            .Contain(builder.TopicCollection.ToArray(),
                "because the content of the builder should remain the same");

        publisherBuilder.Consumer
            .Should()
            .Be(TopicConsumer.Publisher,
                "because the consumer should have changed for the converted builder");
    }

    /// <summary>
    /// Ensure the behavior of a conversion of a <see cref="ITopicBuilder"/> whose consumer is
    /// <see cref="TopicConsumer.Subscriber"/> and contains wildcards
    /// </summary>
    [Fact]
    public void ToPublisherBuilder_FromSubscriberWithWildcards()
    {
        // Arrange
        var topics = Fixture.Create<List<string>>();
        topics.Add(Mqtt.Wildcard.SingleLevel.ToString());
        topics.AddRange(Fixture.Create<List<string>>());

        var builder = new TopicBuilder(topics.Count + 1, TopicConsumer.Subscriber)
            .AddTopics(topics);

        // Act
        Action convertingSubscriberWithTopicsToPublisher = () =>
            _ = builder.ToPublisherBuilder();

        // Assert
        convertingSubscriberWithTopicsToPublisher.Should()
            .Throw<IllegalTopicConstructionException>(
                "because converting a topic should not bypass topic construction rules");
    }

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