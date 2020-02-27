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
    using Constants;
    using Exceptions;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Implements a builder to construct topics
    /// </summary>
    public class TopicBuilder
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

        /// <summary>
        /// Check if there is any staged topics
        /// </summary>
        public bool IsEmpty
            => StagedTopics.Count == 0;

        /// <summary>
        /// Count all topics added
        /// </summary>
        public int Level
            => StagedTopics.Count;

        /// <summary>
        /// Maximum depth of the topic to be build
        /// </summary>
        public int MaxDepth { get; set; }

        /// <summary>
        /// The staged topics who will result in the topic's path
        /// </summary>
        public Queue<string> StagedTopics { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="maxDepth">Maximum topics to add allowed</param>
        public TopicBuilder(int maxDepth = Topics.MaxDepth)
        {
            StagedTopics = new Queue<string>(maxDepth);
            MaxDepth = maxDepth;
        }

        /// <summary>
        /// Constructor to initialize the builder with an existing topic
        /// </summary>
        /// <param name="topicBase">The existing topic to add to the staged ones</param>
        /// <param name="maxDepth">Maximum topics to add allowed</param>
        public TopicBuilder(string topicBase, int maxDepth = Topics.MaxDepth)
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
        public TopicBuilder(IEnumerable<string> topicsBase, int maxDepth = Topics.MaxDepth)
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

        /// <summary>
        /// Add a topic to the staged ones
        /// </summary>
        /// <param name="topic">Topic to add</param>
        /// <exception cref="EmptyTopicException">Raised if the topic is blank or empty</exception>
        /// <exception cref="InvalidTopicException">Raised if the topic is malformed</exception>
        /// <returns>The builder itself (Fluent pattern)</returns>
        public TopicBuilder AddTopic(string topic)
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

        /// <summary>
        /// Add a single-level wildcard to the staged topics
        /// </summary>
        /// <returns>The builder itself (Fluent pattern)</returns>
        public TopicBuilder AddWildcardSingleLevel()
        {
            CheckAppendingAllowance();

            StagedTopics.Enqueue(
                Wildcards.SingleLevel.ToString());

            return this;
        }

        /// <summary>
        /// Add a multi-level wildcard to the staged topics
        /// </summary>
        /// <returns>The builder itself (Fluent pattern)</returns>
        public TopicBuilder AddWildcardMultiLevel()
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
