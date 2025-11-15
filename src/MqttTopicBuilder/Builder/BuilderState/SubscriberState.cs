using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Allow unit test project to reach this class
[assembly: InternalsVisibleTo("MqttTopicBuilder.UnitTests")]

namespace MqttTopicBuilder.Builder.BuilderState;

/// <summary>
/// Specialized <see cref="IBuilderState"/> for topic construction
/// when the consumer is <see cref="Consumer.Subscriber"/>
/// </summary>
internal class SubscriberState : BuilderState
{
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="topicBuilder">State context</param>
    public SubscriberState(ITopicBuilder topicBuilder) 
        : base(topicBuilder) { }

    /// <inheritdoc cref="BuilderState.AddMultiLevelWildcard"/>
    public override ITopicBuilder AddMultiLevelWildcard()
    {
        var modifiedCollection = TopicBuilder.TopicCollection.AddMultiLevelWildcard();
        return new TopicBuilder(modifiedCollection, TopicBuilder.Consumer);
    }

    /// <inheritdoc cref="BuilderState.AddTopic"/>
    public override ITopicBuilder AddTopic(string topic)
        => AddTopics(topic);

    /// <inheritdoc cref="BuilderState.AddTopics(IEnumerable&lt;string&gt;)"/>
    public override ITopicBuilder AddTopics(IEnumerable<string> topics)
    {
        var modifiedCollection = TopicBuilder.TopicCollection.AddTopics(topics);
        return new TopicBuilder(modifiedCollection, TopicBuilder.Consumer);
    }

    /// <inheritdoc cref="BuilderState.AddTopics(string[])"/>
    public override ITopicBuilder AddTopics(params string[] topics)
        => AddTopics((IEnumerable<string>) topics);

    /// <inheritdoc cref="BuilderState.AddSingleLevelWildcard"/>
    public override ITopicBuilder AddSingleLevelWildcard()
    {
        var modifiedCollection = TopicBuilder.TopicCollection.AddSingleLevelWildcard();
        return new TopicBuilder(modifiedCollection, TopicBuilder.Consumer);
    }
}
