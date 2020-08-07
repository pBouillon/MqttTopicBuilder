/*
 * Author
 *      Pierre Bouillon - https://github.com/pBouillon
 *
 * Repository
 *      MqttTopicBuilder - https://github.com/pBouillon/MqttTopicBuilder
 *
 * License
 *      MIT - https://github.com/pBouillon/MqttTopicBuilder/blob/master/LICENSE
 */

using System.Runtime.InteropServices.ComTypes;
using AutoFixture;
using FluentAssertions;
using MqttTopicBuilder.Builder;
using MqttTopicBuilder.UnitTests.Utils;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Builder
{
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
    }
}
