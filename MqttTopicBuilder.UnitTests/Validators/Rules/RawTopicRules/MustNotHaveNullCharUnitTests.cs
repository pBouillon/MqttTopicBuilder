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

using FluentAssertions;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions.Classes;
using MqttTopicBuilder.UnitTests.Utils;
using MqttTopicBuilder.Validators.Rules.RawTopicRules;
using System;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.RawTopicRules
{
    /// <summary>
    /// Unit test suite for <see cref="MustNotHaveNullChar"/>
    /// </summary>
    public class MustNotHaveNullCharUnitTests
    {
        /// <summary>
        /// Ensure that a topic that does not use any null char will pass the rule
        /// </summary>
        [Fact]
        public void Validate_OnNoNullChar()
        {
            // Arrange
            var rawTopic = TestUtils.GenerateSingleValidTopic();
            var rule = new MustNotHaveNullChar();

            // Act
            Action validatingRawTopic = () =>
                rule.Validate(rawTopic);

            // Assert
            validatingRawTopic.Should()
                .NotThrow<IllegalTopicConstructionException>(
                    "because the raw topic is correctly formed");
        }

        /// <summary>
        /// Ensure that a topic made of a null char will break the rule
        /// </summary>
        [Fact]
        public void Validate_OnNullChar()
        {
            // Arrange
            var rawTopic = Mqtt.Topic.NullCharacter.ToString();
            var rule = new MustNotHaveNullChar();

            // Act
            Action validatingRawTopic = () =>
                rule.Validate(rawTopic);

            // Assert
            validatingRawTopic.Should()
                .Throw<IllegalTopicConstructionException>(
                    "because a topic using the null char is not valid");
        }

        /// <summary>
        /// Ensure that a topic containing a null char will break the rule
        /// </summary>
        [Fact]
        public void Validate_OnNullCharAmongTopic()
        {
            // Arrange
            var rawTopic = TestUtils.GenerateSingleValidTopic() 
                           + Mqtt.Topic.NullCharacter + TestUtils.GenerateSingleValidTopic();
            var rule = new MustNotHaveNullChar();

            // Act
            Action validatingRawTopic = () =>
                rule.Validate(rawTopic);

            // Assert
            validatingRawTopic.Should()
                .Throw<IllegalTopicConstructionException>(
                    "because a topic using the null char is not valid");
        }
    }
}
