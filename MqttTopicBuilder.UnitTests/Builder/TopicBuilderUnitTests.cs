using AutoFixture;

using FluentAssertions;

using MqttTopicBuilder.Builder;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions.Classes;
using MqttTopicBuilder.UnitTests.Utils;

using System;

using Xunit;

namespace MqttTopicBuilder.UnitTests.Builder;

/// <summary>
/// Unit test suite for <see cref="ITopicBuilder"/> for operations
/// independent of the <see cref="TopicConsumer"/>
/// </summary>
public class TopicBuilderUnitTests
{
    /// <summary>
    /// Private instance of <see cref="IFixture"/> for test data generation purposes
    /// </summary>
    private static readonly IFixture Fixture = new Fixture();

    /// <summary>
    /// Ensure that the building behaviour is valid
    /// </summary>
    [Fact]
    public void Build()
    {
        // Arrange
        ITopicBuilder builder = Fixture.Create<TopicBuilder>();

        var upperBound = builder.MaxLevel - 1;
        var topicCount = (Fixture.Create<int>() % upperBound) + 1;
        for (var i = 0; i < topicCount; ++i)
        {
            builder = builder.AddTopic(Fixture.Create<string>());
        }

        // Act
        var topic = builder.Build();

        // Assert
        topic.Levels
            .Should()
            .Be(builder.Levels,
                "because the content of the topic should not be altered");
    }

    /// <summary>
    /// Ensure that the object's cleaning behaviour is valid
    /// </summary>
    [Fact]
    public void Clear()
    {
        // Arrange
        var builder = Fixture.Create<TopicBuilder>();

        // Act
        var cleaned = builder.Clear();

        // Assert
        cleaned.Levels
            .Should()
            .Be(0,
                "because the builder must not contain any level anymore");

        cleaned.TopicCollection
            .ToList()
            .Should()
            .BeEmpty("because the inner collection should have been cleaned");
    }

    /// <summary>
    /// Ensure that the clone is successful
    /// </summary>
    [Fact]
    public void Clone()
    {
        // Arrange
        var initial = Fixture.Create<TopicBuilder>();

        // Act
        var clone = initial.Clone();

        // Assert
        clone.MaxLevel.Should()
            .Be(initial.MaxLevel,
                "because the same max level should have been copied");

        clone.Levels.Should()
            .Be(initial.Levels,
                "because the content level count should also have been cloned");
    }

    /// <summary>
    /// Ensure that the clone is not altered when modifying the original copy
    /// </summary>
    [Fact]
    public void Clone_OnAlteredOriginalInstance()
    {
        // Arrange
        var initial = Fixture.Create<TopicBuilder>();
        var clone = initial.Clone();
        var initialCount = initial.Levels;

        // Act
        initial.AddTopic(TestUtils.GenerateSingleValidTopic());

        // Assert
        clone.Levels.Should()
            .Be(initialCount,
                "because altering the origin should not alter the cloned instance");
    }

    /// <summary>
    /// Ensure that a <see cref="ITopicBuilder"/> can be created from an existing topic
    /// </summary>
    [Fact]
    public void FromTopic()
    {
        // Arrange
        var topic = MqttTopicBuilder.Builder.Topic.FromString(
            TestUtils.GenerateValidTopic());

        // Act
        var builder = TopicBuilder.FromTopic(topic, TopicConsumer.Subscriber);

        // Assert
        builder.Build()
            .Should()
            .Be(topic, 
                "because the topic should have been built from the provided one");

        builder.MaxLevel
            .Should()
            .Be(topic.Levels,
                "because the builder should only hold the provided topic");
    }

    /// <summary>
    /// Ensure that it is not possible to bypass construction rule on a specific <see cref="TopicConsumer"/>
    /// by seeding the <see cref="TopicBuilder"/> with a <see cref="Topic"/> containing forbidden values
    /// </summary>
    [Fact]
    public void FromTopic_WithIllegalConsumer()
    {
        // Arrange
        var wildcardTopic = MqttTopicBuilder.Builder.Topic.FromString(
            Mqtt.Wildcard.SingleLevel.ToString());

        // Act
        Action creatingPublisherBuilderWithSubscriberTopic = () =>
            _ = TopicBuilder.FromTopic(wildcardTopic, TopicConsumer.Publisher);

        // Assert
        creatingPublisherBuilderWithSubscriberTopic.Should()
            .Throw<IllegalTopicConstructionException>(
                "because a wildcard should not be allowed in PUB mode");
    }
}