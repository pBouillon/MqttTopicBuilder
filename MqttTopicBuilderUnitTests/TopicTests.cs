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
    }
}
