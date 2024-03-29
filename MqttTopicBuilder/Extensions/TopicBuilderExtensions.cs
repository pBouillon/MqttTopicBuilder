using MqttTopicBuilder.Builder;

namespace MqttTopicBuilder.Extensions;

/// <summary>
/// Various extensions on <see cref="ITopicBuilder"/>
/// </summary>
public static class TopicBuilderExtensions
{
    /// <summary>
    /// Get an instance of a <see cref="ITopicBuilder"/> in <see cref="Consumer.Publisher"/> mode
    /// from an existing builder
    /// </summary>
    /// <param name="builder">Builder to convert</param>
    /// <returns>A new builder whose consumer is <see cref="Consumer.Publisher"/></returns>
    /// <remarks>
    /// If the builder's consumer already is <see cref="Consumer.Publisher"/>, this will return a clone
    /// of the provided <paramref name="builder"/>
    /// </remarks>
    public static ITopicBuilder ToPublisherBuilder(this ITopicBuilder builder)
        => Convert(builder, Consumer.Publisher);

    /// <summary>
    /// Get an instance of a <see cref="ITopicBuilder"/> in <see cref="Consumer.Subscriber"/> mode
    /// from an existing builder
    /// </summary>
    /// <param name="builder">Builder to convert</param>
    /// <returns>A new builder whose consumer is <see cref="Consumer.Subscriber"/></returns>
    /// <remarks>
    /// If the builder's consumer already is <see cref="Consumer.Subscriber"/>, this will return a clone
    /// of the provided <paramref name="builder"/>
    /// </remarks>
    public static ITopicBuilder ToSubscriberBuilder(this ITopicBuilder builder)
        => Convert(builder, Consumer.Subscriber);

    /// <summary>
    /// Get an instance of a <see cref="ITopicBuilder"/> in the desired <paramref name="target"/> mode
    /// from an existing builder
    /// </summary>
    /// <param name="builder">Builder to convert</param>
    /// <param name="target">Mode in which converting the target</param>
    /// <returns>A new builder whose consumer is <paramref name="target"/></returns>
    /// <remarks>
    /// If the builder's consumer already is in <paramref name="target"/> mode, this will return a clone
    /// of the provided <paramref name="builder"/>
    /// </remarks>
    private static ITopicBuilder Convert(ITopicBuilder builder, Consumer target)
        => builder.Consumer == target
            ? builder.Clone()
            : new TopicBuilder(builder.TopicCollection, target);
}
