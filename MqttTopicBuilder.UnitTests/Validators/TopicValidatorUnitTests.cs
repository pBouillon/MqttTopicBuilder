using FluentAssertions;

using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.UnitTests.Utils;
using MqttTopicBuilder.Validators;

using System.Linq;

using Xunit;

namespace MqttTopicBuilder.UnitTests.Validators;

/// <summary>
/// Unit test suite for <see cref="TopicValidator"/>
/// </summary>
public class TopicValidatorUnitTests
{
    /// <summary>
    /// Ensure that a blank topic is not allowed
    /// </summary>
    [Fact]
    public void ValidateTopicForAppending_OnBlankTopic()
    {
        var blankTopic = string.Empty;

        var validatingBlankTopic = () => blankTopic.ValidateTopicForAppending();

        validatingBlankTopic
            .Should()
            .Throw<EmptyTopicException>("because a topic can not be empty");
    }

    /// <summary>
    /// Ensure that appending a multi-level wildcard is allowed
    /// </summary>
    [Fact]
    public void ValidateTopicForAppending_OnMultiLevelWildcard()
    {
        var multiLevelWildcard = Mqtt.Wildcard.MultiLevel.ToString();

        var validatingTopic = () => multiLevelWildcard.ValidateTopicForAppending();

        validatingTopic
            .Should()
            .NotThrow<MqttBaseException>("because a single wildcard is allowed to be appended");
    }

    /// <summary>
    /// Ensure that a topic that is not UTF-8 is not valid
    /// </summary>
    [Fact]
    public void ValidateTopicForAppending_OnNonUtf8Topic()
    {
        const string nonUtf8Topic = "non🌱utf🛠8🐛";

        var validatingNonUtf8Topic = () => nonUtf8Topic.ValidateTopicForAppending();

        validatingNonUtf8Topic.Should().Throw<InvalidTopicException>();
    }

    /// <summary>
    /// Ensure that a single separator is not a valid topic to be appended
    /// </summary>
    [Fact]
    public void ValidateTopicForAppending_OnSeparator()
    {
        var separatorTopic = Mqtt.Topic.Separator.ToString();

        var validatingSeparator = () => separatorTopic.ValidateTopicForAppending();

        validatingSeparator
            .Should()
            .Throw<InvalidTopicException>("because appending the separator will result in an empty level");
    }

    /// <summary>
    /// Ensure that no more than one wildcard can be used at the time
    /// </summary>
    [Fact]
    public void ValidateTopicForAppending_OnSeveralWildcards()
    {
        var mixedWildcards = $"{Mqtt.Wildcard.SingleLevel}{Mqtt.Wildcard.SingleLevel}";

        var validatingTopic = () => mixedWildcards.ValidateTopicForAppending();

        validatingTopic
            .Should()
            .Throw<InvalidTopicException>("because only one wildcard may be used on a single level");
    }

    /// <summary>
    /// Ensure that a topic can not exceed the size limit
    /// </summary>
    [Fact]
    public void ValidateTopicForAppending_OnTooLongTopic()
    {
        var tooLongTopic = new string('_', Mqtt.Topic.MaxSubTopicLength + 1);

        var validatingTooLongTopic = () => tooLongTopic.ValidateTopicForAppending();

        validatingTooLongTopic
            .Should()
            .Throw<TooLongTopicException>("because a topic must not exceed the size limit");
    }

    /// <summary>
    /// Ensure that appending a single-level wildcard is allowed
    /// </summary>
    [Fact]
    public void ValidateTopicForAppending_OnSingleLevelWildcard()
    {
        var singleLevelWildcard = Mqtt.Wildcard.SingleLevel.ToString();

        var validatingTopic = () => singleLevelWildcard.ValidateTopicForAppending();

        validatingTopic
            .Should()
            .NotThrow<MqttBaseException>("because a single wildcard is allowed to be appended");
    }

    /// <summary>
    /// Ensure that a blank topic is not allowed
    /// </summary>
    [Fact]
    public void ValidateTopic_OnBlankTopic()
    {
        var blankTopic = string.Empty;

        var validatingBlankTopic = () => TopicValidator.ValidateTopic(blankTopic);

        validatingBlankTopic
            .Should()
            .Throw<EmptyTopicException>("because a topic can not be empty");
    }

    /// <summary>
    /// Ensure that no exceptions are thrown when validating a minimal topic
    /// </summary>
    [Fact]
    public void ValidateTopic_OnMinimalTopic()
    {
        var minimalValidTopic = Mqtt.Topic.Separator.ToString();

        var validatingTopic = () => TopicValidator.ValidateTopic(minimalValidTopic);

        validatingTopic
            .Should()
            .NotThrow<MqttBaseException>("because the topic is minimal yet allowed");
    }

    /// <summary>
    /// Ensure that a topic containing non-UTF-8 chars is not valid
    /// </summary>
    [Fact]
    public void ValidateTopic_OnNonUtf8Topic()
    {
        const string nonUtf8Topic = "non/🌱/utf/🛠/8/🐛";

        var validatingNonUtf8Topic = () => TopicValidator.ValidateTopic(nonUtf8Topic);

        validatingNonUtf8Topic
            .Should()
            .Throw<InvalidTopicException>();
    }

    /// <summary>
    /// Ensure that the multi-level wildcard can not be used at the beginning or at the end
    /// of a topic
    /// </summary>
    [Fact]
    public void ValidateTopic_OnMultiLevelWildcardsBeforeTopicEnd()
    {
        var topicWithMultiLevelWildcardBeforeEnd = $"{Mqtt.Topic.Separator}{Mqtt.Wildcard.MultiLevel}";
        topicWithMultiLevelWildcardBeforeEnd += $"{Mqtt.Topic.Separator}{TestUtils.GenerateValidTopic()}";

        var validatingTopicWithMultiLevelWildcardsBeforeEnd = () => TopicValidator.ValidateTopic(topicWithMultiLevelWildcardBeforeEnd);

        validatingTopicWithMultiLevelWildcardsBeforeEnd
            .Should()
            .Throw<IllegalTopicConstructionException>(
                    "because a multi-level wildcard may only be used anywhere but at the end of a topic");
    }

    /// <summary>
    /// Ensure that the multi-level wildcard can't be used more than once
    /// </summary>
    [Fact]
    public void ValidateTopic_OnMultiLevelWildcardsTopic()
    {
        var multiLevelWildcardsTopic = TestUtils.GenerateValidTopic(
            Mqtt.Topic.MaximumAllowedLevels - 2);

        multiLevelWildcardsTopic += string.Concat(
            Enumerable.Repeat($"{Mqtt.Topic.Separator}{Mqtt.Wildcard.MultiLevel}", 2));

        var validatingMultiLevelWildcardTopic = () => TopicValidator.ValidateTopic(multiLevelWildcardsTopic);

        validatingMultiLevelWildcardTopic
            .Should()
            .Throw<IllegalTopicConstructionException>();
    }

    /// <summary>
    /// Ensure that no exceptions are thrown when validating a valid topic
    /// </summary>
    [Fact]
    public void ValidateTopic_OnValidTopic()
    {
        var validRawTopic = TestUtils.GenerateValidTopic();

        var validatingMinimalTopic = () => TopicValidator.ValidateTopic(validRawTopic);

        validatingMinimalTopic
            .Should()
            .NotThrow<MqttBaseException>("because the topic is correctly built");
    }
}
