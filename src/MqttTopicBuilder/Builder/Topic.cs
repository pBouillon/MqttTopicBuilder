using System.Linq;

using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Validators;

namespace MqttTopicBuilder.Builder;

/// <summary>
/// Represent an MQTT topic
/// </summary>
public record Topic
{
    /// <summary>
    /// Number of levels of this topic
    /// </summary>
    public int Levels
        => Value == Mqtt.Topic.Separator.ToString()
            ? 1
            : Value.Split(Mqtt.Topic.Separator).Length;

    /// <summary>
    /// Value of the MQTT topic (e.g. "a/b/c")
    /// </summary>
    public readonly string Value;

    /// <summary>
    /// Convert a <see cref="Topic"/> to a <see cref="string"/>
    /// </summary>
    /// <param name="topic"><see cref="Topic"/> to be converted</param>
    public static explicit operator string(Topic topic)
        => topic.Value;

    /// <summary>
    /// Convert a <see cref="string"/> to a <see cref="Topic"/>
    /// </summary>
    /// <param name="rawTopic">String to be converted</param>
    public static implicit operator Topic(string? rawTopic )
        => new(rawTopic);

    /// <summary>
    /// Create a new MQTT Topic from a raw string
    /// <para>
    /// If <paramref name="rawTopic"/> is empty, the minimal topic will be created 
    /// (made only of a single <see cref="Mqtt.Topic.Separator"/>)
    /// </para>
    /// </summary>
    /// <param name="rawTopic">Raw MQTT topic</param>
    /// <remarks>
    /// Any trailing separator will be removed
    /// </remarks>
    public Topic(string? rawTopic )
    {
        var isEmpty = string.IsNullOrEmpty(rawTopic) || rawTopic == Mqtt.Topic.Separator.ToString();
        if (isEmpty)
        {
            Value = Mqtt.Topic.Separator.ToString();
            return;
        }

        TopicValidator.ValidateTopic(rawTopic!);
        
        var hasTrailingSeparator = rawTopic!.Last() == Mqtt.Topic.Separator;
        if (hasTrailingSeparator)
        {
            rawTopic = rawTopic!.Remove(rawTopic.Length - 1);
        }

        Value = rawTopic!;
    }

    /// <summary>
    /// Static method to create a new MQTT Topic from a raw string
    /// </summary>
    /// <param name="rawTopic">Raw MQTT topic</param>
    /// <remarks>
    /// The raw string will be validated beforehand using <see cref="TopicValidator"/> methods
    /// </remarks>
    /// <returns>A new instance of the Topic</returns>
    public static Topic FromString(string rawTopic)
        => new(rawTopic);

    /// <summary>
    /// Retrieve all atomic topics contained in the topic
    /// </summary>
    /// <returns>An array of those atomic topics</returns>
    public string[] ToArray()
        => Value.Split(Mqtt.Topic.Separator);

    /// <summary>
    /// Returns the topic's value as a string, same as as <see cref="Value"/>
    /// </summary>
    /// <returns>The topic's value</returns>
    public override string ToString()
        => Value;
}
