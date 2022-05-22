using System.Collections.Generic;

namespace MqttTopicBuilder.Builder.BuilderState;

/// <summary>
/// Abstract implementation of <see cref="IBuilderState"/>
/// </summary>
internal abstract class BuilderState : IBuilderState
{
    /// <summary>
    /// State's context
    /// </summary>
    protected readonly ITopicBuilder TopicBuilder;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="topicBuilder">Context to be used</param>
    protected BuilderState(ITopicBuilder topicBuilder)
        => TopicBuilder = topicBuilder;

    /// <inheritdoc cref="IBuilderState.AddMultiLevelWildcard"/>
    public abstract ITopicBuilder AddMultiLevelWildcard();

    /// <inheritdoc cref="IBuilderState.AddTopic"/>
    public abstract ITopicBuilder AddTopic(string topic);

    /// <inheritdoc cref="IBuilderState.AddTopics(IEnumerable&lt;string&gt;)"/>
    public abstract ITopicBuilder AddTopics(IEnumerable<string> topics);

    /// <inheritdoc cref="IBuilderState.AddTopics(string[])"/>
    public abstract ITopicBuilder AddTopics(params string[] topics);

    /// <inheritdoc cref="IBuilderState.AddSingleLevelWildcard"/>
    public abstract ITopicBuilder AddSingleLevelWildcard();
}