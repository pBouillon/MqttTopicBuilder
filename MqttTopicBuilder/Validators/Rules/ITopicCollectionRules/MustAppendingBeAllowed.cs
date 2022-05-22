using MqttTopicBuilder.Collection;
using MqttTopicBuilder.Exceptions;

namespace MqttTopicBuilder.Validators.Rules.ITopicCollectionRules;

/// <summary>
/// Rule to ensure that appending a topic to this collection is allowed
/// </summary>
public class MustAppendingBeAllowed : BaseITopicCollectionRule
{
    /// <inheritdoc cref="Rule{T}.IsValid"/>
    protected override bool IsValid(ITopicCollection value)
        => value.IsAppendingAllowed;

    /// <inheritdoc cref="Rule{T}.IsValid"/>
    protected override void OnError()
        => throw new IllegalTopicConstructionException();
}
