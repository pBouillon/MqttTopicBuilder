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

using MqttTopicBuilder.Validators.Rules;

namespace MqttTopicBuilder.Validators
{
    /// <summary>
    /// Provide pre-built validators
    /// </summary>
    public static class ValidatorFactory
    {
        /// <summary>
        /// Get a validator with a set of rules to validate a topic
        /// </summary>
        /// <returns>The validator to be used</returns>
        /// <remarks>
        /// Does not ensure that the single topics from which the main topic is made of are valid
        /// </remarks>
        public static Validator<string> GetRawTopicValidator()
            => Validator<string>
                .ForRulesInOrder(
                    new MustNotBeBlank(),
                    new MustEndWithMultiLevelWildcardIfAny(),
                    new MustHaveAtMostOneMultiLevelWildcard());

        /// <summary>
        /// Get a validator with a set of rules to validate raw single topics
        /// </summary>
        /// <returns>The validator to be used</returns>
        public static Validator<string> GetSingleRawTopicValidator()
            => Validator<string>
                .ForRulesInOrder(
                    new MustNotBeBlank(),
                    new MustBeUtf8(),
                    new MustNotHaveNullChar(),
                    new MustNotHaveSeparator(),
                    new MustRespectMaximumLength(),
                    new MustRespectWildcardsExclusivity());
    }
}
