using System.Collections.Generic;

using AutoFixture;

using FluentAssertions;

using MqttTopicBuilder.Builder;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;

using Xunit;

namespace MqttTopicBuilder.UnitTests.Builder;

/// <summary>
/// Unit test suite for <see cref="ITopicBuilder"/> when used
/// along with <see cref="Consumer.Publisher"/>
/// </summary>
public class PublisherTopicBuilderUnitTests
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
        ITopicBuilder builder = new TopicBuilder(Consumer.Publisher);

        var addMultiLevelWildcard = () => builder = builder.AddMultiLevelWildcard();

        addMultiLevelWildcard
            .Should()
            .Throw<IllegalStateOperationException>("because a topic used on PUBLISH mode cannot use wildcard");
    }

    /// <summary>
    /// Ensure that the appending of a single-level wildcard is correctly made
    /// </summary>
    [Fact]
    public void AddSingleLevelWildcard()
    {
        ITopicBuilder builder = new TopicBuilder(Consumer.Publisher);

        var addSingleLevelWildcard = () => builder = builder.AddMultiLevelWildcard();

        addSingleLevelWildcard
            .Should()
            .Throw<IllegalStateOperationException>("because a topic used on PUBLISH mode cannot use wildcard");
    }

    /// <summary>
    /// Ensure that a blank topic can not be added
    /// </summary>
    [Fact]
    public void AddTopic_OnBlankTopic()
    {
        ITopicBuilder builder = new TopicBuilder(Consumer.Publisher);

        var appendingEmptyTopic = () => builder.AddTopic(string.Empty);

        appendingEmptyTopic
            .Should()
            .Throw<EmptyTopicException>("because an empty topic is not a valid one to be added");
    }

    /// <summary>
    /// Ensure that a multi-level wildcard manually added has the same effects as the
    /// method to perform the same addition
    /// </summary>
    [Fact]
    public void AddTopic_OnMultiLevelWildcard()
    {
        ITopicBuilder builder = new TopicBuilder(Consumer.Publisher);

        var addSingleLevelWildcard = () => builder = builder.AddTopic(
                Mqtt.Wildcard.MultiLevel.ToString());

        addSingleLevelWildcard
            .Should()
            .Throw<IllegalTopicConstructionException>("because a topic used on PUBLISH mode cannot use wildcard");
    }

    /// <summary>
    /// Ensure that a single-level wildcard manually added has the same effects as the
    /// method to perform the same addition
    /// </summary>
    [Fact]
    public void AddTopic_OnSingleLevelWildcard()
    {
        ITopicBuilder builder = new TopicBuilder(Consumer.Publisher);

        var addSingleLevelWildcard = () => builder = builder.AddTopic(
                Mqtt.Wildcard.SingleLevel.ToString());

        addSingleLevelWildcard
            .Should()
            .Throw<IllegalTopicConstructionException>("because a topic used on PUBLISH mode cannot use wildcard");
    }

    /// <summary>
    /// Ensure that the appending of a topic separator is forbidden
    /// </summary>
    [Fact]
    public void AddTopic_OnTopicSeparator()
    {
        ITopicBuilder builder = new TopicBuilder(Consumer.Publisher);

        var appendingTopic = () => builder.AddTopic(
                Mqtt.Topic.Separator.ToString());

        appendingTopic
            .Should()
            .Throw<InvalidTopicException>("because the topic separator is not a valid topic to be appended");
    }

    /// <summary>
    /// Ensure that the regular behavior is valid
    /// </summary>
    [Fact]
    public void AddTopic_OnValidTopic()
    {
        var addCount = Fixture.Create<int>();
        ITopicBuilder builder = new TopicBuilder(addCount + 1, Consumer.Publisher);

        for (var i = 0; i < addCount; ++i)
        {
            builder = builder.AddTopic(Fixture.Create<string>());
        }

        builder.Levels
            .Should()
            .Be(addCount, "because there should be as many levels as topics added");
    }

    /// <summary>
    /// Ensure that multiple topics can successfully be added at once
    /// </summary>
    [Fact]
    public void AddTopics_OnTopicsWithMultiLevelWildcard()
    {
        var topics = Fixture.Create<List<string>>();
        topics.Add(Mqtt.Wildcard.MultiLevel.ToString());
        topics.AddRange(Fixture.Create<List<string>>());

        ITopicBuilder builder = new TopicBuilder(topics.Count + 1, Consumer.Publisher);

        var addTopicsWithAMultiLevelWildcard = () => builder.AddTopics(topics);

        addTopicsWithAMultiLevelWildcard
            .Should()
            .Throw<IllegalTopicConstructionException>(
                "because adding a multi-level wildcard among other topics is not valid");
    }

    /// <summary>
    /// Ensure that multiple topics can successfully be added at once
    /// </summary>
    [Fact]
    public void AddTopics_OnValidTopics()
    {
        var topics = Fixture.Create<string[]>();
        ITopicBuilder builder = new TopicBuilder(topics.Length + 1, Consumer.Publisher);

        builder = builder.AddTopics(topics);

        builder.Levels
            .Should()
            .Be(topics.Length, "because all topics should have been added");
    }
}
