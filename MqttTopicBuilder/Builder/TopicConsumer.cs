namespace MqttTopicBuilder.Builder;

/// <summary>
/// Represent the different possible consumers of a topic to be build
/// using <see cref="ITopicBuilder"/>
/// </summary>
public enum TopicConsumer
{
    /// <summary>
    /// Indicate that the topic is intended to be used on an MQTT PUBLISH
    /// </summary>
    Publisher,

    /// <summary>
    /// Indicate that the topic is intended to be used on an MQTT SUBSCRIBE
    /// </summary>
    Subscriber,
}