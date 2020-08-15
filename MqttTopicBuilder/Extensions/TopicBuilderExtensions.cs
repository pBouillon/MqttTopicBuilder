using MqttTopicBuilder.Builder;

namespace MqttTopicBuilder.Extensions
{
    /// <summary>
    /// Various extensions on <see cref="ITopicBuilder"/>
    /// </summary>
    public static class TopicBuilderExtensions
    {
        /// <summary>
        /// Get an instance of a <see cref="ITopicBuilder"/> in <see cref="TopicConsumer.Subscriber"/> mode
        /// from an existing builder
        /// </summary>
        /// <param name="builder">Builder to convert</param>
        /// <returns>A new builder whose consumer is <see cref="TopicConsumer.Subscriber"/></returns>
        /// <remarks>
        /// If the builder's consumer already is <see cref="TopicConsumer.Subscriber"/>, this will return a clone
        /// of the provided <paramref name="builder"/>
        /// </remarks>
        public static ITopicBuilder ToSubscriberBuilder(this ITopicBuilder builder)
            => builder.Consumer == TopicConsumer.Subscriber
                // If the builder is already in the right state, return its clone
                ? builder.Clone() 
                // ReSharper disable once RedundantArgumentDefaultValue
                : new TopicBuilder(builder.TopicCollection, TopicConsumer.Subscriber);
    }
}
