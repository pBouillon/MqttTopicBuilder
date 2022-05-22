using AutoFixture;

using FluentAssertions;

using MqttTopicBuilder.Builder;
using MqttTopicBuilder.Builder.BuilderState;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions.Classes;
using MqttTopicBuilder.UnitTests.Utils;

using System;
using System.Collections.Generic;

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
        // Arrange
        var topicBuilder = new TopicBuilder(TopicConsumer.Subscriber);
        var subscriberState = new SubscriberState(topicBuilder);

        // Act
        Action addingMultiLevelWildcard = () =>
            subscriberState.AddMultiLevelWildcard();

        // Assert
        addingMultiLevelWildcard.Should()
            .NotThrow<MqttBaseException>(
                "because adding a wildcard is allowed when subscribing");
    }

    /// <summary>
    /// Ensure that the topic addition is allowed
    /// </summary>
    [Fact]
    public void AddTopic()
    {
        // Arrange
        var topicBuilder = new TopicBuilder(TopicConsumer.Subscriber);
        var subscriberState = new SubscriberState(topicBuilder);

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
        var topicBuilder = new TopicBuilder(TopicConsumer.Subscriber);
        var subscriberState = new SubscriberState(topicBuilder);

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
    /// Ensure that the single-level wildcard addition is allowed
    /// </summary>
    [Fact]
    public void AddSingleLevelWildcard()
    {
        // Arrange
        var topicBuilder = new TopicBuilder(TopicConsumer.Subscriber);
        var subscriberState = new SubscriberState(topicBuilder);

        // Act
        Action addingMultiLevelWildcard = () =>
            subscriberState.AddSingleLevelWildcard();

        // Assert
        addingMultiLevelWildcard.Should()
            .NotThrow<MqttBaseException>(
                "because adding a wildcard is allowed when subscribing");
    }
}