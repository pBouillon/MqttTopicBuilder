using System.Linq;

using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;

namespace MqttTopicBuilder.Validators;

/// <summary>
/// Validates MQTT topic rules
/// </summary>
public static class TopicValidator
{
    /// <summary>
    /// Validate a fully built topic
    /// </summary>
    /// <param name="topic">Topic value to be checked</param>
    /// <exception cref="EmptyTopicException">If the topic is blank or empty</exception>
    /// <exception cref="IllegalTopicConstructionException">If the topic is abusing wildcard usage</exception>
    /// <exception cref="InvalidTopicException">If the topic is malformed</exception>
    public static void ValidateTopic(string topic)
    {
        // Allows minimal (and deprecated) MQTT topic: "/"
        if (topic == Mqtt.Topic.Separator.ToString())
        {
            return;
        }

        // Validate the whole topic
        ValidatorFactory
            .GetRawTopicValidator()
            .Validate(topic);

        // Remove trailing "/" if any
        if (topic.Last() == Mqtt.Topic.Separator)
        {
            topic = topic.Remove(topic.Length - 1);
        }

        // Validate each of the single topics from which the main one is made
        var singleTopicValidator = ValidatorFactory.GetSingleRawTopicValidator();

        topic
            .Split(Mqtt.Topic.Separator)
            .ToList()
            .ForEach(singleTopicValidator.Validate);
    }

    /// <summary>
    /// Validate a topic to be appended to a longer one
    /// </summary>
    /// <param name="topic">Topic value to be checked</param>
    /// <exception cref="EmptyTopicException">If the topic is blank or empty</exception>
    /// <exception cref="InvalidTopicException">If the topic is malformed</exception>
    public static void ValidateTopicForAppending(this string topic)
        => ValidatorFactory.GetSingleRawTopicValidator()
            .Validate(topic);
}
