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

using MqttTopicBuilder.Core.Constants;
using MqttTopicBuilder.Exceptions.Classes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MqttTopicBuilder.Utils;

namespace MqttTopicBuilder.Collections
{
    /// <inheritdoc cref="ITopicCollection"/>
    internal class TopicCollection : ITopicCollection
    {
        /// <summary>
        /// Inner-collection on which relies the topics appending
        /// </summary>
        private readonly Queue<string> _stagedTopics;

        /// <inheritdoc cref="ITopicCollection.IsAppendingAllowed"/>
        public bool IsAppendingAllowed
            => _stagedTopics.Last() != Wildcards.MultiLevel;

        /// <inheritdoc cref="ITopicCollection.IsEmpty"/>
        public bool IsEmpty
            => _stagedTopics.Count == 0;

        /// <inheritdoc cref="ITopicCollection.Levels"/>
        public int Levels
            => _stagedTopics.Count;

        /// <inheritdoc cref="ITopicCollection.MaxLevel"/>
        public int MaxLevel { get; }

        /// <summary>
        /// Create a new collection with a predefined maximum level
        /// </summary>
        /// <param name="maxLevel"></param>
        /// <remarks>
        /// By default, <paramref name="maxLevel"/> has the maximum allowed
        /// value <see cref="Topics.MaximumAllowedLevels"/>
        /// </remarks>
        public TopicCollection(int maxLevel = Topics.MaximumAllowedLevels)
            => _stagedTopics = new Queue<string>(maxLevel);

        /// <summary>
        /// Private constructor to create a copy of the current collection
        /// </summary>
        /// <param name="stagedTopics">Collection of staged topics to be copied</param>
        /// <param name="maxLevel">Collection of staged topics to be copied</param>
        /// <remarks>
        /// Since this is only a copy, no "overflow" (stagedTopics.Count > MaxLength) should
        /// occurs
        /// </remarks>
        private TopicCollection(Queue<string> stagedTopics, int maxLevel)
            => (_stagedTopics, MaxLevel) = (stagedTopics, maxLevel);

        /// <inheritdoc cref="ITopicCollection.AddMultiLevelWildcard"/>
        public ITopicCollection AddMultiLevelWildcard()
            => AddTopic(Wildcards.MultiLevel);

        /// <inheritdoc cref="ITopicCollection.AddSingleLevelWildcard"/>
        public ITopicCollection AddSingleLevelWildcard()
            => AddTopic(Wildcards.SingleLevel);

        /// <inheritdoc cref="ITopicCollection.AddTopic"/>
        public ITopicCollection AddTopic(string topic)
        {
            if (!IsAppendingAllowed)
            {
                throw new IllegalTopicConstructionException();
            }

            if (Levels + 1 >= MaxLevel)
            {
                throw new TooManyTopicsAppendingException();
            }

            topic.ValidateTopic();

            // Creating a copy of the staged topics to add the new one
            // (keeping the object "immutable")
            var newStaged = new Queue<string>(_stagedTopics);
            newStaged.Enqueue(topic);

            // Return the new instance of the collection with the new topics collection
            return new TopicCollection(newStaged, MaxLevel);
        }

        /// <summary>
        /// Return an enumerator that iterates through the <see cref="TopicCollection"/>
        /// </summary>
        /// <returns>
        /// A <see cref="Queue{T}.Enumerator"/>
        /// </returns>
        public IEnumerator<string> GetEnumerator()
            => _stagedTopics.GetEnumerator();

        /// <summary>
        /// Return an enumerator that iterates through the <see cref="TopicCollection"/>
        /// </summary>
        /// <returns>
        /// A <see cref="Queue{T}.Enumerator"/>
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <inheritdoc cref="ITopicCollection.ToArray"/>
        public string[] ToArray()
            => _stagedTopics.ToArray();

        /// <inheritdoc cref="ITopicCollection.ToList"/>
        public List<string> ToList()
            => _stagedTopics.ToList();
    }
}
