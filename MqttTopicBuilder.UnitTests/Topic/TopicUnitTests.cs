using FluentAssertions;

using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.UnitTests.Utils;

using Xunit;

namespace MqttTopicBuilder.UnitTests.Topic;

/// <summary>
/// Unit test suite for <see cref="Topic"/>
/// </summary>
public class TopicUnitTests
{
    /// <summary>
    /// Ensure that the implicit conversion of a string to a topic is valid
    /// </summary>
    [Fact]
    public void Conversion_ExplicitFromTopic()
    {
        var rawTopic = TestUtils.GenerateValidTopic();
        var topic = new MqttTopicBuilder.Builder.Topic(rawTopic);

        var rawTopicValue = (string) topic;

        rawTopicValue
            .Should()
            .Be(rawTopic, "because a topic should have been created using the initial raw value");
    }

    /// <summary>
    /// Ensure that the implicit conversion of a string to a topic is valid
    /// </summary>
    [Fact]
    public void Conversion_ImplicitFromString()
    {
        var rawTopic = TestUtils.GenerateValidTopic();

        var topic = (MqttTopicBuilder.Builder.Topic) rawTopic;

        topic.Value
            .Should()
            .Be(rawTopic, "because a topic should have been created using the initial raw value");
    }

    /// <summary>
    /// Ensure that the minimal topic is safely built from an empty string
    /// </summary>
    [Fact]
    public void FromString_FromEmptyTopic()
    {
        var emptyTopic = string.Empty;

        var topicBuilt = new MqttTopicBuilder.Builder.Topic(emptyTopic);

        topicBuilt.Value
            .Should()
            .Be(Mqtt.Topic.Separator.ToString(), "because an empty string will result in the smallest valid topic");

        topicBuilt.Levels
            .Should()
            .Be(1, "because there is only one level, the smallest one");
    }

    /// <summary>
    /// Ensure that an invalid string is not successfully converted to a topic
    /// </summary>
    [Fact]
    public void FromString_FromInvalidRawTopic()
    {
        var invalidRawTopic = $"{Mqtt.Topic.Separator}{Mqtt.Topic.Separator}";

        var creatingInvalidTopic = () => _ = MqttTopicBuilder.Builder.Topic.FromString(invalidRawTopic);

        creatingInvalidTopic
            .Should()
            .Throw<EmptyTopicException>("because two consecutive separators will result in an empty topic");
    }

    /// <summary>
    /// Ensure that the topic is safely built from the smallest valid value
    /// </summary>
    [Fact]
    public void FromString_FromMinimalTopic()
    {
        var minimalRawTopic = Mqtt.Topic.Separator.ToString();

        var minimalTopic = MqttTopicBuilder.Builder.Topic.FromString(minimalRawTopic);

        minimalTopic.Value
            .Should()
            .Be(Mqtt.Topic.Separator.ToString());
    }

    /// <summary>
    /// Ensure that the topic is safely built from a valid raw string
    /// </summary>
    [Fact]
    public void FromString_FromValidRawTopic()
    {
        var validRawTopic = TestUtils.GenerateValidTopic();

        var validTopic = MqttTopicBuilder.Builder.Topic.FromString(validRawTopic);

        validTopic.Value
            .Should()
            .Be(validRawTopic, "because a valid value will not be altered");

        validTopic.Levels
            .Should()
            .Be(validRawTopic.Split(Mqtt.Topic.Separator).Length, "because the same number of level is conserved");
    }

    /// <summary>
    /// Ensure that the topic is safely built from a valid raw string
    /// </summary>
    [Fact]
    public void FromString_FromValidRawTopicWithTrailingSeparator()
    {
        var validRawTopic = TestUtils.GenerateValidTopic();
        var validRawTopicWithTrailingSeparator = validRawTopic + Mqtt.Topic.Separator;

        var validTopic = MqttTopicBuilder.Builder.Topic.FromString(validRawTopicWithTrailingSeparator);

        validTopic.Value
            .Should()
            .Be(validRawTopic, "because the separator has been trimmed");

        validTopic.Levels
            .Should()
            .Be(validRawTopicWithTrailingSeparator.Split(Mqtt.Topic.Separator).Length - 1,
                "because the trailing separator has been removed");
    }

    /// <summary>
    /// Ensure that both topics are treated as value object for equality checks
    /// </summary>
    [Fact]
    public void Topic_EqualityCheckBasedOnValue()
    {
        var rawTopic = TestUtils.GenerateValidTopic();
        var firstTopic = new MqttTopicBuilder.Builder.Topic(rawTopic);
        var secondTopic = new MqttTopicBuilder.Builder.Topic(rawTopic);

        var result = firstTopic == secondTopic;

        result
            .Should()
            .BeTrue("because both topics are holding the same values");
    }

    /// <summary>
    /// Ensure that the minimal topic is safely built from an empty string
    /// </summary>
    [Fact]
    public void Topic_FromEmptyTopic()
    {
        var emptyTopic = string.Empty;

        var topicBuilt = new MqttTopicBuilder.Builder.Topic(emptyTopic);

        topicBuilt.Value
            .Should()
            .Be(Mqtt.Topic.Separator.ToString(),
                "because an empty string will result in the smallest valid topic");

        topicBuilt.Levels
            .Should()
            .Be(1, "because there is only one level, the smallest one");
    }

    /// <summary>
    /// Ensure that an invalid string is not successfully converted to a topic
    /// </summary>
    [Fact]
    public void Topic_FromInvalidRawTopic()
    {
        var invalidRawTopic = $"{Mqtt.Topic.Separator}{Mqtt.Topic.Separator}";

        var creatingInvalidTopic = () => _ = new MqttTopicBuilder.Builder.Topic(invalidRawTopic);

        creatingInvalidTopic
            .Should()
            .Throw<EmptyTopicException>("because two consecutive separators will result in an empty topic");
    }

    /// <summary>
    /// Ensure that the topic is safely built from the smallest valid value
    /// </summary>
    [Fact]
    public void Topic_FromMinimalTopic()
    {
        var minimalRawTopic = Mqtt.Topic.Separator.ToString();

        var minimalTopic = new MqttTopicBuilder.Builder.Topic(minimalRawTopic);

        minimalTopic.Value
            .Should()
            .Be(Mqtt.Topic.Separator.ToString());
    }

    /// <summary>
    /// Ensure that the topic is safely built from a valid raw string
    /// </summary>
    [Fact]
    public void Topic_FromValidRawTopic()
    {
        var validRawTopic = TestUtils.GenerateValidTopic();

        var validTopic = new MqttTopicBuilder.Builder.Topic(validRawTopic);

        validTopic.Value
            .Should()
            .Be(validRawTopic, "because a valid value will not be altered");

        validTopic.Levels
            .Should()
            .Be(validRawTopic.Split(Mqtt.Topic.Separator).Length,
                "because the same number of level is conserved");
    }

    /// <summary>
    /// Ensure that the topic is safely built from a valid raw string
    /// </summary>
    [Fact]
    public void Topic_FromValidRawTopicWithTrailingSeparator()
    {
        var validRawTopic = TestUtils.GenerateValidTopic();
        var validRawTopicWithTrailingSeparator = validRawTopic + Mqtt.Topic.Separator;

        var validTopic = new MqttTopicBuilder.Builder.Topic(validRawTopicWithTrailingSeparator);

        validTopic.Value
            .Should()
            .Be(validRawTopic, "because the separator has been trimmed");

        validTopic.Levels
            .Should()
            .Be(validRawTopicWithTrailingSeparator.Split(Mqtt.Topic.Separator).Length - 1,
                "because the trailing separator has been removed");
    }
}
