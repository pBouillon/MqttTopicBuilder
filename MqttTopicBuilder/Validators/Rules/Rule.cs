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

namespace MqttTopicBuilder.Validators.Rules
{
    /// <summary>
    /// Represent a unique rule to be validated by the <see cref="Validator{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of the value to be validated</typeparam>
    public abstract class Rule<T>
    {
        /// <summary>
        /// Check if the value is valid in its case
        /// </summary>
        /// <param name="value">Value to be validated</param>
        /// <returns>True if the rule is respected; false otherwise</returns>
        protected abstract bool IsValid(T value);

        /// <summary>
        /// Logic to be executed if <see cref="Rule{T}.IsValid"/> fails
        /// </summary>
        protected abstract void OnError();

        /// <summary>
        /// Performs the effective validation
        /// </summary>
        /// <param name="value">Value to be validated</param>
        public void Validate(T value)
        {
            if (!IsValid(value))
            {
                OnError();
            }
        }
    }
}
