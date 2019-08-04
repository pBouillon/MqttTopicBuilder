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
    using Xunit;

    public class TopicBuilderTests
    {
        /// <summary>
        /// AutoFixture's object to generate fixtures
        /// </summary>
        /// <see cref="Fixture"/>
        private readonly Fixture fixture = new Fixture();

        /// <summary>
        /// Check the builder's ability to add a valid topic
        /// </summary>
        [Fact]
        public void TopicBuilder_AddTopic_NewValidTopic()
        {
            // Arrange
            var topic = fixture.Create<string>();
            topic = Topic.Normalize(topic);

            var builder = new TopicBuilder();

            // Act
            builder.AddTopic(topic);

            // Assert
            builder.Level.Should()
                .Be(1, "because exactly one element should have been added");
        }

        /// <summary>
        /// Check the initializations when the default constructor is called
        /// </summary>
        [Fact]
        public void TopicBuilder_Constructor_Default()
        {
            // Arrange + Act
            var builder = new TopicBuilder();
            
            // Assert
            builder.IsEmpty.Should()
                .BeTrue("because invoke the default constructor with no parameters must result in an empty queue");

            builder.Level.Should()
                .Be(0, "because the empty builder shouldn't be of any level");
        }

        /// <summary>
        /// Check the initalizations when the constructor is called with a specified stack size
        /// </summary>
        [Fact]
        public void TopicBuilder_Constructor_SpecifiedQueueSize()
        {
            // Arrange
            var queueSize = fixture.Create<int>();

            // Act
            var builder = new TopicBuilder(queueSize);

            // Assert
            builder.IsEmpty.Should()
                .BeTrue("because invoke the default constructor with the queue's size must result in an empty queue");

            builder.Level.Should()
                .Be(0, "because the empty builder shouldn't be of any level");
        }
    }
}
