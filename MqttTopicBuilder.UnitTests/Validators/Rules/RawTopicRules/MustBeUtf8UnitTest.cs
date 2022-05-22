using FluentAssertions;

using MqttTopicBuilder.Exceptions.Classes;
using MqttTopicBuilder.UnitTests.Utils;
using MqttTopicBuilder.Validators.Rules.RawTopicRules;

using System;

using Xunit;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.RawTopicRules;

/// <summary>
/// Unit test suite for <see cref="MustBeUtf8"/>
/// </summary>
public class MustBeUtf8UnitTest
{
    /// <summary>
    /// Ensure that a raw topic not encoded with UTF-8 will not pass the rule
    /// </summary>
    [Fact]
    public void Validate_OnNonUtf8Topic()
    {
        // Arrange
        const string rawTopic = "🚮🕯💻";
        var rule = new MustBeUtf8();

        // Act
        Action validatingRawTopicEncoding = () =>
            rule.Validate(rawTopic);

        // Assert
        validatingRawTopicEncoding.Should()
            .Throw<InvalidTopicException>("because this raw topic is not UTF-8");
    }

    /// <summary>
    /// Ensure that a raw topic encoded with UTF-8 will pass the rule
    /// </summary>
    [Fact]
    public void Validate_OnUtf8Topic()
    {
        // Arrange
        var rawTopic = TestUtils.GenerateSingleValidTopic();
        var rule = new MustBeUtf8();

        // Act
        Action validatingRawTopicEncoding = () =>
            rule.Validate(rawTopic);

        // Assert
        validatingRawTopicEncoding.Should()
            .NotThrow<InvalidTopicException>("because this raw topic is UTF-8");
    }
}