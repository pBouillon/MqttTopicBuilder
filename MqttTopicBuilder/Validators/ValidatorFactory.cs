using System;

using MqttTopicBuilder.Collection;
using MqttTopicBuilder.Validators.Rules.ITopicCollectionRules;
using MqttTopicBuilder.Validators.Rules.RawTopicRules;

namespace MqttTopicBuilder.Validators;

/// <summary>
/// Provide pre-built validators
/// </summary>
public static class ValidatorFactory
{
    private static readonly Lazy<Validator<string>> PublishedTopicValidator = new(() 
        => Validator<string>.CreatePipelineWith(new MustNotContainWildcard()));

    private static readonly Lazy<Validator<string>> RawTopicValidator = new(()
        => Validator<string>.CreatePipelineWith(
            new MustNotBeBlank(),
            new MustEndWithMultiLevelWildcardIfAny(),
            new MustHaveAtMostOneMultiLevelWildcard()));

    private static readonly Lazy<Validator<string>> SingleRawTopicValidator = new(()
        => Validator<string>.CreatePipelineWith(
            new MustNotBeBlank(),
            new MustBeUtf8(),
            new MustNotHaveNullChar(),
            new MustNotHaveSeparator(),
            new MustRespectMaximumLength(),
            new MustRespectWildcardsExclusivity()));

    private static readonly Lazy<Validator<ITopicCollection>> TopicCollectionAppendingValidator = new(()
        => Validator<ITopicCollection>.CreatePipelineWith(
            new MustAppendingBeAllowed(),
            new MustNotBeFull()));


    /// <summary>
    /// Get a validator with a set of rule to validate that a topic can be used on PUBLISH mode
    /// </summary>
    /// <returns>The validator to be used</returns>
    public static Validator<string> GetPublishedTopicValidator() 
        => PublishedTopicValidator.Value;

    /// <summary>
    /// Get a validator with a set of rules to validate a topic
    /// </summary>
    /// <returns>The validator to be used</returns>
    /// <remarks>
    /// Does not ensure that the single topics from which the main topic is made of are valid
    /// </remarks>
    public static Validator<string> GetRawTopicValidator()
        => RawTopicValidator.Value;

    /// <summary>
    /// Get a validator with a set of rules to validate raw single topics
    /// </summary>
    /// <returns>The validator to be used</returns>
    public static Validator<string> GetSingleRawTopicValidator()
        => SingleRawTopicValidator.Value;

    /// <summary>
    /// Get a validator with a set of rules to validate the ability for the collection
    /// to hold another topic
    /// </summary>
    /// <returns>The validator to be used</returns>
    public static Validator<ITopicCollection> GetTopicCollectionAppendingValidator()
        => TopicCollectionAppendingValidator.Value;
}
