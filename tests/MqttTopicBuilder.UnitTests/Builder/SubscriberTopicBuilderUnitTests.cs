using MqttTopicBuilder.Builder;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;
using Shouldly;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Builder;

/// <summary>
/// Unit test suite for <see cref="ITopicBuilder"/> when used
/// along with <see cref="Consumer.Subscriber"/>
/// </summary>
public class SubscriberTopicBuilderUnitTests
{

    /// <summary>
    /// Ensure that the appending of a multi-level wildcard is correctly made
    /// </summary>
    [Fact]
    public void AddMultiLevelWildcard()
    {
        ITopicBuilder builder = new TopicBuilder(Consumer.Subscriber);

        builder = builder.AddMultiLevelWildcard();

        builder.Levels.ShouldBe(1);
        builder.IsAppendingAllowed.ShouldBeFalse();
    }

    /// <summary>
    /// Ensure that the appending of a single-level wildcard is correctly made
    /// </summary>
    [Fact]
    public void AddSingleLevelWildcard()
    {
        ITopicBuilder builder = new TopicBuilder(Consumer.Subscriber);

        builder = builder.AddSingleLevelWildcard();

        builder.Levels.ShouldBe(1);
        builder.IsAppendingAllowed.ShouldBeTrue();
    }

    /// <summary>
    /// Ensure that a blank topic can not be added
    /// </summary>
    [Fact]
    public void AddTopic_OnBlankTopic()
    {
        var builder = new TopicBuilder(Consumer.Subscriber);

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
        ITopicBuilder builder = new TopicBuilder(Consumer.Subscriber);

        builder = builder.AddTopic(Mqtt.Wildcard.MultiLevel.ToString());

        builder.Levels.ShouldBe(1);
        builder.IsAppendingAllowed.ShouldBeFalse();
    }

    /// <summary>
    /// Ensure that a single-level wildcard manually added has the same effects as the
    /// method to perform the same addition
    /// </summary>
    [Fact]
    public void AddTopic_OnSingleLevelWildcard()
    {
        ITopicBuilder builder = new TopicBuilder(Consumer.Subscriber);

        builder = builder.AddTopic(Mqtt.Wildcard.SingleLevel.ToString());

        builder.Levels.ShouldBe(1);
        builder.IsAppendingAllowed.ShouldBeTrue();
    }

    /// <summary>
    /// Ensure that the appending of a topic separator is forbidden
    /// </summary>
    [Fact]
    public void AddTopic_OnTopicSeparator()
    {
        var builder = new TopicBuilder(Consumer.Subscriber);

        var appendingTopic = () => builder.AddTopic(Mqtt.Topic.Separator.ToString());

        appendingTopic.ShouldThrow<InvalidTopicException>();
    }

    /// <summary>
    /// Ensure that the regular behavior is valid
    /// </summary>
    [Fact]
    public void AddTopic_OnValidTopic()
    {
        var builder = new TopicBuilder(Consumer.Subscriber)
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

        var builder = new TopicBuilder(Consumer.Subscriber);

        var addTopicsWithAMultiLevelWildcard = () => builder.AddTopics(topics);

        addTopicsWithAMultiLevelWildcard.ShouldThrow<IllegalTopicConstructionException>();
    }


    /// <summary>
    /// Ensure that multiple topics can successfully be added at once
    /// </summary>
    [Fact]
    public void AddTopics_OnValidTopics()
    {

        ITopicBuilder builder = new TopicBuilder(Consumer.Subscriber);

        builder = builder.AddTopics(["sensors", "bedroom", "temperature"]);

        builder.Levels.ShouldBe(3);

    }
}
