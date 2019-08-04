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
    using Exceptions;
    using MqttUtils;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// TODO: doc
    /// </summary>
    public class TopicBuilder
    {
        /// <summary>
        /// TODO: doc
        /// </summary>
        private bool IsAppendingForbidden
            => !IsEmpty 
            && StagedTopics
                .()
                .Equals(Wildcards.MultiLevel.ToString());

        /// <summary>
        /// TODO: doc
        /// </summary>
        public bool IsEmpty
            => StagedTopics.Count == 0;

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
        public TopicBuilder(int maxLength = Topics.MaxLength)
        {
            StagedTopics = new Queue<string>(maxLength);
        }

        /// <summary>
        /// TODO: doc
        /// </summary>
        /// <param name="topicBase"></param>
        /// <param name="maxLength"></param>
        public TopicBuilder(string topicBase, int maxLength = Topics.MaxLength)
            : this(maxLength)
        {
            foreach (var slice in topicBase.Split(Topics.Separator))
            {
                AddTopic(slice);
            }
        }

        /// <summary>
        /// TODO: doc
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        public TopicBuilder AddTopic(string topic)
        {
            CheckAppendingAllowance();

            // A topic can't be blank
            if (string.IsNullOrEmpty(topic)
                || string.IsNullOrWhiteSpace(topic))
            {
                throw new EmptyTopicException();
            }

            // Manually adding separators is forbidden
            if (topic.Contains(Topics.Separator))
            {
                throw new InvalidTopicException("A topic should not contains a separator");
            }

            // Wildcard must only be used to denote a level and 
            // shouldn't be used to denote multiple characters
            if (topic.Length > 1 
                && (topic.Contains(Wildcards.MultiLevel)
                || topic.Contains(Wildcards.SingleLevel)))
            {
                throw new InvalidTopicException("A topic should not contains a wildcard in its name");
            }

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
            var generatedTopic = IsEmpty
                ? Topics.Separator.ToString()
                : string.Join(Topics.Separator, StagedTopics);

            return new Topic(generatedTopic);
        }

        /// <summary>
        /// TODO: doc
        /// </summary>
        private void CheckAppendingAllowance()
        {
            if (IsAppendingForbidden)
            {
                throw new IllegalTopicConstructionException();
            }
        }
    }
}
