using System.Collections.Generic;

using MqttTopicBuilder.Builder.BuilderState;
using MqttTopicBuilder.Collection;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Validators;

namespace MqttTopicBuilder.Builder;

/// <summary>
/// Immutable builder to build <see cref="Topic"/>
/// </summary>
public class TopicBuilder : ITopicBuilder
{
    /// <inheritdoc cref="ITopicBuilder.IsAppendingAllowed"/>
    public bool IsAppendingAllowed
        => TopicCollection.IsAppendingAllowed;

    /// <inheritdoc cref="ITopicBuilder.Consumer"/>
    public Consumer Consumer { get; }

    /// <inheritdoc cref="ITopicBuilder.IsEmpty"/>
    public bool IsEmpty
        => TopicCollection.IsEmpty;

    /// <inheritdoc cref="ITopicBuilder.Levels"/>
    public int Levels
        => TopicCollection.Levels;

    /// <inheritdoc cref="ITopicBuilder.MaxLevel"/>
    public int MaxLevel
        => TopicCollection.MaxLevel;

    /// <inheritdoc cref="ITopicBuilder.TopicCollection"/>
    public ITopicCollection TopicCollection { get; }

    /// <summary>
    /// Inner <see cref="IBuilderState"/> of the builder
    /// </summary>
    private readonly IBuilderState _state;

    /// <summary>
    /// Create a new <see cref="ITopicCollection"/>
    /// </summary>
    /// <param name="consumer">Context where this topic will be consumed</param>
    /// <remarks>
    /// The maximum capacity will be <see cref="Mqtt.Topic.MaximumAllowedLevels"/>
    /// </remarks>
    public TopicBuilder(Consumer consumer)
        : this(Mqtt.Topic.MaximumAllowedLevels, consumer) { }

    /// <summary>
    /// Create a new <see cref="ITopicCollection"/> with a maximum capacity
    /// </summary>
    /// <param name="maxLevel">Maximum number of topics that the collection can contains</param>
    /// <param name="consumer">Context where this topic will be consumed</param>
    public TopicBuilder(int maxLevel, Consumer consumer)
        : this(new TopicCollection(maxLevel), consumer) { }

    /// <summary>
    /// Create a new instance of <see cref="ITopicCollection"/> from an existing one
    /// </summary>
    /// <param name="topicCollection">Existing collection, seeding this one</param>
    /// <param name="consumer">Context where this topic will be consumed</param>
    public TopicBuilder(ITopicCollection topicCollection, Consumer consumer)
    {
        TopicCollection = topicCollection;
        Consumer = consumer;

        _state = Consumer == Consumer.Publisher
            ? new PublisherState(this)
            : new SubscriberState(this);

        // ReSharper disable once InvertIf
        if (Consumer == Consumer.Publisher)
        {
            var validator = ValidatorFactory.GetPublishedTopicValidator();
            TopicCollection.ToList()
                .ForEach(validator.Validate);
        }
    }

    /// <inheritdoc cref="ITopicBuilder.AddMultiLevelWildcard"/>
    public ITopicBuilder AddMultiLevelWildcard()
        => _state.AddMultiLevelWildcard();

    /// <inheritdoc cref="ITopicBuilder.AddTopic"/>
    public ITopicBuilder AddTopic(string topic)
        => _state.AddTopic(topic);

    /// <inheritdoc cref="ITopicBuilder.AddTopics(IEnumerable&lt;string&gt;)"/>
    public ITopicBuilder AddTopics(IEnumerable<string> topics)
        => _state.AddTopics(topics);

    /// <inheritdoc cref="ITopicBuilder.AddTopics(string[])"/>
    public ITopicBuilder AddTopics(params string[] topics)
        => _state.AddTopics(topics);

    /// <inheritdoc cref="ITopicBuilder.AddSingleLevelWildcard"/>
    public ITopicBuilder AddSingleLevelWildcard()
        => _state.AddSingleLevelWildcard();

    /// <inheritdoc cref="ITopicBuilder.Build"/>
    public Topic Build()
    {
        var parts = TopicCollection.ToArray();
        var topic = string.Join(Mqtt.Topic.Separator.ToString(), parts);

        return new Topic(topic);
    }

    /// <inheritdoc cref="ITopicBuilder.Clear"/>
    public ITopicBuilder Clear()
        => new TopicBuilder(MaxLevel, Consumer);

    /// <inheritdoc cref="ITopicBuilder.Clone"/>
    public ITopicBuilder Clone()
        => new TopicBuilder(TopicCollection, Consumer);
    
    /// <summary>
    /// Create a new <see cref="ITopicBuilder"/> from an existing topic
    /// </summary>
    /// <param name="topic">
    /// <see cref="Topic"/> used for seeding the new <see cref="ITopicBuilder"/> instance
    /// </param>
    /// <param name="consumer">Context where this topic will be consumed</param>
    /// <returns>A new <see cref="ITopicBuilder"/> instance seeded with the provided <see cref="Topic"/></returns>
    public static ITopicBuilder FromTopic(Topic topic, Consumer consumer)
        // Adding topics *after* having set the Consumer property will ensure that no illegal topics has
        // been added in the builder
        => new TopicBuilder(topic.Levels, consumer).AddTopics(topic.ToArray());
}
