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
using System.Linq;

namespace MqttTopicBuilder.Builder
{
    /// <summary>
    /// Immutable builder to build <see cref="Topic.Topic"/>
    /// </summary>
    public class TopicBuilder : ITopicBuilder
    {
        private readonly ITopicCollection _topicCollection;

        public bool IsAppendingAllowed
            => _topicCollection.IsAppendingAllowed;

        public bool IsEmpty
            => _topicCollection.IsEmpty;

        public int MaxDepth { get; }

        public TopicBuilder()
            => (MaxDepth, _topicCollection)
                = (Mqtt.Topic.MaximumAllowedLevels, new TopicCollection(MaxDepth));

        public TopicBuilder(int maxDepth)
            => (MaxDepth, _topicCollection)
                = (maxDepth, new TopicCollection(MaxDepth));

        private TopicBuilder(int maxDepth, ITopicCollection topicCollection)
            => (MaxDepth, _topicCollection) = (maxDepth, topicCollection);

        public ITopicBuilder AddMultiLevelWildcard()
        {
            // Check max-level

            return new TopicBuilder(MaxDepth, _topicCollection.AddMultiLevelWildcard());
        }

        public ITopicBuilder AddTopic(string topic)
        {
            // Check max-level

            return new TopicBuilder(MaxDepth, _topicCollection.AddTopic(topic));
        }

        public ITopicBuilder AddTopics(IEnumerable<string> topics)
        {
            // Check max-level

            return new TopicBuilder(MaxDepth, _topicCollection.AddTopics(topics));
        }

        public ITopicBuilder AddTopics(params string[] topics)
            => AddTopics(topics as IEnumerable<string>);

        public ITopicBuilder AddSingleLevelWildcard()
        {
            // Check max-level

            return new TopicBuilder(MaxDepth, _topicCollection.AddSingleLevelWildcard());
        }

        /// FIXME: empty topic
        public Topic.Topic Build()
        {
            // FIXME: empty topic
            var content = IsEmpty
                ? Mqtt.Topic.Separator.ToString()
                : string.Join(Mqtt.Topic.Separator, _topicCollection.ToArray());

            return new Topic.Topic(content);
        }

        public void Clear()
            => new TopicBuilder(MaxDepth);

        public ITopicBuilder Clone()
            => new TopicBuilder(MaxDepth, _topicCollection);
    }
}
