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

using System.Collections.Generic;

namespace MqttTopicBuilder.Builder
{
    /// <summary>
    /// Builder for MQTT topic dynamic creation
    /// </summary>
    public interface ITopicBuilder
    {
        bool IsAppendingAllowed { get; }

        bool IsEmpty { get; }

        /// <summary>
        /// Maximum depth of the topic to be build
        /// </summary>
        public int MaxDepth { get; }

        ITopicBuilder AddMultiLevelWildcard();

        ITopicBuilder AddTopic(string topic);

        ITopicBuilder AddTopics(IEnumerable<string> topics);

        ITopicBuilder AddTopics(params string[] topics);

        ITopicBuilder AddSingleLevelWildcard();

        Topic.Topic Build();

        void Clear();

        /// <summary>
        /// Clone the current instance of <see cref="ITopicBuilder"/>
        /// </summary>
        /// <returns>A new instance of <see cref="ITopicBuilder"/></returns>
        ITopicBuilder Clone();
    }
}
