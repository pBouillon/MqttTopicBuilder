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

using TinyValidator.Abstractions;

namespace MqttTopicBuilder.Validators.Rules.RawTopicRules
{
    /// <summary>
    /// Set of rules to be applied to raw (string) topics
    /// </summary>
    /// <inheritdoc cref="Rule{T}"/>
    public abstract class BaseRawTopicRule : Rule<string> { }
}
