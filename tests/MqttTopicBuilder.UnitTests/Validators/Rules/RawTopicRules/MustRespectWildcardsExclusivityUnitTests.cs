using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.Validators.Rules.RawTopicRules;
using Shouldly;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.RawTopicRules;

/// <summary>
/// Unit test suite for <see cref="MustRespectWildcardsExclusivity"/>
/// </summary>
public class MustRespectWildcardsExclusivityUnitTests
{
    private readonly MustRespectWildcardsExclusivity _rule = new();

    public void Validate_OnEmptyTopic()
    {
        var validatingRawTopic = () => _rule.Validate(string.Empty);

        validatingRawTopic.ShouldNotThrow();
    }

    public void Validate_OnMixedWildcards()
    {
        var validatingRawTopic = () => _rule.Validate("sensors/#/+");

        validatingRawTopic.ShouldThrow<InvalidTopicException>();
    }

    public void Validate_OnMultiLevelWildcardOnly()
    {
        var validatingRawTopic = () => _rule.Validate("sensors/temperature/#");

        validatingRawTopic.ShouldNotThrow();
    }

    public void Validate_OnSingleLevelWildcardOnly()
    {
        var validatingRawTopic = () => _rule.Validate("sensors/+");

        validatingRawTopic.ShouldNotThrow();
    }
}
