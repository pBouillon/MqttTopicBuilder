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

using System;
using FluentAssertions;
using MqttTopicBuilder.Exceptions.Classes;
using MqttTopicBuilder.UnitTests.Utils;
using MqttTopicBuilder.Validators.Rules.RawTopicRules;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.RawTopicRules
{
    /// <summary>
    /// Unit test suite for <see cref="MustBeUtf8"/>
    /// </summary>
    public class MustBeUtf8UnitTest
    {
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
}
