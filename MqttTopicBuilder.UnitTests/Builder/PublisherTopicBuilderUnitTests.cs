﻿using AutoFixture;

using FluentAssertions;

using MqttTopicBuilder.Builder;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions.Classes;

using System;
using System.Collections.Generic;

using Xunit;

namespace MqttTopicBuilder.UnitTests.Builder;

/// <summary>
/// Unit test suite for <see cref="ITopicBuilder"/> when used
/// along with <see cref="TopicConsumer.Publisher"/>
/// </summary>
public class PublisherTopicBuilderUnitTests
{
    /// <summary>
    /// Private instance of <see cref="IFixture"/> for test data generation purposes
    /// </summary>
    private static readonly IFixture Fixture = new Fixture();

    /// <summary>
    /// Ensure that the appending of a multi-level wildcard is correctly made
    /// </summary>
    [Fact]
    public void AddMultiLevelWildcard()
    {
        // Arrange
        ITopicBuilder builder = new TopicBuilder(TopicConsumer.Publisher);

        // Act
        Action addMultiLevelWildcard = () =>
            builder = builder.AddMultiLevelWildcard();

        // Assert
        addMultiLevelWildcard.Should()
            .Throw<IllegalStateOperationException>(
                "because a topic used on PUBLISH mode cannot use wildcard");
    }

    /// <summary>
    /// Ensure that the appending of a single-level wildcard is correctly made
    /// </summary>
    [Fact]
    public void AddSingleLevelWildcard()
    {
        // Arrange
        ITopicBuilder builder = new TopicBuilder(TopicConsumer.Publisher);

        // Act
        Action addSingleLevelWildcard = () =>
            builder = builder.AddMultiLevelWildcard();

        // Assert
        addSingleLevelWildcard.Should()
            .Throw<IllegalStateOperationException>(
                "because a topic used on PUBLISH mode cannot use wildcard");
    }

    /// <summary>
    /// Ensure that a blank topic can not be added
    /// </summary>
    [Fact]
    public void AddTopic_OnBlankTopic()
    {
        // Arrange
        ITopicBuilder builder = new TopicBuilder(TopicConsumer.Publisher);

        // Act
        Action appendingEmptyTopic = () =>
            builder.AddTopic(string.Empty);

        // Assert
        appendingEmptyTopic.Should()
            .Throw<EmptyTopicException>("because an empty topic is not a valid one to be added");
    }

    /// <summary>
    /// Ensure that a multi-level wildcard manually added has the same effects as the
    /// method to perform the same addition
    /// </summary>
    [Fact]
    public void AddTopic_OnMultiLevelWildcard()
    {
        // Arrange
        ITopicBuilder builder = new TopicBuilder(TopicConsumer.Publisher);

        // Act
        Action addSingleLevelWildcard = () =>
            builder = builder.AddTopic(
                Mqtt.Wildcard.MultiLevel.ToString());

        // Assert
        addSingleLevelWildcard.Should()
            .Throw<IllegalTopicConstructionException>(
                "because a topic used on PUBLISH mode cannot use wildcard");
    }

    /// <summary>
    /// Ensure that a single-level wildcard manually added has the same effects as the
    /// method to perform the same addition
    /// </summary>
    [Fact]
    public void AddTopic_OnSingleLevelWildcard()
    {
        // Arrange
        ITopicBuilder builder = new TopicBuilder(TopicConsumer.Publisher);

        // Act
        Action addSingleLevelWildcard = () =>
            builder = builder.AddTopic(
                Mqtt.Wildcard.SingleLevel.ToString());

        // Assert
        addSingleLevelWildcard.Should()
            .Throw<IllegalTopicConstructionException>(
                "because a topic used on PUBLISH mode cannot use wildcard");
    }

    /// <summary>
    /// Ensure that the appending of a topic separator is forbidden
    /// </summary>
    [Fact]
    public void AddTopic_OnTopicSeparator()
    {
        // Arrange
        ITopicBuilder builder = new TopicBuilder(TopicConsumer.Publisher);

        // Act
        Action appendingTopic = () =>
            builder.AddTopic(
                Mqtt.Topic.Separator.ToString());

        // Assert
        appendingTopic.Should()
            .Throw<InvalidTopicException>("because the topic separator is not a valid topic to be appended");
    }

    /// <summary>
    /// Ensure that the regular behaviour is valid
    /// </summary>
    [Fact]
    public void AddTopic_OnValidTopic()
    {
        // Arrange
        var addCount = Fixture.Create<int>();
        ITopicBuilder builder = new TopicBuilder(addCount + 1, TopicConsumer.Publisher);

        // Act
        for (var i = 0; i < addCount; ++i)
        {
            builder = builder.AddTopic(Fixture.Create<string>());
        }

        // Assert
        builder.Levels
            .Should()
            .Be(addCount,
                "because there should be as many levels as topics added");
    }

    /// <summary>
    /// Ensure that multiple topics can successfully be added at once
    /// </summary>
    [Fact]
    public void AddTopics_OnTopicsWithMultiLevelWildcard()
    {
        // Arrange
        var topics = Fixture.Create<List<string>>();
        topics.Add(Mqtt.Wildcard.MultiLevel.ToString());
        topics.AddRange(Fixture.Create<List<string>>());

        ITopicBuilder builder = new TopicBuilder(topics.Count + 1, TopicConsumer.Publisher);

        // Act
        Action addTopicsWithAMultiLevelWildcard = () =>
            builder.AddTopics(topics);

        // Assert
        addTopicsWithAMultiLevelWildcard.Should()
            .Throw<IllegalTopicConstructionException>(
                "because adding a multi-level wildcard among other topics is not valid");
    }

    /// <summary>
    /// Ensure that multiple topics can successfully be added at once
    /// </summary>
    [Fact]
    public void AddTopics_OnValidTopics()
    {
        // Arrange
        var topics = Fixture.Create<string[]>();
        ITopicBuilder builder = new TopicBuilder(topics.Length + 1, TopicConsumer.Publisher);

        // Act
        builder = builder.AddTopics(topics);

        // Assert
        builder.Levels
            .Should()
            .Be(topics.Length,
                "because all topics should have been added");
    }
}