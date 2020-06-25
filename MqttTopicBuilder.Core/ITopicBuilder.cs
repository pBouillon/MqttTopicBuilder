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
using MqttTopicBuilder.Exception.Classes;

namespace MqttTopicBuilder.Core
{
    public interface ITopicBuilder
    {
        /// <summary>
        /// Check if there is any staged topics
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Count all topics added
        /// </summary>
        int Level { get; }

        /// <summary>
        /// Maximum depth of the topic to be build
        /// </summary>
        int MaxDepth { get; set; }

        /// <summary>
        /// The staged topics who will result in the topic's path
        /// </summary>
        Queue<string> StagedTopics { get; }

        /// <summary>
        /// Add a topic to the staged ones
        /// </summary>
        /// <param name="topic">Topic to add</param>
        /// <exception cref="EmptyTopicException">Raised if the topic is blank or empty</exception>
        /// <exception cref="InvalidTopicException">Raised if the topic is malformed</exception>
        /// <returns>The builder itself (Fluent pattern)</returns>
        ITopicBuilder AddTopic(string topic);

        /// <summary>
        /// Add a single-level wildcard to the staged topics
        /// </summary>
        /// <returns>The builder itself (Fluent pattern)</returns>
        ITopicBuilder AddWildcardSingleLevel();

        /// <summary>
        /// Add a multi-level wildcard to the staged topics
        /// </summary>
        /// <returns>The builder itself (Fluent pattern)</returns>
        ITopicBuilder AddWildcardMultiLevel();
    }
}
