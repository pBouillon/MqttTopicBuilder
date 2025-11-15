using AutoFixture;

using FluentAssertions;

using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.UnitTests.Utils;
using MqttTopicBuilder.Validators.Rules.RawTopicRules;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.RawTopicRules;

/// <summary>
/// Unit test suite for <see cref="MustRespectWildcardsExclusivity"/>
/// </summary>
public class MustRespectWildcardsExclusivityUnitTests
{
    /// <summary>
    /// Private instance of <see cref="IFixture"/> for test data generation purposes
    /// </summary>
    private static readonly IFixture Fixture = new Fixture();

    public void Validate_OnEmptyTopic()
    {
        var rawTopic = string.Empty;
        var rule = new MustRespectWildcardsExclusivity();

        var validatingRawTopic = () => rule.Validate(rawTopic);

        validatingRawTopic
            .Should()
            .NotThrow<InvalidTopicException>("because an empty conflict does not result in a conflict between wildcards");
    }

    public void Validate_OnMixedWildcards()
    {
        var rawTopic = Mqtt.Wildcard.MultiLevel.ToString() + Mqtt.Wildcard.SingleLevel;
        var rule = new MustRespectWildcardsExclusivity();

        var validatingRawTopic = () => rule.Validate(rawTopic);

        validatingRawTopic
            .Should()
            .Throw<InvalidTopicException>("because mixing wildcards is not allowed");
    }

    public void Validate_OnMultiLevelWildcardOnly()
    {
        var rawTopic = TestUtils.GenerateValidTopic(Fixture.Create<int>())
                       + Mqtt.Topic.Separator + Mqtt.Wildcard.MultiLevel;
        var rule = new MustRespectWildcardsExclusivity();

        var validatingRawTopic = () => rule.Validate(rawTopic);

        validatingRawTopic
            .Should()
            .NotThrow<InvalidTopicException>("because the usage of this wildcard is exclusive");
    }

    public void Validate_OnSingleLevelWildcardOnly()
    {
        var rawTopic = TestUtils.GenerateValidTopic(Fixture.Create<int>())
                       + Mqtt.Topic.Separator + Mqtt.Wildcard.SingleLevel;
        var rule = new MustRespectWildcardsExclusivity();

        var validatingRawTopic = () => rule.Validate(rawTopic);

        validatingRawTopic
            .Should()
            .NotThrow<InvalidTopicException>("because the usage of this wildcard is exclusive");
    }
}
