using System.Collections.Generic;

using AutoFixture;

using FluentAssertions;

using MqttTopicBuilder.Builder;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.Extensions;

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
    /// <see cref="Consumer.Publisher"/>
    /// </summary>
    [Fact]
    public void ToPublisherBuilder_FromPublisherBuilder()
    {
        var topics = Fixture.Create<List<string>>();

        var builder = new TopicBuilder(topics.Count + 1, Consumer.Publisher)
            .AddTopics(topics);

        var publisher = builder.ToPublisherBuilder();

        publisher.Levels
            .Should()
            .Be(builder.Levels, "because the content of the builder should remain the same");

        publisher.TopicCollection.ToArray()
            .Should()
            .Contain(builder.TopicCollection.ToArray(), "because the content of the builder should remain the same");

        publisher.Consumer
            .Should()
            .Be(Consumer.Publisher, "because the consumer should have changed for the converted builder");
    }

    /// <summary>
    /// Ensure the behavior of a conversion of a <see cref="ITopicBuilder"/> whose consumer is
    ///
    /// <see cref="Consumer.Subscriber"/> and contains no wildcard
    /// </summary>
    [Fact]
    public void ToPublisherBuilder_FromSubscriberWithoutWildcards()
    {
        var topics = Fixture.Create<List<string>>();

        var builder = new TopicBuilder(topics.Count + 1, Consumer.Subscriber)
            .AddTopics(topics);

        var publisherBuilder = builder.ToPublisherBuilder();

        publisherBuilder.Levels
            .Should()
            .Be(builder.Levels, "because the content of the builder should remain the same");

        publisherBuilder.TopicCollection.ToArray()
            .Should()
            .Contain(builder.TopicCollection.ToArray(),
                "because the content of the builder should remain the same");

        publisherBuilder.Consumer
            .Should()
            .Be(Consumer.Publisher, "because the consumer should have changed for the converted builder");
    }

    /// <summary>
    /// Ensure the behavior of a conversion of a <see cref="ITopicBuilder"/> whose consumer is
    /// <see cref="Consumer.Subscriber"/> and contains wildcards
    /// </summary>
    [Fact]
    public void ToPublisherBuilder_FromSubscriberWithWildcards()
    {
        var topics = Fixture.Create<List<string>>();
        topics.Add(Mqtt.Wildcard.SingleLevel.ToString());
        topics.AddRange(Fixture.Create<List<string>>());

        var builder = new TopicBuilder(topics.Count + 1, Consumer.Subscriber)
            .AddTopics(topics);

        var convertingSubscriberWithTopicsToPublisher = () => _ = builder.ToPublisherBuilder();

        convertingSubscriberWithTopicsToPublisher
            .Should()
            .Throw<IllegalTopicConstructionException>(
                "because converting a topic should not bypass topic construction rules");
    }

    /// <summary>
    /// Ensure the behavior of a conversion of a <see cref="ITopicBuilder"/> whose consumer is
    /// <see cref="Consumer.Publisher"/>
    /// </summary>
    [Fact]
    public void ToSubscriberBuilder_FromPublisherBuilder()
    {
        var topics = Fixture.Create<List<string>>();

        var publisherBuilder = new TopicBuilder(topics.Count + 1, Consumer.Publisher)
            .AddTopics(topics);

        var subscriberBuilder = publisherBuilder.ToSubscriberBuilder();

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
            .Be(Consumer.Subscriber, "because the consumer should have changed for the converted builder");
    }

    /// <summary>
    /// Ensure the behavior of a conversion of a <see cref="ITopicBuilder"/> whose consumer is
    /// <see cref="Consumer.Subscriber"/>
    /// </summary>
    [Fact]
    public void ToSubscriberBuilder_FromSubscriberBuilder()
    {
        var topics = Fixture.Create<List<string>>();

        var builder = new TopicBuilder(topics.Count + 1, Consumer.Subscriber)
            .AddTopics(topics);

        var subscriberBuilder = builder.ToSubscriberBuilder();

        subscriberBuilder.Levels
            .Should()
            .Be(builder.Levels, "because the content of the builder should remain the same");

        subscriberBuilder.TopicCollection.ToArray()
            .Should()
            .Contain(builder.TopicCollection.ToArray(), "because the content of the builder should remain the same");

        subscriberBuilder.Consumer
            .Should()
            .Be(Consumer.Subscriber, "because the consumer should have changed for the converted builder");
    }
}
