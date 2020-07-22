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
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions.Classes;
using MqttTopicBuilder.UnitTests.Utils;
using MqttTopicBuilder.Validators.Rules.RawTopicRules;
using System;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.RawTopicRules
{
    /// <summary>
    /// Unit test suite for <see cref="MustHaveAtMostOneMultiLevelWildcard"/>
    /// </summary>
    public class MustHaveAtMostOneMultiLevelWildcardUnitTests
    {
        /// <summary>
        /// Private instance of <see cref="IFixture"/> for test data generation purposes
        /// </summary>
        private static readonly IFixture Fixture = new Fixture();

        /// <summary>
        /// Ensure that a topic containing two or more wildcard will break the rule
        /// </summary>
        [Fact]
        public void Validate_OnMoreThanOneWildcard()
        {
            // Arrange
            var wildcardsCount = Fixture.Create<int>() + 2;
            var rawTopic = new string(Mqtt.Wildcard.MultiLevel, wildcardsCount);
            var rule = new MustHaveAtMostOneMultiLevelWildcard();

            // Act
            Action validatingRawTopic = () =>
                rule.Validate(rawTopic);

            // Assert
            validatingRawTopic.Should()
                .Throw<IllegalTopicConstructionException>(
                    "because there is more than one wildcard, which is forbidden");
        }

        /// <summary>
        /// Ensure that a topic that does not contain any wildcard does not break the rule
        /// </summary>
        [Fact]
        public void Validate_OnNoWildcards()
        {
            // Arrange
            var rawTopic = TestUtils.GenerateSingleValidTopic();
            var rule = new MustHaveAtMostOneMultiLevelWildcard();

            // Act
            Action validatingRawTopic = () =>
                rule.Validate(rawTopic);

            // Assert
            validatingRawTopic.Should()
                .NotThrow<IllegalTopicConstructionException>(
                    "because the wildcard count is not violated");
        }

        /// <summary>
        /// Ensure that a topic containing exactly one wildcard will not break the rule
        /// </summary>
        [Fact]
        public void Validate_OnOneWildcard()
        {
            // Arrange
            var rawTopic = Mqtt.Wildcard.MultiLevel.ToString();
            var rule = new MustHaveAtMostOneMultiLevelWildcard();

            // Act
            Action validatingRawTopic = () =>
                rule.Validate(rawTopic);

            // Assert
            validatingRawTopic.Should()
                .NotThrow<IllegalTopicConstructionException>(
                    "because the wildcard count is not violated");
        }
    }
}
