using System.Collections.Generic;

namespace MqttTopicBuilder.Builder.BuilderState;

/// <summary>
/// <see cref="ITopicBuilder"/> inner state for method's behavior
/// modifications
/// </summary>
internal interface IBuilderState
{
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
}
