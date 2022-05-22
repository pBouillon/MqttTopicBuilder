using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.Validators;

// Allow unit test project to reach this class
[assembly: InternalsVisibleTo("MqttTopicBuilder.UnitTests")]

namespace MqttTopicBuilder.Builder.BuilderState;

/// <summary>
/// Specialized <see cref="IBuilderState"/> for topic construction
/// when the consumer is <see cref="Consumer.Publisher"/>
/// </summary>
internal class PublisherState : BuilderState
{
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="topicBuilder">State context</param>
    public PublisherState(ITopicBuilder topicBuilder) 
        : base(topicBuilder) { }

    /// <inheritdoc cref="BuilderState.AddMultiLevelWildcard"/>
    public override ITopicBuilder AddMultiLevelWildcard()
        => throw new IllegalStateOperationException("Multi-level wildcards are forbidden when publishing");

    /// <inheritdoc cref="BuilderState.AddTopic"/>
    public override ITopicBuilder AddTopic(string topic)
        => AddTopics(topic);
        
    /// <inheritdoc cref="BuilderState.AddTopics(IEnumerable&lt;string&gt;)"/>
    public override ITopicBuilder AddTopics(IEnumerable<string> topics)
    {
        var validator = ValidatorFactory.GetPublishedTopicValidator();
         
        // Enumerate topics to avoid multiple enumerations
        var enumeratedTopics = topics.ToList();

        // Validate each one of the topics
        enumeratedTopics.ForEach(validator.Validate);

        // Append all topics
        var modifiedCollection = TopicBuilder.TopicCollection.AddTopics(enumeratedTopics);
            
        // Return the new instance of the builder with the new topics
        return new TopicBuilder(modifiedCollection, TopicBuilder.Consumer);
    }

    /// <inheritdoc cref="BuilderState.AddTopics(string[])"/>
    public override ITopicBuilder AddTopics(params string[] topics)
        => AddTopics((IEnumerable<string>) topics);

    /// <inheritdoc cref="BuilderState.AddSingleLevelWildcard"/>
    public override ITopicBuilder AddSingleLevelWildcard()
        => throw new IllegalStateOperationException("Single-level wildcards are forbidden when publishing");
}
