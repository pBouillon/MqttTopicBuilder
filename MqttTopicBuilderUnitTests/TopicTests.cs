﻿/**
 * Author
 *      Pierre Bouillon - https://github.com/pBouillon
 *
 * Repository
 *      MqttTopicBuilder - https://github.com/pBouillon/MqttTopicBuilder
 *
 * License
 *      MIT - https://github.com/pBouillon/MqttTopicBuilder/blob/master/LICENSE
 */

using AutoFixture;

namespace MqttTopicBuilderUnitTests
{
    using FluentAssertions;
    using MqttTopicBuilder;
    using MqttTopicBuilder.Exceptions;
    using MqttTopicBuilder.MqttUtils;
    using System;
    using System.Text;
    using Xunit;

    public class TopicTests
    {
        /// <summary>
        /// Ensure that the topic is correctly built from a valid topic
        /// </summary>
        [Fact]
        public void Topic_CreateTopicFromValidRawString()
        {
            // Arrange
            var fixture = new Fixture();
            
            var topicDepth = fixture.Create<int>();
            topicDepth = topicDepth > Topics.MaxDepth
                ? Topics.MaxDepth
                : topicDepth;

            var topic = "";
            for (var i = 0; i < topicDepth; ++i)
            {
                topic += "MqttTopicBuilder";
                
                if (i + 1 != topicDepth)
                {
                    topic += Topics.Separator;
                }
            }

            // Act
            var actual = new Topic(topic).Path;

            // Arrange
            actual.Should()
                .Be(topic);
        }

        /// <summary>
        /// Ensure that the topic is correctly raising an error when creating a topic containing a global wildcard
        /// </summary>
        [Fact]
        public void Topic_CreateTopicFromRawStringContainingMultiLevelWildcard()
        {
            // Arrange
            var fixture = new Fixture();
            var topic = $"mqtt{Topics.Separator}" +
                        $"topic{Topics.Separator}" +
                        $"{Wildcards.MultiLevel}{Topics.Separator}" +
                        $"builder";

            // Act
            Action attemptingToBuildTopicContainingMultiLevelWildcard = ()
                => new Topic(topic);

            // Arrange
            attemptingToBuildTopicContainingMultiLevelWildcard.Should()
                .Throw<IllegalTopicConstructionException>(
                    "because a topic containing multi level wildcard should not be a valid one");
        }

        /// <summary>
        /// Ensure that the topic is correctly raising an error when creating a topic containing a global wildcard
        /// </summary>
        [Fact]
        public void Topic_CreateTopicFromRawStringContainingSingleLevelWildcard()
        {
            // Arrange
            var fixture = new Fixture();
            var topic = $"mqtt{Topics.Separator}" +
                        $"topic{Topics.Separator}" +
                        $"{Wildcards.SingleLevel}{Topics.Separator}" +
                        $"builder";

            // Act
            Action attemptingToBuildTopicContainingMultiLevelWildcard = ()
                => new Topic(topic);

            // Arrange
            attemptingToBuildTopicContainingMultiLevelWildcard.Should()
                .NotThrow<IllegalTopicConstructionException>(
                    "because a topic containing multi level wildcard should not be a valid one");
        }

        /// <summary>
        /// Check if the topic correctly raises an exception on a topic made of whitespaces
        /// </summary>
        [Fact]
        public void Topic_EmptyTopicExceptionOnBlankTopic()
        {
            // Arrange
            var baseTopic = " \t ";

            // Act
            Action createTopicFromEmptyString = ()
                => new Topic(baseTopic);

            // Assert
            createTopicFromEmptyString.Should()
                .Throw<EmptyTopicException>(
                    "because a topic made of spaces is not a valid topic");
        }

        /// <summary>
        /// Check if the topic correctly raises an exception on an empty topic
        /// </summary>
        [Fact]
        public void Topic_EmptyTopicExceptionOnEmptyTopic()
        {
            // Arrange
            var baseTopic = string.Empty;

            // Act
            Action createTopicFromEmptyString = ()
                => new Topic(baseTopic);

            // Assert
            createTopicFromEmptyString.Should()
                .Throw<EmptyTopicException>(
                    "because an empty string must throw an exception");
        }

        /// <summary>
        /// Check if the topic correctly raises an exception on a null topic
        /// </summary>
        [Fact]
        public void Topic_EmptyTopicExceptionOnNullTopic()
        {
            // Arrange
            var baseTopic = $"{Topics.Separator}{Topics.Separator}";

            // Act
            Action createTopicFromEmptyString = ()
                => new Topic(baseTopic);

            // Assert
            createTopicFromEmptyString.Should()
                .Throw<EmptyTopicException>(
                    "because a null topic is not be considered as valid");
        }

        /// <summary>
        /// Check if the topic correctly raises an exception on too long slice of it
        /// </summary>
        [Fact]
        public void Topic_TooLongTopicExceptionOnTooLongTopic()
        {
            // Arrange
            var baseTopic = $"mqtt{Topics.Separator}" +
                            $"topic{Topics.Separator}" +
                            $"{new string('*', Topics.MaxSliceLength + 1)}{Topics.Separator}" +
                            $"builder";

            // Act
            Action createTopicFromEmptyString = ()
                => new Topic(baseTopic);

            // Assert
            createTopicFromEmptyString.Should()
                .Throw<TooLongTopicException>(
                    "because a topic with a slice exceeding the `Topics.MaxSliceLength` length should not be considered as valid");
        }

