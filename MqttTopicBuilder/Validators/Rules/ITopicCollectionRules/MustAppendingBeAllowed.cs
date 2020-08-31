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

using MqttTopicBuilder.Collection;
using MqttTopicBuilder.Exceptions.Classes;
using TinyValidator.Abstractions;

namespace MqttTopicBuilder.Validators.Rules.ITopicCollectionRules
{
    /// <summary>
    /// Rule to ensure that appending a topic to this collection is allowed
    /// </summary>
    public class MustAppendingBeAllowed : BaseITopicCollectionRule
    {
        /// <inheritdoc cref="Rule{T}.IsValidWhen"/>
        protected override bool IsValidWhen(ITopicCollection value)
            => value.IsAppendingAllowed;

        /// <inheritdoc cref="Rule{T}.OnInvalid"/>
        protected override void OnInvalid()
            => throw new IllegalTopicConstructionException();
    }
}
