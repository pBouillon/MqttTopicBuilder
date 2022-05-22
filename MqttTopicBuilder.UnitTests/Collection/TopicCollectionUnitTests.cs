using AutoFixture;

using FluentAssertions;

using MqttTopicBuilder.Collection;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.UnitTests.Utils;

using System.Collections.Generic;
using MqttTopicBuilder.Exceptions;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Collection;

/// <summary>
/// Unit test suite for <see cref="ITopicCollection"/>
/// </summary>
public class TopicCollectionUnitTests
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
        ITopicCollection collection = new TopicCollection(Fixture.Create<int>());

        collection = collection.AddMultiLevelWildcard();

        collection.Levels
            .Should()
            .Be(1, "because the wildcard consist of one level");

        collection.IsAppendingAllowed
            .Should()
            .BeFalse("because no addition should be allowed after a multi-level wildcard");
    }

    /// <summary>
    /// Ensure that the appending of a single-level wildcard is correctly made
    /// </summary>
    [Fact]
    public void AddSingleLevelWildcard()
    {
        ITopicCollection collection = new TopicCollection(Fixture.Create<int>());

        collection = collection.AddSingleLevelWildcard();

        collection.Levels
            .Should()
            .Be(1, "because the wildcard consist of one level");

        collection.IsAppendingAllowed
            .Should()
            .BeTrue("because a single level wildcard should not block topic appending");
    }

    /// <summary>
    /// Ensure that a blank topic can not be added
    /// </summary>
    [Fact]
    public void AddTopic_OnBlankTopic()
    {
        ITopicCollection collection = new TopicCollection(Fixture.Create<int>());

        var appendingEmptyTopic = () => collection.AddTopic(string.Empty);

        appendingEmptyTopic.Should()
            .Throw<EmptyTopicException>("because an empty topic is not a valid one to be added");
    }

    /// <summary>
    /// Ensure that a multi-level wildcard manually added has the same effects as the
    /// method to perform the same addition
    /// </summary>
    [Fact]
    public void AddTopic_OnMultiLevelWildcard()
    {
        ITopicCollection collection = new TopicCollection(Fixture.Create<int>());

        collection = collection.AddTopic(
            Mqtt.Wildcard.MultiLevel.ToString());

        collection.Levels
            .Should()
            .Be(1, "because the wildcard consist of one level");

        collection.IsAppendingAllowed
            .Should()
            .BeFalse("because no addition should be allowed after a multi-level wildcard");
    }

    /// <summary>
    /// Ensure that a single-level wildcard manually added has the same effects as the
    /// method to perform the same addition
    /// </summary>
    [Fact]
    public void AddTopic_OnSingleLevelWildcard()
    {
        ITopicCollection collection = new TopicCollection(Fixture.Create<int>());

        collection = collection.AddTopic(
            Mqtt.Wildcard.SingleLevel.ToString());

        collection.Levels
            .Should()
            .Be(1, "because the wildcard consist of one level");

        collection.IsAppendingAllowed
            .Should()
            .BeTrue("because a topic appending is allowed after a single-level wildcard");
    }

    /// <summary>
    /// Ensure that the appending of a topic separator is forbidden
    /// </summary>
    [Fact]
    public void AddTopic_OnTopicSeparator()
    {
        ITopicCollection collection = new TopicCollection(Fixture.Create<int>());

        var appendingTopic = () => collection.AddTopic(
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
        ITopicCollection collection = new TopicCollection(addCount + 1);

        for (var i = 0; i < addCount; ++i)
        {
            collection = collection.AddTopic(Fixture.Create<string>());
        }

        collection.Levels
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

        ITopicCollection collection = new TopicCollection(topics.Count + 1);

        var addTopicsWithAMultiLevelWildcard = () => collection.AddTopics(topics);

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
        ITopicCollection collection = new TopicCollection(topics.Length + 1);

        collection = collection.AddTopics(topics);

        collection.Levels
            .Should()
            .Be(topics.Length, "because all topics should have been added");

        collection.ToArray()
            .Should()
            .Contain(topics, "because the same topics as the ones provided should have been added");
    }

    /// <summary>
    /// Ensure that the clone is successful
    /// </summary>
    [Fact]
    public void Clone()
    {
        var initial = Fixture.Create<TopicCollection>();

        var clone = initial.Clone();

        clone.MaxLevel
            .Should()
            .Be(initial.MaxLevel, "because the same max level should have been copied");

        clone.Levels
            .Should()
            .Be(initial.Levels, "because the content level count should also have been cloned");

        clone.ToArray()
            .Should()
            .BeEquivalentTo(initial.ToArray(), "because all elements should also have been clones");
    }

    /// <summary>
    /// Ensure that the clone is not altered when modifying the original copy
    /// </summary>
    [Fact]
    public void Clone_OnAlteredOriginalInstance()
    {
        var initial = Fixture.Create<TopicCollection>();
        var clone = initial.Clone();
        var initialCount = initial.Levels;

        initial.AddTopic(TestUtils.GenerateSingleValidTopic());

        clone.Levels
            .Should()
            .Be(initialCount, "because altering the origin should not alter the cloned instance");
    }

    /// <summary>
    /// Ensure that the maximum level is successfully set
    /// </summary>
    [Fact]
    public void TopicCollection_MaxLevel()
    {
        var maxLevel = Fixture.Create<int>();

        var collection = new TopicCollection(maxLevel);

        collection.MaxLevel
            .Should()
            .Be(maxLevel, "because the provided value should be the upper bound");
    }
}
