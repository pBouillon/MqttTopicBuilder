using System.Collections.Generic;

using Shouldly;

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
    /// Ensure the behavior of a conversion of a <see cref="ITopicBuilder"/> whose consumer is
    /// <see cref="Consumer.Publisher"/>
    /// </summary>
    [Fact]
    public void ToPublisherBuilder_FromPublisherBuilder()
    {
        var builder = new TopicBuilder(3, Consumer.Publisher)
            .AddTopics(["sensors", "bedroom", "temperature"]);

        var publisher = builder.ToPublisherBuilder();

        publisher.Levels.ShouldBe(builder.Levels);
        publisher.TopicCollection.ShouldBeEquivalentTo(builder.TopicCollection);
        publisher.Consumer.ShouldBe(Consumer.Publisher);
    }

    /// <summary>
    /// Ensure the behavior of a conversion of a <see cref="ITopicBuilder"/> whose consumer is
    ///
    /// <see cref="Consumer.Subscriber"/> and contains no wildcard
    /// </summary>
    [Fact]
    public void ToPublisherBuilder_FromSubscriberWithoutWildcards()
    {
        var builder = new TopicBuilder(3, Consumer.Subscriber)
            .AddTopics(["sensors", "bedroom", "temperature"]);

        var publisherBuilder = builder.ToPublisherBuilder();

        publisherBuilder.Levels.ShouldBe(builder.Levels);

        publisherBuilder.TopicCollection.ShouldBeEquivalentTo(builder.TopicCollection);

        publisherBuilder.Consumer.ShouldBe(Consumer.Publisher);
    }

    /// <summary>
    /// Ensure the behavior of a conversion of a <see cref="ITopicBuilder"/> whose consumer is
    /// <see cref="Consumer.Subscriber"/> and contains wildcards
    /// </summary>
    [Fact]
    public void ToPublisherBuilder_FromSubscriberWithWildcards()
    {
        var builder = new TopicBuilder(3, Consumer.Subscriber)
            .AddTopics(["sensors", Mqtt.Wildcard.SingleLevel.ToString(), "temperature"]);

        var convertingSubscriberWithTopicsToPublisher = () => _ = builder.ToPublisherBuilder();

        convertingSubscriberWithTopicsToPublisher
            .ShouldThrow<IllegalTopicConstructionException>();
    }

    /// <summary>
    /// Ensure the behavior of a conversion of a <see cref="ITopicBuilder"/> whose consumer is
    /// <see cref="Consumer.Publisher"/>
    /// </summary>
    [Fact]
    public void ToSubscriberBuilder_FromPublisherBuilder()
    {
        var publisherBuilder = new TopicBuilder(3, Consumer.Publisher)
            .AddTopics(["sensors", "bedroom", "temperature"]);

        var subscriberBuilder = publisherBuilder.ToSubscriberBuilder();

        subscriberBuilder.Levels.ShouldBe(publisherBuilder.Levels);

        subscriberBuilder.TopicCollection.ShouldBeEquivalentTo(publisherBuilder.TopicCollection);

        subscriberBuilder.Consumer.ShouldBe(Consumer.Subscriber);
    }

    /// <summary>
    /// Ensure the behavior of a conversion of a <see cref="ITopicBuilder"/> whose consumer is
    /// <see cref="Consumer.Subscriber"/>
    /// </summary>
    [Fact]
    public void ToSubscriberBuilder_FromSubscriberBuilder()
    {
        var builder = new TopicBuilder(3, Consumer.Subscriber)
            .AddTopics(["sensors", "bedroom", "temperature"]);

        var subscriberBuilder = builder.ToSubscriberBuilder();

        subscriberBuilder.Levels.ShouldBe(builder.Levels);

        subscriberBuilder.TopicCollection.ShouldBeEquivalentTo(builder.TopicCollection);

        subscriberBuilder.Consumer.ShouldBe(Consumer.Subscriber);
    }
}
