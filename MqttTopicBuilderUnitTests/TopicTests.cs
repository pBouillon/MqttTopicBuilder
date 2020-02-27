/**
 * Author
 *      Pierre Bouillon - https://github.com/pBouillon
 *
 * Repository
 *      MqttTopicBuilder - https://github.com/pBouillon/MqttTopicBuilder
 *
 * License
 *      MIT - https://github.com/pBouillon/MqttTopicBuilder/blob/master/LICENSE
 */

namespace MqttTopicBuilderUnitTests
{
    using AutoFixture;
    using FluentAssertions;
    using MqttTopicBuilder;
    using MqttTopicBuilder.Exceptions;
    using MqttTopicBuilder.Constants;
    using System;
    using System.Text;
    using Xunit;

    public class TopicTests
    {
        /// <summary>
        /// AutoFixture's object to generate fixtures
        /// </summary>
        /// <see cref="Fixture"/>
        private readonly Fixture _fixture = new Fixture();

        /// <summary>
        /// Ensure that the topic is correctly built from a valid topic
        /// </summary>
        [Fact]
        public void Topic_CreateTopicFromValidRawString()
        {
            // Arrange
            var topicDepth = _fixture.Create<int>();
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
                    "because a topic containing single level wildcards should be valid");
        }

        /// <summary>
        /// Check if the topic correctly raises an exception on a topic made of whitespaces
        /// </summary>
        [Fact]
        public void Topic_EmptyTopicExceptionOnBlankTopic()
        {
            // Arrange
            const string baseTopic = " \t ";

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
        /// Check if an exception is correctly raised when creating a topic containing the <see cref="Topics.NullCharacter"/>
        /// </summary>
        [Fact]
        public void Topic_EmptyTopicExceptionOnNullChar()
        {
            // Arrange
            const char rawTopic = Topics.NullCharacter;

            // Act
            Action creatingATopicFromNullCharacter = ()
                => new Topic(rawTopic.ToString());

            // Arrange
            creatingATopicFromNullCharacter.Should()
                .Throw<EmptyTopicException>(
                    "because a valid topic should not contain the null character");
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
        /// Ensure that the topic is correctly raising an error when creating a topic containing a global wildcard
        /// </summary>
        [Fact]
        public void Topic_Parse_CreateTopicFromRawStringContainingMultiLevelWildcard()
        {
            // Arrange
            var rawTopic = $"mqtt{Topics.Separator}" +
                        $"topic{Topics.Separator}" +
                        $"{Wildcards.MultiLevel}{Topics.Separator}" +
                        $"builder";

            // Act
            Action attemptingToBuildTopicContainingMultiLevelWildcard = ()
                => Topic.Parse(rawTopic);

            // Arrange
            attemptingToBuildTopicContainingMultiLevelWildcard.Should()
                .Throw<IllegalTopicConstructionException>(
                    "because a topic containing multi level wildcard should not be a valid one");
        }

        /// <summary>
        /// Check if an exception is correctly raised when creating a topic containing the <see cref="Topics.NullCharacter"/>
        /// </summary>
        [Fact]
        public void Topic_Parse_NullCharacter()
        {
            // Arrange
            const char rawTopic = Topics.NullCharacter;

            // Act
            Action creatingATopicFromNullCharacter = ()
                => Topic.Parse(rawTopic.ToString());

            // Arrange
            creatingATopicFromNullCharacter.Should()
                .Throw<EmptyTopicException>(
                    "because a valid topic should not contain the null character");
        }

        /// <summary>
        /// Ensure that the topic is correctly raising an error when creating a topic containing a global wildcard
        /// </summary>
        [Fact]
        public void Topic_Parse_CreateTopicFromRawStringContainingSingleLevelWildcard()
        {
            // Arrange
            var rawTopic = $"mqtt{Topics.Separator}" +
                        $"topic{Topics.Separator}" +
                        $"{Wildcards.SingleLevel}{Topics.Separator}" +
                        $"builder";

            // Act
            Action attemptingToBuildTopicContainingMultiLevelWildcard = ()
                => Topic.Parse(rawTopic);

            // Arrange
            attemptingToBuildTopicContainingMultiLevelWildcard.Should()
                .NotThrow<IllegalTopicConstructionException>(
                    "because a topic containing single level wildcards should be valid");
        }

        /// <summary>
        /// Check if the topic correctly raises an exception on a topic made of whitespaces
        /// </summary>
        [Fact]
        public void Topic_Parse_EmptyTopicExceptionOnBlankTopic()
        {
            // Arrange
            const string baseTopic = " \t ";

            // Act
            Action createTopicFromEmptyString = ()
                => Topic.Parse(baseTopic);

            // Assert
            createTopicFromEmptyString.Should()
                .Throw<EmptyTopicException>(
                    "because a topic made of spaces is not a valid topic");
        }

        /// <summary>
        /// Check if the topic correctly raises an exception on an empty topic
        /// </summary>
        [Fact]
        public void Topic_Parse_EmptyTopicExceptionOnEmptyTopic()
        {
            // Arrange
            var baseTopic = string.Empty;

            // Act
            Action createTopicFromEmptyString = ()
                => Topic.Parse(baseTopic);

            // Assert
            createTopicFromEmptyString.Should()
                .Throw<EmptyTopicException>(
                    "because an empty string must throw an exception");
        }

        /// <summary>
        /// Check if the topic correctly raises an exception on a null topic
        /// </summary>
        [Fact]
        public void Topic_Parse_EmptyTopicExceptionOnNullTopic()
        {
            // Arrange
            var baseTopic = $"{Topics.Separator}{Topics.Separator}";

            // Act
            Action createTopicFromEmptyString = ()
                => Topic.Parse(baseTopic);

            // Assert
            createTopicFromEmptyString.Should()
                .Throw<EmptyTopicException>(
                    "because a null topic is not be considered as valid");
        }

        /// <summary>
        /// Check if the topic correctly raises an exception on too long slice of it
        /// </summary>
        [Fact]
        public void Topic_Parse_TooLongTopicExceptionOnTooLongTopic()
        {
            // Arrange
            var baseTopic = $"mqtt{Topics.Separator}" +
                            $"topic{Topics.Separator}" +
                            $"{new string('*', Topics.MaxSliceLength + 1)}{Topics.Separator}" +
                            $"builder";

            // Act
            Action createTopicFromEmptyString = ()
                => Topic.Parse(baseTopic);

            // Assert
            createTopicFromEmptyString.Should()
                .Throw<TooLongTopicException>(
                    "because a topic with a slice exceeding the `Topics.MaxSliceLength` length should not be considered as valid");
        }

        /// <summary>
        /// Check if a topic is correctly created from the static method
        /// </summary>
        [Fact]
        public void Topic_Parse_ValidRawString()
        {
            // Arrange
            var topicDepth = _fixture.Create<int>();
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
        /// Ensure that the topic is correctly raising an error when creating a topic containing a global wildcard
        /// </summary>
        [Fact]
        public void Topic_TryParse_CreateTopicFromRawStringContainingMultiLevelWildcard()
        {
            // Arrange
            var rawTopic = $"mqtt{Topics.Separator}" +
                        $"topic{Topics.Separator}" +
                        $"{Wildcards.MultiLevel}{Topics.Separator}" +
                        $"builder";

            // Act
            var result = Topic.TryParse(rawTopic, out var topic);

            // Assert
            result.Should()
                .BeFalse(
                    "because a `IllegalTopicConstructionException` should have been raised but silenced");

            topic.Should()
                .BeNull(
                    "because no instantiation should have been done");
        }

        /// <summary>
        /// Ensure that the topic is correctly raising an error when creating a topic containing a global wildcard
        /// </summary>
        [Fact]
        public void Topic_TryParse_CreateTopicFromRawStringContainingSingleLevelWildcard()
        {
            // Arrange
            var rawTopic = $"mqtt{Topics.Separator}" +
                        $"topic{Topics.Separator}" +
                        $"{Wildcards.SingleLevel}{Topics.Separator}" +
                        $"builder";

            // Act
            Action attemptingToBuildTopicContainingMultiLevelWildcard = ()
                => Topic.TryParse(rawTopic, out var topic);

            // Arrange
            attemptingToBuildTopicContainingMultiLevelWildcard.Should()
                .NotThrow<IllegalTopicConstructionException>(
                    "because a topic containing single level wildcards should be valid");
        }

        /// <summary>
        /// Check if the topic correctly raises an exception on a topic made of whitespaces
        /// </summary>
        [Fact]
        public void Topic_TryParse_EmptyTopicExceptionOnBlankTopic()
        {
            // Arrange
            const string baseTopic = " \t ";

            // Act
            var result = Topic.TryParse(baseTopic, out var topic);

            // Assert
            result.Should()
                .BeFalse(
                    "because a `EmptyTopicException` should have been raised but silenced");

            topic.Should()
                .BeNull(
                    "because no instantiation should have been done");
        }

        /// <summary>
        /// Check if the topic correctly raises an exception on an empty topic
        /// </summary>
        [Fact]
        public void Topic_TryParse_EmptyTopicExceptionOnEmptyTopic()
        {
            // Arrange
            var baseTopic = string.Empty;

            // Act
            var result = Topic.TryParse(baseTopic, out var topic);

            // Assert
            result.Should()
                .BeFalse(
                    "because a `EmptyTopicException` should have been raised but silenced");

            topic.Should()
                .BeNull(
                    "because no instantiation should have been done");
        }

        /// <summary>
        /// Check if the topic correctly raises an exception on a null topic
        /// </summary>
        [Fact]
        public void Topic_TryParse_EmptyTopicExceptionOnNullTopic()
        {
            // Arrange
            var baseTopic = $"{Topics.Separator}{Topics.Separator}";

            // Act
            var result = Topic.TryParse(baseTopic, out var topic);

            // Assert
            result.Should()
                .BeFalse(
                    "because a `EmptyTopicException` should have been raised but silenced");

            topic.Should()
                .BeNull(
                    "because no instantiation should have been done");
        }

        /// <summary>
        /// Check if an exception is correctly raised when creating a topic containing the <see cref="Topics.NullCharacter"/>
        /// </summary>
        [Fact]
        public void Topic_TryParse_NullCharacter()
        {
            // Arrange
            const char nullChar = Topics.NullCharacter;

            // Act
            var result = Topic.TryParse(nullChar.ToString(), out var topic);

            // Arrange
            result.Should()
                .BeFalse(
                    "because a `EmptyTopicException` should have been raised but silenced");

            topic.Should()
                .BeNull(
                    "because a valid topic should not contain the null character");
        }

        /// <summary>
        /// Check if the topic correctly raises an exception on too long slice of it
        /// </summary>
        [Fact]
        public void Topic_TryParse_TooLongTopicExceptionOnTooLongTopic()
        {
            // Arrange
            var baseTopic = $"mqtt{Topics.Separator}" +
                            $"topic{Topics.Separator}" +
                            $"{new string('*', Topics.MaxSliceLength + 1)}{Topics.Separator}" +
                            $"builder";

            // Act
            var result = Topic.TryParse(baseTopic, out var topic);

            // Assert
            result.Should()
                .BeFalse(
                    "because a `TooLongTopicException` should have been raised but silenced");

            topic.Should()
                .BeNull(
                    "because no instantiation should have been done");
        }

        /// <summary>
        /// Check if a topic is correctly created from the static method
        /// </summary>
        [Fact]
        public void Topic_TryParse_ValidRawString()
        {
            // Arrange
            var topicDepth = _fixture.Create<int>();
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
