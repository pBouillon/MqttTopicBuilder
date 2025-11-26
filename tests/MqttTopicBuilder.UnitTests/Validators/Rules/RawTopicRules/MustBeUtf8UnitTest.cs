using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.Validators.Rules.RawTopicRules;
using Shouldly;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.RawTopicRules;

/// <summary>
/// Unit test suite for <see cref="MustBeUtf8"/>
/// </summary>
public class MustBeUtf8UnitTest
{
    private readonly MustBeUtf8 _rule = new();

    /// <summary>
    /// Ensure that a raw topic not encoded with UTF-8 will not pass the rule
    /// </summary>
    [Fact]
    public void Validate_OnNonUtf8Topic()
    {
        var validatingRawTopicEncoding = () => _rule.Validate("🚮🕯💻");

        validatingRawTopicEncoding.ShouldThrow<InvalidTopicException>();
    }

    /// <summary>
    /// Ensure that a raw topic encoded with UTF-8 will pass the rule
    /// </summary>
    [Fact]
    public void Validate_OnUtf8Topic()
    {
        var validatingRawTopicEncoding = () => _rule.Validate("sensors/bedroom/temperature");

        validatingRawTopicEncoding.ShouldNotThrow();
    }
}
