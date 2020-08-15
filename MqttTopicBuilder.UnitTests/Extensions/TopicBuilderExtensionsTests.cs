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

using MqttTopicBuilder.Builder;
using MqttTopicBuilder.Extensions;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Extensions
{
    /// <summary>
    /// Unit test suite for <see cref="TopicBuilderExtensions"/>
    /// </summary>
    public class TopicBuilderExtensionsTests
    {
        /// <summary>
        /// Ensure the behavior of a conversion of a <see cref="ITopicBuilder"/> whose consumer is
        /// <see cref="TopicConsumer.Publisher"/>
        /// </summary>
        [Fact]
        public void ToSubscriberBuilder_FromPublisherBuilder() { }

        /// <summary>
        /// Ensure the behavior of a conversion of a <see cref="ITopicBuilder"/> whose consumer is
        /// <see cref="TopicConsumer.Subscriber"/>
        /// </summary>
        [Fact]
        public void ToSubscriberBuilder_FromSubscriberBuilder() { }
    }
}
