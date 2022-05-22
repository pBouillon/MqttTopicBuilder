using System.Collections.Generic;

namespace MqttTopicBuilder.Collection
{
    /// <summary>
    /// Represent a custom topics collection
    /// </summary>
    /// <remarks>
    /// Most operation are returning a new instance instead of updating
    /// the current one
    /// </remarks>
    public interface ITopicCollection : IEnumerable<string>
    {
        /// <summary>
        /// Check if it is possible to add a topic to the collection
        /// </summary>
        bool IsAppendingAllowed { get; }

        /// <summary>
        /// Check if there is any staged topics
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Count all topics added
        /// </summary>
        int Levels { get; }

        /// <summary>
        /// Maximum levels allowed for the final topic
        /// </summary>
        int MaxLevel { get; }

        /// <summary>
        /// Add a multi-level wildcard to the staged topics
        /// </summary>
        /// <returns>
        /// A new instance of <see cref="ITopicCollection"/> holding the newly
        /// added wildcard
        /// </returns>
        ITopicCollection AddMultiLevelWildcard();

        /// <summary>
        /// Add a single-level wildcard to the staged topics
        /// </summary>
        /// <returns>
        /// A new instance of <see cref="ITopicCollection"/> holding the newly
        /// added wildcard
        /// </returns>
        ITopicCollection AddSingleLevelWildcard();

        /// <summary>
        /// Add a topic to the staged ones
        /// </summary>
        /// <param name="topic">Topic to be added</param>
        /// <returns>
        /// A new instance of <see cref="ITopicCollection"/> holding the newly
        /// added topic
        /// </returns>
        ITopicCollection AddTopic(string topic);

        /// <summary>
        /// Add several topics to the staged ones
        /// </summary>
        /// <param name="topics">Topics to be added</param>
        /// <returns>
        /// A new instance of <see cref="ITopicCollection"/> holding the newly
        /// added topics
        /// </returns>
        ITopicCollection AddTopics(IEnumerable<string> topics);

        /// <summary>
        /// Clone the current topic collection
        /// </summary>
        /// <returns>A clone of the current instance</returns>
        ITopicCollection Clone();

        /// <summary>
        /// Retrieve all the staged topics in an array
        /// </summary>
        /// <returns>A string array of all the topics</returns>
        string[] ToArray();

        /// <summary>
        /// Retrieve the currently staged topics as a list
        /// </summary>
        /// <returns>A list of strings of all the topics</returns>
        List<string> ToList();
    }
}
