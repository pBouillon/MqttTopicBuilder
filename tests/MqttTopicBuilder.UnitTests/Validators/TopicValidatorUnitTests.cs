using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.Validators;
using Shouldly;
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

        validatingBlankTopic.ShouldThrow<EmptyTopicException>();
    }

    /// <summary>
    /// Ensure that appending a multi-level wildcard is allowed
    /// </summary>
    [Fact]
    public void ValidateTopicForAppending_OnMultiLevelWildcard()
    {
        var validatingTopic = () => "#".ValidateTopicForAppending();

        validatingTopic.ShouldNotThrow();
    }

    /// <summary>
    /// Ensure that a topic that is not UTF-8 is not valid
    /// </summary>
    [Fact]
    public void ValidateTopicForAppending_OnNonUtf8Topic()
    {
        var validatingNonUtf8Topic = () => "non🌱utf8".ValidateTopicForAppending();

        validatingNonUtf8Topic.ShouldThrow<InvalidTopicException>();
    }

    /// <summary>
    /// Ensure that a single separator is not a valid topic to be appended
    /// </summary>
    [Fact]
    public void ValidateTopicForAppending_OnSeparator()
    {
        var validatingSeparator = () => "/".ValidateTopicForAppending();

        validatingSeparator.ShouldThrow<InvalidTopicException>();
    }

    /// <summary>
    /// Ensure that no more than one wildcard can be used at the time
    /// </summary>
    [Fact]
    public void ValidateTopicForAppending_OnSeveralWildcards()
    {
        var validatingTopic = () => "++".ValidateTopicForAppending();

        validatingTopic.ShouldThrow<InvalidTopicException>("because only one wildcard may be used on a single level");
    }

    /// <summary>
    /// Ensure that a topic can not exceed the size limit
    /// </summary>
    [Fact]
    public void ValidateTopicForAppending_OnTooLongTopic()
    {
        var tooLongTopic = new string('_', Mqtt.Topic.MaxSubTopicLength + 1);

        var validatingTooLongTopic = () => tooLongTopic.ValidateTopicForAppending();

        validatingTooLongTopic.ShouldThrow<TooLongTopicException>();
    }

    /// <summary>
    /// Ensure that appending a single-level wildcard is allowed
    /// </summary>
    [Fact]
    public void ValidateTopicForAppending_OnSingleLevelWildcard()
    {
        var validatingTopic = () => "+".ValidateTopicForAppending();

        validatingTopic.ShouldNotThrow();
    }

    /// <summary>
    /// Ensure that a blank topic is not allowed
    /// </summary>
    [Fact]
    public void ValidateTopic_OnBlankTopic()
    {
        var validatingBlankTopic = () => TopicValidator.ValidateTopic(string.Empty);

        validatingBlankTopic.ShouldThrow<EmptyTopicException>();
    }

    /// <summary>
    /// Ensure that no exceptions are thrown when validating a minimal topic
    /// </summary>
    [Fact]
    public void ValidateTopic_OnMinimalTopic()
    {
        var validatingTopic = () => TopicValidator.ValidateTopic("/");

        validatingTopic.ShouldNotThrow();
    }

    /// <summary>
    /// Ensure that a topic containing non-UTF-8 chars is not valid
    /// </summary>
    [Fact]
    public void ValidateTopic_OnNonUtf8Topic()
    {
        var validatingNonUtf8Topic = () => TopicValidator.ValidateTopic("non/🌱/utf8");

        validatingNonUtf8Topic.ShouldThrow<InvalidTopicException>();
    }

    /// <summary>
    /// Ensure that the multi-level wildcard can not be used at the beginning or at the end
    /// of a topic
    /// </summary>
    [Fact]
    public void ValidateTopic_OnMultiLevelWildcardsBeforeTopicEnd()
    {
        var validatingTopicWithMultiLevelWildcardsBeforeEnd = () => TopicValidator.ValidateTopic("sensors/#/bedroom");

        validatingTopicWithMultiLevelWildcardsBeforeEnd.ShouldThrow<IllegalTopicConstructionException>();
    }

    /// <summary>
    /// Ensure that the multi-level wildcard can't be used more than once
    /// </summary>
    [Fact]
    public void ValidateTopic_OnMultiLevelWildcardsTopic()
    {
        var validatingMultiLevelWildcardTopic = () => TopicValidator.ValidateTopic("sensors/#/bedroom/#");

        validatingMultiLevelWildcardTopic.ShouldThrow<IllegalTopicConstructionException>();
    }

    /// <summary>
    /// Ensure that no exceptions are thrown when validating a valid topic
    /// </summary>
    [Fact]
    public void ValidateTopic_OnValidTopic()
    {
        var validatingMinimalTopic = () => TopicValidator.ValidateTopic("sensors/temperature/bedroom");

        validatingMinimalTopic.ShouldNotThrow();
    }
}
