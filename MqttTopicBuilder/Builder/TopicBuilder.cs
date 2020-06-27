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

using MqttTopicBuilder.Collection;
using MqttTopicBuilder.Constants;
using System.Collections.Generic;

namespace MqttTopicBuilder.Builder
{
    /// <summary>
    /// Immutable builder to build <see cref="Topic.Topic"/>
    /// </summary>
    public class TopicBuilder : ITopicBuilder
    {
        /// <summary>
        /// Inner-collection on which relies the builder for topic creation
        /// </summary>
        private readonly ITopicCollection _topicCollection;

        /// <inheritdoc cref="ITopicBuilder.IsAppendingAllowed"/>
        public bool IsAppendingAllowed
            => _topicCollection.IsAppendingAllowed;

        /// <inheritdoc cref="ITopicBuilder.IsEmpty"/>
        public bool IsEmpty
            => _topicCollection.IsEmpty;

        /// <inheritdoc cref="ITopicBuilder.MaxLevel"/>
        public int MaxLevel
            => _topicCollection.MaxLevel;

        public TopicBuilder()
            => _topicCollection = new TopicCollection(Mqtt.Topic.MaximumAllowedLevels);

        public TopicBuilder(int maxLevel)
            => _topicCollection = new TopicCollection(maxLevel);

        private TopicBuilder(ITopicCollection topicCollection)
            => _topicCollection = topicCollection;

        /// <inheritdoc cref="ITopicBuilder.AddMultiLevelWildcard"/>
        public ITopicBuilder AddMultiLevelWildcard()
            => new TopicBuilder(_topicCollection.AddMultiLevelWildcard());

        /// <inheritdoc cref="ITopicBuilder.AddTopic"/>
        public ITopicBuilder AddTopic(string topic)
            => new TopicBuilder(_topicCollection.AddTopic(topic));

        /// <inheritdoc cref="ITopicBuilder.AddTopics(IEnumerable&lt;string&gt;)"/>
        public ITopicBuilder AddTopics(IEnumerable<string> topics)
            => new TopicBuilder(_topicCollection.AddTopics(topics));

        /// <inheritdoc cref="ITopicBuilder.AddTopics(string[])"/>
        public ITopicBuilder AddTopics(params string[] topics)
            => AddTopics(topics as IEnumerable<string>);

        /// <inheritdoc cref="ITopicBuilder.AddSingleLevelWildcard"/>
        public ITopicBuilder AddSingleLevelWildcard()
            => new TopicBuilder(_topicCollection.AddSingleLevelWildcard());

        /// <inheritdoc cref="ITopicBuilder.Build"/>
        public Topic.Topic Build()
            => new Topic.Topic(
                string.Join(
                        Mqtt.Topic.Separator.ToString(), _topicCollection.ToArray()));

        /// <inheritdoc cref="ITopicBuilder.Clone"/>
        public ITopicBuilder Clone()
            => new TopicBuilder(_topicCollection);
    }
}
