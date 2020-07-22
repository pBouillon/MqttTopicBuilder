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

using AutoFixture;
using FluentAssertions;
using MqttTopicBuilder.Exceptions.Classes;
using MqttTopicBuilder.UnitTests.Utils;
using MqttTopicBuilder.Validators.Rules.RawTopicRules;
using System;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.RawTopicRules
{
    /// <summary>
    /// Unit test suite for <see cref="MustNotBeBlank"/>
    /// </summary>
    public class MustNotBeBlankUnitTests
    {
        /// <summary>
        /// Private instance of <see cref="IFixture"/> for test data generation purposes
        /// </summary>
        private static readonly IFixture Fixture = new Fixture();

        /// <summary>
        /// Ensure that a topic containing only whitespaces will break the rule
        /// </summary>
        [Fact]
        public void Validate_OnBlankString()
        {
            // Arrange
            var whitespaceCount = Fixture.Create<int>() + 1;
            var rawTopic = new string(' ', whitespaceCount);

            var rule = new MustNotBeBlank();

            // Act
            Action validatingRawTopic = () =>
                rule.Validate(rawTopic);

            // Assert
            validatingRawTopic.Should()
                .Throw<EmptyTopicException>(
                    "because a blank topic should break the rule");
        }

        /// <summary>
        /// Ensure that an empty topic will break the rule
        /// </summary>
        [Fact]
        public void Validate_OnEmptyString()
        {
            // Arrange
            var rawTopic = string.Empty;

            var rule = new MustNotBeBlank();

            // Act
            Action validatingRawTopic = () =>
                rule.Validate(rawTopic);

            // Assert
            validatingRawTopic.Should()
                .Throw<EmptyTopicException>(
                    "because an empty topic should break the rule");
        }

        /// <summary>
        /// Ensure that a well formed topic will not break the rule
        /// </summary>
        [Fact]
        public void Validate_OnNonEmptyString()
        {
            // Arrange
            var rawTopic = TestUtils.GenerateSingleValidTopic();
            var rule = new MustNotBeBlank();

            // Act
            Action validatingRawTopic = () =>
                rule.Validate(rawTopic);

            // Assert
            validatingRawTopic.Should()
                .NotThrow<EmptyTopicException>(
                    "because a topic with a valid value should not break the rule");
        }

        /// <summary>
        /// Ensure that a null value will break the rule
        /// </summary>
        [Fact]
        public void Validate_OnNullString()
        {
            // Arrange
            string rawTopic = null;

            var rule = new MustNotBeBlank();

            // Act
            // ReSharper disable once ExpressionIsAlwaysNull
            Action validatingRawTopic = () =>
                rule.Validate(rawTopic);

            // Assert
            validatingRawTopic.Should()
                .Throw<EmptyTopicException>(
                    "because an null topic should break the rule");
        }
    }
}
