using System.Collections.Generic;
using System.Linq;

using MqttTopicBuilder.Validators.Rules;

namespace MqttTopicBuilder.Validators;

/// <summary>
/// Represent a validation chain's entry point
/// </summary>
/// <typeparam name="T">Type of the value to be validated</typeparam>
public class Validator<T>
{
    /// <summary>
    /// Set of rules to check against the provided value
    /// </summary>
    private readonly IEnumerable<Rule<T>> _rules;

    /// <summary>
    /// Create a new validator with its set of rules
    /// </summary>
    /// <param name="rules">Rules to be validated</param>
    private Validator(params Rule<T>[] rules)
        => _rules = rules;

    /// <summary>
    /// Create a new validator with a set of rules to be executed in the provided order
    /// </summary>
    /// <param name="rules">Rules to be validated</param>
    /// <returns>A new instance of <see cref="Validator{T}"/></returns>
    public static Validator<T> CreatePipelineWith(params Rule<T>[] rules)
        => new(rules);

    /// <summary>
    /// Effective call to validate each of the previously provided rules
    /// </summary>
    /// <param name="value">Value to be validated</param>
    public void Validate(T value)
        => _rules
            .ToList()
            .ForEach(rule => rule.Validate(value));
}
