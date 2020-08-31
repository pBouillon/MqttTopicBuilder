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
using TinyValidator.Abstractions;

namespace MqttTopicBuilder.Validators.Rules.ITopicCollectionRules
{
    /// <summary>
    /// Set of rules to be applied to <see cref="ITopicCollection"/>
    /// </summary>
    /// <inheritdoc cref="Rule{T}"/>
    public abstract class BaseITopicCollectionRule : Rule<ITopicCollection> { }
}
