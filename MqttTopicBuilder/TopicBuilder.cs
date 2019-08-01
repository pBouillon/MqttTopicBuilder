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
    using System.Collections.Generic;

    /// <summary>
    /// TODO: doc
    /// </summary>
    public class TopicBuilder
    {
        /// <summary>
        /// TODO: doc
        /// </summary>
        private bool isAppendingBlocked
            => StagedTopics
                .Peek()
                .Equals(Wildcards.MultiLevel.ToString());

        /// <summary>
        /// TODO: doc
        /// </summary>
        public int Level
            => StagedTopics.Count;

        /// <summary>
        /// TODO: doc
        /// </summary>
        public Queue<string> StagedTopics { get; }

        /// <summary>
        /// TODO: doc
        /// </summary>
        /// <param name="maxLength"></param>
        // TODO: str initialization
        public TopicBuilder(int maxLength = Topics.MaxLength)
        {
            StagedTopics = new Queue<string>(maxLength);
        }

        /// <summary>
        /// TODO: doc
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        public TopicBuilder AddTopic(string topic)
        {
            CheckAppendingAllowance();

            // Manually adding separators is forbidden
            if (topic.Contains(Topics.Separator))
            {
                // TODO: raise exception
            }

            // TODO: check for patterns as '##', '#+', etc.

            StagedTopics.Enqueue(topic);

            return this;
        }

        /// <summary>
        /// TODO: doc
        /// </summary>
        /// <returns></returns>
        public TopicBuilder AddWildcardSingleLevel()
        {
            CheckAppendingAllowance();

            StagedTopics.Enqueue(
                Wildcards.SingleLevel.ToString());

            return this;
        }

        /// <summary>
        /// TODO: doc
        /// </summary>
        /// <returns></returns>
        public TopicBuilder AddWildcardMultiLevel()
        {
            CheckAppendingAllowance();

            StagedTopics.Enqueue(
                Wildcards.MultiLevel.ToString());

            return this;
        }

        /// <summary>
        /// TODO: doc
        /// </summary>
        /// <returns></returns>
        public Topic Build()
        {
            // An empty builder will result in the construction of the smallest topic possible
            var generatedTopic = StagedTopics.Count == 0
                ? Topics.Separator.ToString()
                : string.Join(Topics.Separator, StagedTopics);

            return new Topic(generatedTopic);
        }

        /// <summary>
        /// TODO: doc
        /// </summary>
        private void CheckAppendingAllowance()
        {
            if (!isAppendingBlocked)
            {
                // TODO: raise exception
            }
        }
    }
}
