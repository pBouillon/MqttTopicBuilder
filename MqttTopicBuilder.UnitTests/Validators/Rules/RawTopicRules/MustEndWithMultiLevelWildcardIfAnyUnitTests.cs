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

using MqttTopicBuilder.Constants;
using MqttTopicBuilder.UnitTests.Utils;
using System;
using FluentAssertions;
using MqttTopicBuilder.Exceptions.Classes;
using MqttTopicBuilder.Validators.Rules.RawTopicRules;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.RawTopicRules
{
    /// <summary>
    /// Unit test suite for <see cref="MustEndWithMultiLevelWildcardIfAny"/>
    /// </summary>
    public class MustEndWithMultiLevelWildcardIfAnyUnitTests
    {
        [Fact]
        public void Validate_RawTopicWithSeveralWildcards()
        {
            // Arrange
            var topic = TestUtils.GenerateSingleValidTopic() + Mqtt.Topic.Separator
                 + Mqtt.Wildcard.MultiLevel + Mqtt.Topic.Separator
                 + Mqtt.Wildcard.MultiLevel;

            var rule = new MustEndWithMultiLevelWildcardIfAny();

            // Act
            Action validatingRawTopic = () =>
                rule.Validate(topic);

            // Assert
            validatingRawTopic.Should()
                .Throw<IllegalTopicConstructionException>(
                    "because at most one multi-level wildcard is allowed");
        }

        [Fact]
        public void Validate_RawTopicWithWildcardAndSlashAsLastChars()
        {
            // Arrange
            var topic = TestUtils.GenerateSingleValidTopic() + Mqtt.Topic.Separator
                + Mqtt.Wildcard.MultiLevel + Mqtt.Topic.Separator;

            var rule = new MustEndWithMultiLevelWildcardIfAny();

            // Act
            Action validatingRawTopic = () =>
                rule.Validate(topic);

            // Assert
            validatingRawTopic.Should()
                .NotThrow<IllegalTopicConstructionException>(
                    "because a final separator should not make the test fail");
        }

        [Fact]
        public void Validate_RawTopicWithWildcardAsLastChar()
        {
            // Arrange
            var topic = TestUtils.GenerateSingleValidTopic() + Mqtt.Topic.Separator
                + Mqtt.Wildcard.MultiLevel;

            var rule = new MustEndWithMultiLevelWildcardIfAny();

            // Act
            Action validatingRawTopic = () =>
                rule.Validate(topic);

            // Assert
            validatingRawTopic.Should()
                .NotThrow<IllegalTopicConstructionException>(
                    "because ending a topic with a multi-level wildcard is a valid behaviour");
        }

        [Fact]
        public void Validate_RawTopicWithWildcardInItsMiddle()
        {
            // Arrange
            var topic = TestUtils.GenerateSingleValidTopic() + Mqtt.Topic.Separator
                 + Mqtt.Wildcard.MultiLevel + Mqtt.Topic.Separator
                 + TestUtils.GenerateSingleValidTopic();

            var rule = new MustEndWithMultiLevelWildcardIfAny();

            // Act
            Action validatingRawTopic = () =>
                rule.Validate(topic);

            // Assert
            validatingRawTopic.Should()
                .Throw<IllegalTopicConstructionException>(
                    "because a multi-level wildcard is allowed only at the end of the topic");
        }
    }
}
