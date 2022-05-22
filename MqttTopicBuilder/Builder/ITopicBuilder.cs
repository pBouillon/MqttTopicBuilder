using System.Collections.Generic;

using MqttTopicBuilder.Collection;

namespace MqttTopicBuilder.Builder;

/// <summary>
/// Builder for MQTT topic dynamic creation
/// </summary>
public interface ITopicBuilder
{
    /// <summary>
    /// Indicate whether or not it is allowed to append one more topic to the builder
    /// </summary>
    /// <remarks>
    /// Adding a topic when this property is <c>false</c> will throw an exception
    /// </remarks>
    bool IsAppendingAllowed { get; }

    /// <summary>
    /// Consumer type of the topic to be built (PUBLISH / SUBSCRIBE)
    /// </summary>
    Consumer Consumer { get; }

    /// <summary>
    /// Indicate whether or not the builder is empty
    /// </summary>
    bool IsEmpty { get; }

    /// <summary>
    /// Count all topics added
    /// </summary>
    int Levels { get; }

    /// <summary>
    /// Maximum level of the topic to be built and allowed to be built with the builder
    /// </summary>
    int MaxLevel { get; }

    /// <summary>
    /// Inner-collection on which relies the builder for topic creation
    /// </summary>
    ITopicCollection TopicCollection { get; }

    /// <summary>
    /// Add a multi-level wildcard to the builder
    /// </summary>
    /// <returns>An instance of <see cref="ITopicBuilder"/> holding the appended wildcard</returns>
    ITopicBuilder AddMultiLevelWildcard();

    /// <summary>
    /// Add a topic to the builder
    /// </summary>
    /// <param name="topic">Topic to be added</param>
    /// <returns>An instance of <see cref="ITopicBuilder"/> holding the appended topic</returns>
    ITopicBuilder AddTopic(string topic);

    /// <summary>
    /// Add several topics to the builder
    /// </summary>
    /// <param name="topics">Topics to be added</param>
    /// <returns>An instance of <see cref="ITopicBuilder"/> holding the appended topics</returns>
    ITopicBuilder AddTopics(IEnumerable<string> topics);

    /// <summary>
    /// Add a multi-level wildcard to the builder
    /// </summary>
    /// <param name="topics">Topics to be added</param>
    /// <returns>An instance of <see cref="ITopicBuilder"/> holding the appended topic</returns>
    ITopicBuilder AddTopics(params string[] topics);

    /// <summary>
    /// Add a single-level wildcard to the builder
    /// </summary>
    /// <returns>An instance of <see cref="ITopicBuilder"/> holding the appended wildcard</returns>
    ITopicBuilder AddSingleLevelWildcard();

    /// <summary>
    /// Create the topic from the builder's content
    /// </summary>
    /// <returns>
    /// An instance of <see cref="Topic"/> build based on the <see cref="ITopicBuilder"/> content
    /// </returns>
    Topic Build();

    /// <summary>
    /// Clear the <see cref="ITopicBuilder"/>
    /// </summary>
    /// <returns>
    /// A clean instance of <see cref="ITopicBuilder"/>
    /// </returns>
    ITopicBuilder Clear();

    /// <summary>
    /// Clone the current instance of <see cref="ITopicBuilder"/>
    /// </summary>
    /// <returns>A new instance of <see cref="ITopicBuilder"/></returns>
    ITopicBuilder Clone();
}
