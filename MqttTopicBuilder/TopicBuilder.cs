/**
 * Author
 *      Pierre Bouillon - https://github.com/pBouillon
 *
 * Repository
 *      MqttTopicBuilder - https://github.com/pBouillon/MqttTopicBuilder
 *
 * License
 *      MIT - https://github.com/pBouillon/MqttTopicBuilder/blob/master/LICENSE
 */

namespace MqttTopicBuilder
{
    using MqttUtils;
    using System.Collections;

    /// <summary>
    /// TODO: doc
    /// </summary>
    public class TopicBuilder
    {
        /// <summary>
        /// TODO: doc
        /// </summary>
        public int Level
            => StagedTopics.Count;

        /// <summary>
        /// TODO: doc
        /// </summary>
        public Queue StagedTopics { get; }

        /// <summary>
        /// TODO: doc
        /// </summary>
        // TODO: str initialization
        public TopicBuilder(int maxLength = Topics.MaxLength)
        {
            StagedTopics = new Queue();
        }

        /// <summary>
        /// TODO: doc
        /// </summary>
        /// <returns></returns>
        public TopicBuilder AddTopic(string topic)
        {
            // TODO
            return this;
        }

        /// <summary>
        /// TODO: doc
        /// </summary>
        /// <returns></returns>
        public TopicBuilder AddWildcardSingleLevel()
        {
            // TODO
            return this;
        }

        /// <summary>
        /// TODO: doc
        /// </summary>
        /// <returns></returns>
        public TopicBuilder AddWildcardMultiLevel()
        {
            // TODO
            return this;
        }
    }
}
