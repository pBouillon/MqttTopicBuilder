using MqttTopicBuilder.Builder;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;
using Shouldly;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Builder;

/// <summary>
/// Unit test suite for <see cref="ITopicBuilder"/> for operations
/// independent of the <see cref="Consumer"/>
/// </summary>
public class TopicBuilderUnitTests
{
    /// <summary>
    /// Ensure that the building behavior is valid
    /// </summary>
    [Theory]
    [InlineData(Consumer.Publisher)]
    [InlineData(Consumer.Subscriber)]
    public void Build(Consumer mode)
    {
        var builder = new TopicBuilder(mode)
            .AddTopic("sensors")
            .AddTopic("bedroom")
            .AddTopic("temperature");

        var topic = builder.Build();

        topic.Levels.ShouldBe(3);
    }

    /// <summary>
    /// Ensure that the object's cleaning behavior is valid
    /// </summary>
    [Theory]
    [InlineData(Consumer.Publisher)]
    [InlineData(Consumer.Subscriber)]
    public void Clear(Consumer mode)
    {
        var builder = new TopicBuilder(mode).Clear();

        builder.Levels.ShouldBe(0);
        builder.TopicCollection.ShouldBeEmpty();
    }

    /// <summary>
    /// Ensure that the clone is not altered when modifying the original copy
    /// </summary>
    [Theory]
    [InlineData(Consumer.Publisher)]
    [InlineData(Consumer.Subscriber)]
    public void Clone_OnAlteredOriginalInstance(Consumer mode)
    {
        var builder = new TopicBuilder(mode).AddTopic("sensors");
        var clone = builder.Clone();

        builder = builder.AddTopic("bedroom");

        builder.Levels.ShouldBe(2);
        clone.Levels.ShouldBe(1);
    }

    /// <summary>
    /// Ensure that a <see cref="ITopicBuilder"/> can be created from an existing topic
    /// </summary>
    [Fact]
    public void FromTopic()
    {
        var topic = MqttTopicBuilder.Builder.Topic.FromString("sensors/bedroom/temperature");

        var builder = TopicBuilder.FromTopic(topic, Consumer.Subscriber);

        builder.Build().ShouldBe(topic);
        builder.MaxLevel.ShouldBe(topic.Levels);
    }

    /// <summary>
    /// Ensure that it is not possible to bypass construction rule on a specific <see cref="Consumer"/>
    /// by seeding the <see cref="TopicBuilder"/> with a <see cref="Topic"/> containing forbidden values
    /// </summary>
    [Fact]
    public void FromTopic_WithIllegalConsumer()
    {
        var wildcardTopic = MqttTopicBuilder.Builder.Topic.FromString(Mqtt.Wildcard.SingleLevel.ToString());

        var creatingPublisherBuilderWithSubscriberTopic
            = () => TopicBuilder.FromTopic(wildcardTopic, Consumer.Publisher);

        creatingPublisherBuilderWithSubscriberTopic
            .ShouldThrow<IllegalTopicConstructionException>();
    }
}
