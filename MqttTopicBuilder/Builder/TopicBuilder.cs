/*
 * Author
 *      Pierre Bouillon - https://github.com/pBouillon
 *
 * Repository
 *      MqttTopicBuilder - https://github.com/pBouillon/MqttTopicBuilder
 *
 * License
 *      MIT - https://github.com/pBouillon/MqttTopicBuilder/blob/master/LICENSE
 */

using MqttTopicBuilder.Builder.BuilderState;
using MqttTopicBuilder.Collection;
using MqttTopicBuilder.Constants;
using System.Collections.Generic;

namespace MqttTopicBuilder.Builder
{
    /// <summary>
    /// Immutable builder to build <see cref="Topic"/>
    /// </summary>
    public class TopicBuilder : ITopicBuilder
    {
        /// <inheritdoc cref="ITopicBuilder.IsAppendingAllowed"/>
        public bool IsAppendingAllowed
            => TopicCollection.IsAppendingAllowed;

        /// <inheritdoc cref="ITopicBuilder.Consumer"/>
        public TopicConsumer Consumer { get; }

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
        /// <param name="topicConsumer">Context where this topic will be consumed</param>
        /// <remarks>
        /// The maximum capacity will be <see cref="Mqtt.Topic.MaximumAllowedLevels"/>
        /// </remarks>
        public TopicBuilder(TopicConsumer topicConsumer)
            : this(Mqtt.Topic.MaximumAllowedLevels, topicConsumer) { }

        /// <summary>
        /// Create a new <see cref="ITopicCollection"/> with a maximum capacity
        /// </summary>
        /// <param name="maxLevel">Maximum number of topics that the collection can contains</param>
        /// <param name="topicConsumer">Context where this topic will be consumed</param>
        public TopicBuilder(int maxLevel, TopicConsumer topicConsumer)
            : this(new TopicCollection(maxLevel), topicConsumer) { }

        /// <summary>
        /// Create a new instance of <see cref="ITopicCollection"/> from an existing one
        /// </summary>
        /// <param name="topicCollection">Existing collection, seeding this one</param>
        /// <param name="topicConsumer">Context where this topic will be consumed</param>
        public TopicBuilder(ITopicCollection topicCollection, TopicConsumer topicConsumer)
        {
            TopicCollection = topicCollection;
            
            Consumer = topicConsumer;

            _state = topicConsumer == TopicConsumer.Publisher
                ? (IBuilderState) new PublisherState(this)
                : new SubscriberState(this);
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
            => new Topic(
                string.Join(
                        Mqtt.Topic.Separator.ToString(), TopicCollection.ToArray()));

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
        /// <param name="topicConsumer">Context where this topic will be consumed</param>
        /// <returns>A new <see cref="ITopicBuilder"/> instance seeded with the provided <see cref="Topic"/></returns>
        public static ITopicBuilder FromTopic(Topic topic, TopicConsumer topicConsumer)
            => new TopicBuilder(topic.Levels, topicConsumer)
                // Adding topics *after* having set the TopicConsumer property will ensure that no illegal topics has
                // been added in the builder
                .AddTopics(topic.ToArray());
    }
}
