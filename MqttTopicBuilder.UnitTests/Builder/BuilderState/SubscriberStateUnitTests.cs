using AutoFixture;

using FluentAssertions;

using MqttTopicBuilder.Builder;
using MqttTopicBuilder.Builder.BuilderState;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.UnitTests.Utils;

using System.Collections.Generic;
using MqttTopicBuilder.Exceptions;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Builder.BuilderState;

/// <summary>
/// Unit test suite for <see cref="PublisherState"/>
/// </summary>
public class SubscriberStateUnitTests
{
    /// <summary>
    /// Private instance of <see cref="IFixture"/> for test data generation purposes
    /// </summary>
    private static readonly IFixture Fixture = new Fixture();

    /// <summary>
    /// Ensure that the multi-level wildcard addition is allowed
    /// </summary>
    [Fact]
    public void AddMultiLevelWildcard()
    {
        var topicBuilder = new TopicBuilder(TopicConsumer.Subscriber);
        var subscriberState = new SubscriberState(topicBuilder);

        var addingMultiLevelWildcard = () => subscriberState.AddMultiLevelWildcard();

        addingMultiLevelWildcard
            .Should()
            .NotThrow<MqttBaseException>("because adding a wildcard is allowed when subscribing");
    }

    /// <summary>
    /// Ensure that the topic addition is allowed
    /// </summary>
    [Fact]
    public void AddTopic()
    {
        var topicBuilder = new TopicBuilder(TopicConsumer.Subscriber);
        var subscriberState = new SubscriberState(topicBuilder);

        var topic = TestUtils.GenerateSingleValidTopic();

        var addingMultiLevelWildcard = () => subscriberState.AddTopic(topic);

        addingMultiLevelWildcard
            .Should()
            .NotThrow<MqttBaseException>("because adding a topic should be allowed on subscribe");
    }

    /// <summary>
    /// Ensure that topics addition is allowed
    /// </summary>
    [Fact]
    public void AddTopics()
    {
        var topicBuilder = new TopicBuilder(TopicConsumer.Subscriber);
        var subscriberState = new SubscriberState(topicBuilder);

        var count = Fixture.Create<int>() % Mqtt.Topic.MaximumAllowedLevels;
        var topics = new Queue<string>();

        for (var i = 0; i < count; ++i)
        {
            topics.Enqueue(TestUtils.GenerateSingleValidTopic());
        }

        var addingMultiLevelWildcard = () => subscriberState.AddTopics(topics);

        addingMultiLevelWildcard
            .Should()
            .NotThrow<MqttBaseException>("because adding a topic should be allowed on subscribe");
    }

    /// <summary>
    /// Ensure that the single-level wildcard addition is allowed
    /// </summary>
    [Fact]
    public void AddSingleLevelWildcard()
    {
        var topicBuilder = new TopicBuilder(TopicConsumer.Subscriber);
        var subscriberState = new SubscriberState(topicBuilder);

        var addingMultiLevelWildcard = () => subscriberState.AddSingleLevelWildcard();

        addingMultiLevelWildcard
            .Should()
            .NotThrow<MqttBaseException>("because adding a wildcard is allowed when subscribing");
    }
}
