using MqttTopicBuilder.Builder;
using MqttTopicBuilder.Builder.BuilderState;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Builder.BuilderState;

/// <summary>
/// Unit test suite for <see cref="PublisherState"/>
/// </summary>
public class SubscriberStateUnitTests
{
    /// <summary>
    /// Ensure that the multi-level wildcard addition is allowed
    /// </summary>
    [Fact]
    public void AddMultiLevelWildcard()
    {
        var topicBuilder = new TopicBuilder(Consumer.Subscriber);

        var addingMultiLevelWildcard = () => topicBuilder.AddMultiLevelWildcard();

        addingMultiLevelWildcard.ShouldNotThrow();
    }

    /// <summary>
    /// Ensure that the topic addition is allowed
    /// </summary>
    [Fact]
    public void AddTopic()
    {
        var topicBuilder = new TopicBuilder(Consumer.Subscriber);

        var addingMultiLevelWildcard = () => topicBuilder.AddTopic("bedroom");

        addingMultiLevelWildcard.ShouldNotThrow();
    }

    /// <summary>
    /// Ensure that topics addition is allowed
    /// </summary>
    [Fact]
    public void AddTopics()
    {
        var topicBuilder = new TopicBuilder(Consumer.Subscriber);

        var addingMultiLevelWildcard = () => topicBuilder.AddTopics(["sensors", "bedroom", "temperature"]);

        addingMultiLevelWildcard.ShouldNotThrow();
    }

    /// <summary>
    /// Ensure that the single-level wildcard addition is allowed
    /// </summary>
    [Fact]
    public void AddSingleLevelWildcard()
    {
        var topicBuilder = new TopicBuilder(Consumer.Subscriber);

        var addingMultiLevelWildcard = () => topicBuilder.AddSingleLevelWildcard();

        addingMultiLevelWildcard.ShouldNotThrow();
    }
}