        /// <summary>
        /// Check if the object correctly retrieves the topic's level
        /// </summary>
        [Fact]
        public void Topic_Level_OneOnSingleTopic()
        {
            // Arrange
            const int expected = 1;
            const string singleTopic = "MqttTopicBuilder";

            // Act
            var actual = new Topic(singleTopic).Level;

            // Assert
            actual.Should()
                .Be(expected,
                    "because a single level topic without '/' should be considered as a level one topic");
        }

        /// <summary>
        /// Check if no error is thrown or further modification performed on the sanitizing of an empty string
        /// </summary>
        [Fact]
        public void Topic_Normalize_EmptyString()
        {
            // Arrange
            var expected = string.Empty;
            var toNormalize = string.Empty;

            // Act
            var actual = Topic.Normalize(toNormalize);

            // Assert
            actual.Should()
                .Be(expected,
                    "because an empty string can't be normalized more than it is");
        }

        /// <summary>
        /// Check if a topic containing separators is correctly sanitized
        /// </summary>
        [Fact]
        public void Topic_Normalize_RemoveSeparator()
        {
            // Arrange
            const string expected = "MqttTopicBuilder";
            var toNormalize = new StringBuilder()
                .Append(Topics.Separator)
                .Append("Mqtt")
                .Append("Topic")
                .Append("Builder")
                .Append(Topics.Separator)
                .ToString();

            // Act
            var actual = Topic.Normalize(toNormalize);

            // Assert
            actual.Should()
                .Be(expected,
                    "because all topic separators must have been cleared");
        }

        /// <summary>
        /// Check if a topic containing spaces is correctly sanitized
        /// </summary>
        [Fact]
        public void Topic_Normalize_RemoveSpaces()
        {
            // Arrange
            const string expected = "MqttTopicBuilder";
            var toNormalize = new StringBuilder()
                .Append(" ")
                .Append("Mqtt")
                .Append("Topic")
                .Append(" ")
                .Append("Builder")
                .Append(" ")
                .ToString();

            // Act
            var actual = Topic.Normalize(toNormalize);

            // Assert
            actual.Should()
                .Be(expected,
                    "because all spaces must have been cleared");
        }

        /// <summary>
        /// Check if a topic containing illegal symbols is correctly sanitized
        /// </summary>
        [Fact]
        public void Topic_Normalize_RemoveUnexpectedSymbols()
        {
            // Arrange
            const string expected = "MqttTopicBuilder";
            var toNormalize = new StringBuilder()
                .Append(Wildcards.SingleLevel)
                .Append("Mqtt")
                .Append(Wildcards.MultiLevel)
                .Append("Topic")
                .Append(Topics.Separator)
                .Append("Builder")
                .Append(" ")
                .ToString();

            // Act
            var actual = Topic.Normalize(toNormalize);

            // Assert
            actual.Should()
                .Be(expected,
                    "because all wildcards and separators must have been cleared");
        }

        /// <summary>
        /// Check if a topic containing wildcards is correctly sanitized
        /// </summary>
        [Fact]
        public void Topic_Normalize_RemoveWildcards()
        {
            // Arrange
            const string expected = "MqttTopicBuilder";
            var toNormalize = new StringBuilder()
                .Append(Wildcards.SingleLevel)
                .Append("Mqtt")
                .Append(Wildcards.MultiLevel)
                .Append("Topic")
                .Append(Wildcards.MultiLevel)
                .Append("Builder")
                .Append(Wildcards.SingleLevel)
                .ToString();

            // Act
            var actual = Topic.Normalize(toNormalize);

            // Assert
            actual.Should()
                .Be(expected,
                    "because all wildcards must have been cleared");
        }

        /// <summary>
        /// Check if a topic is correctly created from the static method
        /// </summary>
        [Fact]
        public void Topic_Parse_ParseAValidRawString()
        {
            // Arrange
            var fixture = new Fixture();

            var topicDepth = fixture.Create<int>();
            topicDepth = topicDepth > Topics.MaxDepth
                ? Topics.MaxDepth
                : topicDepth;

            var topic = "";
            for (var i = 0; i < topicDepth; ++i)
            {
                topic += "MqttTopicBuilder";

                if (i + 1 != topicDepth)
                {
                    topic += Topics.Separator;
                }
            }

            // Act
            var actual = Topic.Parse(topic);

            // Arrange
            actual.Path.Should()
                .Be(topic,
                    "because the topic must have been correctly built by the static method");
        }

        /// <summary>
        /// Check if a topic is correctly created from the static method
        /// </summary>
        [Fact]
        public void Topic_TryParse_TryParseAValidRawString()
        {
            // Arrange
            var fixture = new Fixture();

            var topicDepth = fixture.Create<int>();
            topicDepth = topicDepth > Topics.MaxDepth
                ? Topics.MaxDepth
                : topicDepth;

            var rawTopic = "";
            for (var i = 0; i < topicDepth; ++i)
            {
                rawTopic += "MqttTopicBuilder";

                if (i + 1 != topicDepth)
                {
                    rawTopic += Topics.Separator;
                }
            }

            // Act
            var result = Topic.TryParse(rawTopic, out var topic);

            // Arrange
            topic.Path.Should()
                .Be(rawTopic,
                    "because the topic should not have been altered during the process");

            result.Should()
                .BeTrue("because the topic is valid and must not have raise any error");
        }
    }
}
