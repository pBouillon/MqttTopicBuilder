using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Validators;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

// Allow unit test project to reach this class
[assembly: InternalsVisibleTo("MqttTopicBuilder.UnitTests")]

namespace MqttTopicBuilder.Collection;

/// <summary>
/// Immutable implementation of an <see cref="ITopicCollection"/>
/// </summary>
/// <inheritdoc cref="ITopicCollection"/>
internal class TopicCollection : ITopicCollection
{
    /// <summary>
    /// Inner-collection on which relies the topics appending
    /// </summary>
    private readonly Queue<string> _stagedTopics;

    /// <inheritdoc cref="ITopicCollection.IsAppendingAllowed"/>
    public bool IsAppendingAllowed
        => IsEmpty
           || _stagedTopics.Last() != Mqtt.Wildcard.MultiLevel.ToString();

    /// <inheritdoc cref="ITopicCollection.IsEmpty"/>
    public bool IsEmpty
        => _stagedTopics.Count == 0;

    /// <inheritdoc cref="ITopicCollection.Levels"/>
    public int Levels
        => _stagedTopics.Count;

    /// <inheritdoc cref="ITopicCollection.MaxLevel"/>
    public int MaxLevel { get; }

    /// <summary>
    /// Create a new collection with a predefined maximum level
    /// </summary>
    /// <param name="maxLevel">Maximum number of items allowed in the collection</param>
    public TopicCollection(int maxLevel)
        => (_stagedTopics, MaxLevel) = (new Queue<string>(maxLevel), maxLevel);

    /// <summary>
    /// Private constructor to create a copy of the current collection
    /// </summary>
    /// <param name="stagedTopics">Collection of staged topics to be copied</param>
    /// <param name="maxLevel">Collection of staged topics to be copied</param>
    /// <remarks>
    /// Since this is only a copy, no "overflow" (stagedTopics.Count > MaxLength) should
    /// occurs
    /// </remarks>
    private TopicCollection(Queue<string> stagedTopics, int maxLevel)
        => (_stagedTopics, MaxLevel) = (stagedTopics, maxLevel);

    /// <inheritdoc cref="ITopicCollection.AddMultiLevelWildcard"/>
    public ITopicCollection AddMultiLevelWildcard()
        => AddTopic(Mqtt.Wildcard.MultiLevel.ToString());

    /// <inheritdoc cref="ITopicCollection.AddSingleLevelWildcard"/>
    public ITopicCollection AddSingleLevelWildcard()
        => AddTopic(Mqtt.Wildcard.SingleLevel.ToString());

    /// <inheritdoc cref="ITopicCollection.AddTopic"/>
    public ITopicCollection AddTopic(string topic)
    {
        CheckAppendingAllowanceFor(topic);

        // Creating a copy of the staged topics to add the new one
        // (keeping the object "immutable")
        var newStaged = new Queue<string>(_stagedTopics);
        newStaged.Enqueue(topic);

        // Return the new instance of the collection with the new topics collection
        return new TopicCollection(newStaged, MaxLevel);
    }

    /// <inheritdoc cref="ITopicCollection.AddTopics"/>
    public ITopicCollection AddTopics(IEnumerable<string> topics)
        => topics.Aggregate(
            (ITopicCollection) this, (current, topic) => 
                current.AddTopic(topic));

    /// <summary>
    /// Check whether it is possible or not to append the provided topic to the current collection.
    /// </summary>
    /// <param name="topic">Topic to be appended</param>
    private void CheckAppendingAllowanceFor(string topic)
    {
        ValidatorFactory.GetTopicCollectionAppendingValidator()
            .Validate(this);

        topic.ValidateTopicForAppending();
    }

    /// <inheritdoc cref="ITopicCollection.Clone"/>
    public ITopicCollection Clone()
        => new TopicCollection(_stagedTopics, MaxLevel);

    /// <summary>
    /// Return an enumerator that iterates through the <see cref="TopicCollection"/>
    /// </summary>
    /// <returns>
    /// A <see cref="Queue{T}.Enumerator"/>
    /// </returns>
    public IEnumerator<string> GetEnumerator()
        => _stagedTopics.GetEnumerator();

    /// <summary>
    /// Return an enumerator that iterates through the <see cref="TopicCollection"/>
    /// </summary>
    /// <returns>
    /// A <see cref="Queue{T}.Enumerator"/>
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    /// <inheritdoc cref="ITopicCollection.ToArray"/>
    public string[] ToArray()
        => _stagedTopics.ToArray();

    /// <inheritdoc cref="ITopicCollection.ToList"/>
    public List<string> ToList()
        => _stagedTopics.ToList();
}