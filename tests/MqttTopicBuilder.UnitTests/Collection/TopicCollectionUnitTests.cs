using System.Collections.Generic;

using Shouldly;


using MqttTopicBuilder.Collection;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;

using Xunit;

namespace MqttTopicBuilder.UnitTests.Collection;

/// <summary>
/// Unit test suite for <see cref="ITopicCollection"/>
/// </summary>
public class TopicCollectionUnitTests
{
    /// <summary>
    /// Ensure that the appending of a multi-level wildcard is correctly made
    /// </summary>
    [Fact]
    public void AddMultiLevelWildcard()
    {
        var collection = new TopicCollection(5).AddMultiLevelWildcard();

        collection.Levels.ShouldBe(1);
        collection.IsAppendingAllowed.ShouldBeFalse();
    }

    /// <summary>
    /// Ensure that the appending of a single-level wildcard is correctly made
    /// </summary>
    [Fact]
    public void AddSingleLevelWildcard()
    {
        ITopicCollection collection = new TopicCollection(5).AddSingleLevelWildcard();

        collection.Levels.ShouldBe(1);

        collection.IsAppendingAllowed.ShouldBeTrue();
    }

    /// <summary>
    /// Ensure that a blank topic can not be added
    /// </summary>
    [Fact]
    public void AddTopic_OnBlankTopic()
    {
        var collection = new TopicCollection(5);

        var appendingEmptyTopic = () => collection.AddTopic(string.Empty);

        appendingEmptyTopic.ShouldThrow<EmptyTopicException>();
    }

    /// <summary>
    /// Ensure that a multi-level wildcard manually added has the same effects as the
    /// method to perform the same addition
    /// </summary>
    [Fact]
    public void AddTopic_OnMultiLevelWildcard()
    {
        ITopicCollection collection = new TopicCollection(5);

        collection = collection.AddTopic(Mqtt.Wildcard.MultiLevel.ToString());

        collection.Levels.ShouldBe(1);
        collection.IsAppendingAllowed.ShouldBeFalse();
    }

    /// <summary>
    /// Ensure that a single-level wildcard manually added has the same effects as the
    /// method to perform the same addition
    /// </summary>
    [Fact]
    public void AddTopic_OnSingleLevelWildcard()
    {
        ITopicCollection collection = new TopicCollection(5);

        collection = collection.AddTopic(Mqtt.Wildcard.SingleLevel.ToString());

        collection.Levels.ShouldBe(1);
        collection.IsAppendingAllowed.ShouldBeTrue();
    }

    /// <summary>
    /// Ensure that the appending of a topic separator is forbidden
    /// </summary>
    [Fact]
    public void AddTopic_OnTopicSeparator()
    {
        var collection = new TopicCollection(5);

        var appendingTopic = () => collection.AddTopic(Mqtt.Topic.Separator.ToString());

        appendingTopic.ShouldThrow<InvalidTopicException>();
    }

    /// <summary>
    /// Ensure that the regular behavior is valid
    /// </summary>
    [Fact]
    public void AddTopic_OnValidTopic()
    {
        var collection = new TopicCollection(5)
            .AddTopic("sensors")
            .AddTopic("bedroom")
            .AddTopic("temperature");

        collection.Levels.ShouldBe(3);
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
            "temperature"
        ];

        var addTopicsWithAMultiLevelWildcard = () => new TopicCollection(3).AddTopics(topics);

        addTopicsWithAMultiLevelWildcard.ShouldThrow<IllegalTopicConstructionException>();
    }

    /// <summary>
    /// Ensure that multiple topics can successfully be added at once
    /// </summary>
    [Fact]
    public void AddTopics_OnValidTopics()
    {
        string[] topics = [
            "sensors",
            "bedroom",
            "temperature"
        ];

        var collection = new TopicCollection(3).AddTopics(topics);

        collection.Levels.ShouldBe(topics.Length);
        collection.ToArray().ShouldBeEquivalentTo(topics);
    }

    /// <summary>
    /// Ensure that the clone is successful
    /// </summary>
    [Fact]
    public void Clone()
    {
        var initial = new TopicCollection(3);

        var clone = initial.Clone();

        clone.MaxLevel.ShouldBe(initial.MaxLevel);
        clone.Levels.ShouldBe(initial.Levels);
        clone.ShouldBeEquivalentTo(initial);
    }

    /// <summary>
    /// Ensure that the maximum level is successfully set
    /// </summary>
    [Fact]
    public void TopicCollection_MaxLevel()
    {
        var collection = new TopicCollection(5);
        collection.MaxLevel.ShouldBe(5);
    }
}
