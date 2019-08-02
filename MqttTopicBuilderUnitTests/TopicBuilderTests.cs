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
        /// 
        /// </summary>
        private readonly Fixture fixture = new Fixture();

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
                .Be(1,
                    "because exactly one element should have been added");
        }
    }
}
