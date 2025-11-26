using MqttTopicBuilder.Builder;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Builder;

/// <summary>
/// Unit test suite for <see cref="ITopicBuilder"/> when used
/// along with <see cref="Consumer.Publisher"/>
/// </summary>
public class PublisherTopicBuilderUnitTests
{
    /// <summary>
    /// Ensure that the appending of a multi-level wildcard is correctly made
    /// </summary>
    [Fact]
    public void AddMultiLevelWildcard()
    {
        var builder = new TopicBuilder(Consumer.Publisher);

        var addMultiLevelWildcard = () => builder.AddMultiLevelWildcard();

        addMultiLevelWildcard.ShouldThrow<IllegalStateOperationException>();
    }

    /// <summary>
    /// Ensure that the appending of a single-level wildcard is correctly made
    /// </summary>
    [Fact]
    public void AddSingleLevelWildcard()
    {
        var builder = new TopicBuilder(Consumer.Publisher);

        var addSingleLevelWildcard = () => builder.AddMultiLevelWildcard();

        addSingleLevelWildcard.ShouldThrow<IllegalStateOperationException>();
    }

    /// <summary>
    /// Ensure that a blank topic can not be added
    /// </summary>
    [Fact]
    public void AddTopic_OnBlankTopic()
    {
        var builder = new TopicBuilder(Consumer.Publisher);

        var appendingEmptyTopic = () => builder.AddTopic(string.Empty);

        appendingEmptyTopic.ShouldThrow<EmptyTopicException>();
    }

    /// <summary>
    /// Ensure that a multi-level wildcard manually added has the same effects as the
    /// method to perform the same addition
    /// </summary>
    [Fact]
    public void AddTopic_OnMultiLevelWildcard()
    {
        var builder = new TopicBuilder(Consumer.Publisher);

        var addSingleLevelWildcard = () => builder.AddTopic(Mqtt.Wildcard.MultiLevel.ToString());

        addSingleLevelWildcard.ShouldThrow<IllegalTopicConstructionException>();
    }

    /// <summary>
    /// Ensure that a single-level wildcard manually added has the same effects as the
    /// method to perform the same addition
    /// </summary>
    [Fact]
    public void AddTopic_OnSingleLevelWildcard()
    {
        var builder = new TopicBuilder(Consumer.Publisher);

        var addSingleLevelWildcard = () => builder.AddTopic(Mqtt.Wildcard.SingleLevel.ToString());

        addSingleLevelWildcard.ShouldThrow<IllegalTopicConstructionException>();
    }

    /// <summary>
    /// Ensure that the appending of a topic separator is forbidden
    /// </summary>
    [Fact]
    public void AddTopic_OnTopicSeparator()
    {
        var builder = new TopicBuilder(Consumer.Publisher);

        var appendingTopic = () => builder.AddTopic(Mqtt.Topic.Separator.ToString());

        appendingTopic.ShouldThrow<InvalidTopicException>();
    }

    /// <summary>
    /// Ensure that the regular behavior is valid
    /// </summary>
    [Fact]
    public void AddTopic_OnValidTopic()
    {
        ITopicBuilder builder = new TopicBuilder(5, Consumer.Publisher);

        builder = builder
            .AddTopic("sensors")
            .AddTopic("bedroom")
            .AddTopic("temperature");

        builder.Levels.ShouldBe(3);
    }

    /// <summary>
    /// Ensure that multiple topics can successfully be added at once
    /// </summary>
    [Fact]
    public void AddTopics_OnTopicsWithMultiLevelWildcard()
    {
        string[] topics = [
            "sensors",
            Mqtt.Wildcard.MultiLevel.ToString(),
            "temperature",
        ];

        var builder = new TopicBuilder(Consumer.Publisher);

        var addTopicsWithAMultiLevelWildcard = () => builder.AddTopics(topics);

        addTopicsWithAMultiLevelWildcard.ShouldThrow<IllegalTopicConstructionException>();
    }

    /// <summary>
    /// Ensure that multiple topics can successfully be added at once
    /// </summary>
    [Fact]
    public void AddTopics_OnValidTopics()
    {
        ITopicBuilder builder = new TopicBuilder(Consumer.Publisher);

        builder = builder.AddTopics(["sensors", "bedroom", "temperature"]);

        builder.Levels.ShouldBe(3);
    }
}
