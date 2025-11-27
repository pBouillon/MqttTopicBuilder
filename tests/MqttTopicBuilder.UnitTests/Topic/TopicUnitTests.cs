using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;
using Shouldly;
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
        var topic = new MqttTopicBuilder.Builder.Topic("sensors/temperature");

        var rawTopicValue = (string)topic;

        rawTopicValue.ShouldBe("sensors/temperature");
    }

    /// <summary>
    /// Ensure that the implicit conversion of a string to a topic is valid
    /// </summary>
    [Fact]
    public void Conversion_ImplicitFromString()
    {
        var topic = (MqttTopicBuilder.Builder.Topic)"sensors/temperature";

        topic.Value.ShouldBe("sensors/temperature");
    }

    /// <summary>
    /// Ensure that the minimal topic is safely built from an empty string
    /// </summary>
    [Fact]
    public void FromString_FromEmptyTopic()
    {
        var emptyTopic = string.Empty;

        var topicBuilt = new MqttTopicBuilder.Builder.Topic(emptyTopic);

        topicBuilt.Value.ShouldBe(Mqtt.Topic.Separator.ToString());

        topicBuilt.Levels.ShouldBe(1);
    }

    /// <summary>
    /// Ensure that an invalid string is not successfully converted to a topic
    /// </summary>
    [Fact]
    public void FromString_FromInvalidRawTopic()
    {
        var invalidRawTopic = $"{Mqtt.Topic.Separator}{Mqtt.Topic.Separator}";

        var creatingInvalidTopic = () => _ = MqttTopicBuilder.Builder.Topic.FromString(invalidRawTopic);

        creatingInvalidTopic.ShouldThrow<EmptyTopicException>();
    }

    /// <summary>
    /// Ensure that the topic is safely built from the smallest valid value
    /// </summary>
    [Fact]
    public void FromString_FromMinimalTopic()
    {
        var minimalRawTopic = Mqtt.Topic.Separator.ToString();

        var minimalTopic = MqttTopicBuilder.Builder.Topic.FromString(minimalRawTopic);

        minimalTopic.Value.ShouldBe(Mqtt.Topic.Separator.ToString());
    }

    /// <summary>
    /// Ensure that the topic is safely built from a valid raw string
    /// </summary>
    [Fact]
    public void FromString_FromValidRawTopic()
    {
        var validTopic = MqttTopicBuilder.Builder.Topic.FromString("sensors/temperature");

        validTopic.Value.ShouldBe("sensors/temperature");

        validTopic.Levels.ShouldBe(2);
    }

    /// <summary>
    /// Ensure that the topic is safely built from a valid raw string
    /// </summary>
    [Fact]
    public void FromString_FromValidRawTopicWithTrailingSeparator()
    {
        var validRawTopicWithTrailingSeparator = "sensors/temperature/";

        var validTopic = MqttTopicBuilder.Builder.Topic.FromString(validRawTopicWithTrailingSeparator);

        validTopic.Value.ShouldBe("sensors/temperature");

        validTopic.Levels.ShouldBe(2);
    }

    /// <summary>
    /// Ensure that both topics are treated as value object for equality checks
    /// </summary>
    [Fact]
    public void Topic_EqualityCheckBasedOnValue()
    {
        var rawTopic = "sensors/temperature";

        var firstTopic = new MqttTopicBuilder.Builder.Topic(rawTopic);
        var secondTopic = new MqttTopicBuilder.Builder.Topic(rawTopic);

        var result = firstTopic == secondTopic;

        result.ShouldBeTrue();
    }

    /// <summary>
    /// Ensure that the minimal topic is safely built from an empty string
    /// </summary>
    [Fact]
    public void Topic_FromEmptyTopic()
    {
        var emptyTopic = string.Empty;

        var topicBuilt = new MqttTopicBuilder.Builder.Topic(emptyTopic);

        topicBuilt.Value.ShouldBe(Mqtt.Topic.Separator.ToString());

        topicBuilt.Levels.ShouldBe(1);
    }

    /// <summary>
    /// Ensure that an invalid string is not successfully converted to a topic
    /// </summary>
    [Fact]
    public void Topic_FromInvalidRawTopic()
    {
        var creatingInvalidTopic = () => _ = new MqttTopicBuilder.Builder.Topic("//");

        creatingInvalidTopic.ShouldThrow<EmptyTopicException>();
    }

    /// <summary>
    /// Ensure that the topic is safely built from the smallest valid value
    /// </summary>
    [Fact]
    public void Topic_FromMinimalTopic()
    {
        var minimalTopic = new MqttTopicBuilder.Builder.Topic("/");

        minimalTopic.Value.ShouldBe(Mqtt.Topic.Separator.ToString());
    }

    /// <summary>
    /// Ensure that the topic is safely built from a valid raw string
    /// </summary>
    [Fact]
    public void Topic_FromValidRawTopic()
    {
        var validTopic = new MqttTopicBuilder.Builder.Topic("sensors/bedroom/temperature");

        validTopic.Value.ShouldBe("sensors/bedroom/temperature");

        validTopic.Levels.ShouldBe(3);
    }

    /// <summary>
    /// Ensure that the topic is safely built from a valid raw string
    /// </summary>
    [Fact]
    public void Topic_FromValidRawTopicWithTrailingSeparator()
    {
        var validRawTopicWithTrailingSeparator = "sensors/bedroom/temperature/";
        var validTopic = new MqttTopicBuilder.Builder.Topic(validRawTopicWithTrailingSeparator);

        validTopic.Value.ShouldBe("sensors/bedroom/temperature");

        validTopic.Levels.ShouldBe(3);
    }
}
