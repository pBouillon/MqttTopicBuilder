using MqttTopicBuilder.Collection;
using MqttTopicBuilder.Exceptions;

namespace MqttTopicBuilder.Validators.Rules.ITopicCollectionRules;

/// <summary>
/// Rule to ensure that the collection is able to hold another topic
/// </summary>
public class MustNotBeFull : BaseITopicCollectionRule
{
    /// <inheritdoc cref="Rule{T}.IsValid"/>
    protected override bool IsValid(ITopicCollection value)
        => value.Levels < value.MaxLevel;

    /// <inheritdoc cref="Rule{T}.IsValid"/>
    protected override void OnError()
        => throw new TooManyTopicsAppendingException();
}