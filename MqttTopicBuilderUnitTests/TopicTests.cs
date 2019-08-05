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
    using MqttTopicBuilder.MqttUtils;
    using System.Text;
    using Xunit;

    public class TopicTests
    {
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
        /// Check if the topic correctly retrieves the topic level on an empty topic path
        /// </summary>
        [Fact]
        public void Topic_Level_ZeroOnEmptyTopic()
        {
            // Arrange
            const int expected = 0;
            var baseTopic = string.Empty;

            // Act
            var actual = new Topic(baseTopic).Level;

            // Assert
            actual.Should()
                .Be(expected,
                    "because an empty string must result in a topic of level 0");
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
                    "because all topic's separators must have been cleared");
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
