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
using System.Linq;
using MqttTopicBuilder.Core;
using MqttTopicBuilder.Core.Constants;

namespace MqttTopicBuilder.Builder
{
    /// <summary>
    /// Implements a builder to construct topics
    /// </summary>
    public class TopicBuilder : ITopicBuilder
    {
        /// <summary>
        /// Check if the appending is forbidden
        /// Appending is locked if the queue is not empty
        /// And if the last element is not a multi-level wildcard
        /// </summary>
        private bool IsAppendingForbidden
            => !IsEmpty 
            && StagedTopics
                .Last()
                .Equals(Wildcards.MultiLevel.ToString());

        /// <inheritdoc cref="ITopicBuilder.IsEmpty"/>
        public bool IsEmpty
            => StagedTopics.Count == 0;

        /// <inheritdoc cref="ITopicBuilder.Level"/>
        public int Level
            => StagedTopics.Count;

        /// <inheritdoc cref="ITopicBuilder.MaxDepth"/>
        public int MaxDepth { get; set; }

        /// <inheritdoc cref="ITopicBuilder.StagedTopics"/>
        public Queue<string> StagedTopics { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="maxDepth">Maximum topics to add allowed</param>
        public TopicBuilder(int maxDepth = Topics.MaximumAllowedDepth)
        {
            StagedTopics = new Queue<string>(maxDepth);
            MaxDepth = maxDepth;
        }

        /// <summary>
        /// Constructor to initialize the builder with an existing topic
        /// </summary>
        /// <param name="topicBase">The existing topic to add to the staged ones</param>
        /// <param name="maxDepth">Maximum topics to add allowed</param>
        public TopicBuilder(string topicBase, int maxDepth = Topics.MaximumAllowedDepth)
            : this(maxDepth)
        {
            var slices = topicBase.Split(Topics.Separator);

            if (slices.Count() > MaxDepth)
            {
                MaxDepth = slices.Count();
            }

            foreach (var slice in slices)
            {
                AddTopic(slice);
            }
        }

        /// <summary>
        /// Constructor to initialize the builder with a collection of existing topics
        /// </summary>
        /// <param name="topicsBase">The existing collection of topics to add to the staged ones</param>
        /// <param name="maxDepth">Maximum topics to add allowed</param>
        public TopicBuilder(IEnumerable<string> topicsBase, int maxDepth = Topics.MaximumAllowedDepth)
            : this(maxDepth)
        {
            var slices = topicsBase as string[] 
                         ?? topicsBase.ToArray();

            if (slices.Count() > MaxDepth)
            {
                MaxDepth = slices.Count();
            }

            foreach (var slice in slices)
            {
                AddTopic(slice);
            }
        }

        /// <inheritdoc cref="ITopicBuilder.AddTopic"/>
        public ITopicBuilder AddTopic(string topic)
        {
            CheckAppendingAllowance();

            // A topic can't be blank
            if (string.IsNullOrEmpty(topic)
                || string.IsNullOrWhiteSpace(topic)
                || topic.Contains(Topics.NullCharacter))
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

        /// <inheritdoc cref="ITopicBuilder.AddWildcardSingleLevel"/>
        public ITopicBuilder AddWildcardSingleLevel()
        {
            CheckAppendingAllowance();

            StagedTopics.Enqueue(
                Wildcards.SingleLevel.ToString());

            return this;
        }

        /// <inheritdoc cref="ITopicBuilder.AddWildcardMultiLevel"/>
        public ITopicBuilder AddWildcardMultiLevel()
        {
            CheckAppendingAllowance();

            StagedTopics.Enqueue(
                Wildcards.MultiLevel.ToString());

            return this;
        }

        /// <summary>
        /// Build the topic from the staged ones
        /// </summary>
        /// <returns>The newly formed topic as a Topic object</returns>
        /// <see cref="Topic"/>
        public Topic Build()
        {
            // An empty builder will result in the construction of the smallest topic possible ('/')
            var generatedTopic = IsEmpty
                ? Topics.Separator.ToString()
                : string.Join(Topics.Separator, StagedTopics);

            return new Topic(generatedTopic);
        }

        /// <summary>
        /// Check the ability for the user to add a new topic
        /// </summary>
        private void CheckAppendingAllowance()
        {
            // Check if the appending is allowed
            if (IsAppendingForbidden)
            {
                throw new IllegalTopicConstructionException();
            }

            // Check if the depth limit is reached
            if (StagedTopics.Count == MaxDepth)
            {
                throw new TopicBuilderOverflowException();
            }
        }

        /// <summary>
        /// Removes all staged topics
        /// </summary>
        public void Clear()
        {
            StagedTopics.Clear();
        }
    }
}
