using MqttTopicBuilder.Builder;
using MqttTopicBuilder.Builder.BuilderState;
using MqttTopicBuilder.Exceptions;
using Shouldly;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Builder.BuilderState;

/// <summary>
/// Unit test suite for <see cref="PublisherState"/>
/// </summary>
public class PublisherStateUnitTests
{
    /// <summary>
    /// Ensure that the multi-level wildcard addition is prevented
    /// </summary>
    [Fact]
    public void AddMultiLevelWildcard()
    {
        var topicBuilder = new TopicBuilder(Consumer.Publisher);

        var addingMultiLevelWildcard = () => topicBuilder.AddMultiLevelWildcard();

        addingMultiLevelWildcard.ShouldThrow<IllegalStateOperationException>();
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
        var topicBuilder = new TopicBuilder(Consumer.Publisher);

        var addingMultiLevelWildcard = () => topicBuilder.AddTopics(["sensors", "bedroom", "temperature"]);

        addingMultiLevelWildcard.ShouldNotThrow();
    }

    /// <summary>
    /// Ensure that the single-level wildcard addition is forbidden
    /// </summary>
    [Fact]
    public void AddSingleLevelWildcard()
    {
        var topicBuilder = new TopicBuilder(Consumer.Publisher);

        var addingMultiLevelWildcard = () => topicBuilder.AddSingleLevelWildcard();

        addingMultiLevelWildcard.ShouldThrow<MqttBaseException>();
    }
}
