﻿/**
 * Author
 *      Pierre Bouillon - https://github.com/pBouillon
 *
 * Repository
 *      MqttTopicBuilder - https://github.com/pBouillon/MqttTopicBuilder
 *
 * License
 *      MIT - https://github.com/pBouillon/MqttTopicBuilder/blob/master/LICENSE
 */

namespace MqttTopicBuilderUnitTests
{
    using AutoFixture;
    using FluentAssertions;
    using MqttTopicBuilder;
    using MqttTopicBuilder.Exceptions;
    using MqttTopicBuilder.Constants;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xunit;

    public class TopicBuilderTests
    {
        /// <summary>
        /// AutoFixture's object to generate fixtures
        /// </summary>
        /// <see cref="Fixture"/>
        private readonly Fixture _fixture = new Fixture();

        /// <summary>
        /// Check if an exception is correctly raised on the addition of a blank topic
        /// </summary>
        [Fact]
        public void TopicBuilder_AddTopic_BlankTopic()
        {
            // Arrange
            const string topic = " ";
            var builder = new TopicBuilder();

            // Act
            Action addBlankTopic = ()
                => builder.AddTopic(topic);

            // Assert
            addBlankTopic.Should()
                .Throw<EmptyTopicException>(
                    "because a topic made of spaces added should result in an exception");
        }

        /// <summary>
        /// Check if an exception is correctly raised on the addition of an empty topic
        /// </summary>
        [Fact]
        public void TopicBuilder_AddTopic_EmptyTopic()
        {
            // Arrange
            var topic = string.Empty;
            var builder = new TopicBuilder();

            // Act
            Action addEmptyTopic = ()
                => builder.AddTopic(topic);

            // Assert
            addEmptyTopic.Should()
                .Throw<EmptyTopicException>(
                    "because an empty topic added should result in an exception");
        }

        /// <summary>
        /// Check if an exception is correctly raised on the addition of a topic after the builder locked it
        /// </summary>
        [Fact]
        public void TopicBuilder_AddTopic_IllegalTopicAppending()
        {
            // Arrange
            var builder = new TopicBuilder();

            // Act
            Action illegalTopicAppending = ()
                => builder.AddTopic(_fixture.Create<string>())
                    // appending should be forbidden after here
                    .AddWildcardMultiLevel() 
                    .AddTopic(_fixture.Create<string>());

            // Assert
            illegalTopicAppending.Should()
                .Throw<IllegalTopicConstructionException>(
                    "because a topic added after a multi level wildcard should result in an exception");
        }

        /// <summary>
        /// Check if an exception is correctly raised when adding the <see cref="Topics.NullCharacter"/> in a topic
        /// </summary>
        [Fact]
        public void TopicBuilder_AddTopic_NullCharacter()
        {
            // Arrange
            var builder = new TopicBuilder();
            var invalidTopic = _fixture.Create<string>() 
                        + Topics.NullCharacter 
                        + _fixture.Create<string>();

            // Act
            Action addingANewCharacterInATopic = ()
                => builder.AddTopic(invalidTopic);

            // Assert
            addingANewCharacterInATopic.Should()
                .Throw<EmptyTopicException>(
                    "because a valid topic should not contain the null character");
        }

        /// <summary>
        /// Check if an exception is correctly raised when adding the <see cref="Topics.NullCharacter"/> as a topic
        /// </summary>
        [Fact]
        public void TopicBuilder_AddTopic_NullCharacterAlone()
        {
            // Arrange
            var builder = new TopicBuilder();
            const char nullChar = Topics.NullCharacter;

            // Act
            Action addingANewCharacterInATopic = ()
                => builder.AddTopic(nullChar.ToString());

            // Assert
            addingANewCharacterInATopic.Should()
                .Throw<EmptyTopicException>(
                    "because a valid topic should not contain the null character");
        }

        /// <summary>
        /// Check if an exception is correctly raised when adding too much data to the builder
        /// </summary>
        [Fact]
        public void TopicBuilder_AddTopic_OverflowedTopicBuilder()
        {
            // Arrange
            var fixture = new Fixture();

            var topicDepth = fixture.Create<int>();
            topicDepth = topicDepth > Topics.MaxDepth
                ? Topics.MaxDepth
                : topicDepth;

            var builder = new TopicBuilder(topicDepth);

            // Act
            Action overflowingBuilder = ()
                =>
            {
                for (var i = 0; i < topicDepth + 1; ++i)
                {
                    builder.AddTopic(fixture.Create<string>());
                }
            };

            // Assert
            overflowingBuilder.Should()
                .Throw<TopicBuilderOverflowException>(
                    "because the user should not b able to add more topic than the limit");
        }

        /// <summary>
        /// Check the builder's ability to add a valid topic
        /// </summary>
        [Fact]
        public void TopicBuilder_AddTopic_ValidSingleLevelTopic()
        {
            // Arrange
            var topic = _fixture.Create<string>();
            topic = Topic.Normalize(topic);

            var builder = new TopicBuilder();

            // Act
            builder.AddTopic(topic);

            // Assert
            builder.Level.Should()
                .Be(1, "because exactly one element should have been added");
        }

        /// <summary>
        /// Check if an exception is correctly raised on the addition of a topic containing a separator
        /// </summary>
        [Fact]
        public void TopicBuilder_AddTopic_WithSeparator()
        {
            // Arrange
            var topic = new StringBuilder()
                .Append(_fixture.Create<string>())
                .Append(Topics.Separator)
                .Append(_fixture.Create<string>())
                .ToString();
            var builder = new TopicBuilder();
            
            // Act
            Action addTopicWithSeparator = ()
                => builder.AddTopic(topic);

            // Assert
            addTopicWithSeparator.Should()
                .Throw<InvalidTopicException>(
                    "because a topic containing a topic separator added should result in an exception.");
        }

        /// <summary>
        /// Check if an exception is correctly raised on the addition of a topic containing a wildcard (multi level)
        /// </summary>
        [Fact]
        public void TopicBuilder_AddTopic_WithWildcardMultiLevel()
        {
            // Arrange
            var topic = new StringBuilder()
                .Append(_fixture.Create<string>())
                .Append(Wildcards.MultiLevel)
                .Append(_fixture.Create<string>())
                .ToString();
            var builder = new TopicBuilder();

            // Act
            Action addTopicWithSeparator = ()
                => builder.AddTopic(topic);

            // Assert
            addTopicWithSeparator.Should()
                .Throw<InvalidTopicException>(
                    "because a topic containing a wildcard (multi level) should result in an exception.");
        }

        /// <summary>
        /// Check if an exception is correctly raised on the addition of a topic containing a wildcard (single level)
        /// </summary>
        [Fact]
        public void TopicBuilder_AddTopic_WithWildcardSingleLevel()
        {
            // Arrange
            var topic = new StringBuilder()
                .Append(_fixture.Create<string>())
                .Append(Wildcards.SingleLevel)
                .Append(_fixture.Create<string>())
                .ToString();
            var builder = new TopicBuilder();

            // Act
            Action addTopicWithSeparator = ()
                => builder.AddTopic(topic);

            // Assert
            addTopicWithSeparator.Should()
                .Throw<InvalidTopicException>(
                    "because a topic containing a wildcard (single level) should result in an exception.");
        }

        /// <summary>
        /// Check if single level wildcard appending is correctly appended
        /// </summary>
        [Fact]
        public void TopicBuilder_AddWildcardSingleLevel_AddToEmptyBuilder()
        {
            // Arrange
            var builder = new TopicBuilder();

            // Act
            builder.AddWildcardSingleLevel();

            // Assert
            builder.Level.Should()
                .Be(1,
                    "because we only append data once");

            builder.Build().Path.Should()
                .Be(Wildcards.SingleLevel.ToString(),
                    "because the wildcard must have been used in this macro");
        }

        /// <summary>
        /// Check if the addition of a single level wildcard correctly raise an exception after a multi level wildcard
        /// </summary>
        [Fact]
        public void TopicBuilder_AddWildcardSingleLevel_BlockAdditionAfterMultiLevelWildcard()
        {
            // Arrange
            var builder = new TopicBuilder();
            builder.AddWildcardMultiLevel();

            // Act
            Action appendingAfterMultiLevelWildcard = ()
                => builder.AddWildcardSingleLevel();

            // Assert
            appendingAfterMultiLevelWildcard.Should()
                .Throw<IllegalTopicConstructionException>(
                    "because no topics should ever be append after a multi level wildcard");
        }

        /// <summary>
        /// Check if the smallest topic is built when the builder does not contains any data
        /// </summary>
        [Fact]
        public void TopicBuilder_Build_EmptyTopicBuilder()
        {
            // Arrange
            var builder = new TopicBuilder();

            // Act
            var result = builder.Build();

            // Assert
            result.Path.Should()
                .Be(Topics.Separator.ToString(),
                    "because a builder with no topic staged should build the smallest one, even if '/' as a topic is deprecated");
        }

        /// <summary>
        /// Check the builder's ability to generate a valid topic
        /// </summary>
        [Fact]
        public void TopicBuilder_Build_ValidMultipleLevelsTopic()
        {
            // Arrange
            var builder = new TopicBuilder();
            
            var topics = new Queue<string>();

            var topicDepth = _fixture.Create<int>();
            if (topicDepth > Topics.MaxDepth)
            {
                topicDepth = Topics.MaxDepth;
            }

            for (var i = 0; i < topicDepth; ++i)
            {
                topics.Enqueue(_fixture.Create<string>());
            }

            var expectedTopic = string.Join(Topics.Separator, topics);

            // Act
            foreach (var topic in topics)
            {
                builder.AddTopic(topic);
            }

            // Assert
            builder.Level.Should()
                .Be(builder.Build().Level, 
                    "because there should be as much elements as elements added in the builder")
                .And
                .Be(topicDepth,
                    "because there should be as much elements as elements added");

            builder.Build().Path.Should()
                .Be(expectedTopic,
                    "because appending simple topics should be them of joint by the separator");
        }

        /// <summary>
        /// Ensure that the topic is correctly built from a collection
        /// </summary>
        [Fact]
        public void Topic_Clear()
        {
            // Arrange
            var topicDepth = _fixture.Create<int>();
            if (topicDepth > Topics.MaxDepth)
            {
                topicDepth = Topics.MaxDepth;
            }

            var topicBuilder = new TopicBuilder();
            for (var i = 0; i < topicDepth; ++i)
            {
                topicBuilder.AddTopic(_fixture.Create<string>());
            }

            // Act
            topicBuilder.Clear();

            // Assert
            topicBuilder.Level.Should()
                .Be(0,
                    "because all staged topics should have been cleared");

            topicBuilder.IsEmpty
                .Should()
                .BeTrue("because all staged topics should have been cleared");
        }

        /// <summary>
        /// Check the initializations when the default constructor is called
        /// </summary>
        [Fact]
        public void TopicBuilder_Constructor_Default()
        {
            // Arrange + Act
            var builder = new TopicBuilder();
            
            // Assert
            builder.IsEmpty.Should()
                .BeTrue("because invoke the default constructor with no parameters must result in an empty queue");

            builder.Level.Should()
                .Be(0, "because the empty builder shouldn't be of any level");
        }

        /// <summary>
        /// Ensure that the topic is correctly built from a collection
        /// </summary>
        [Fact]
        public void Topic_Constructor_CreateTopicFromValidCollection()
        {
            // Arrange
            var topicDepth = _fixture.Create<int>();
            if (topicDepth > Topics.MaxDepth)
            {
                topicDepth = Topics.MaxDepth;
            }

            var topicsCollection = new Queue<string>();
            for (var i = 0; i < topicDepth; ++i)
            {
                topicsCollection.Enqueue(_fixture.Create<string>());
            }

            // Act
            var topicBuilder = new TopicBuilder(topicsCollection);

            // Assert
            topicBuilder.Level.Should()
                .Be(topicDepth,
                    "because there should be as much topic level as elements in the original collection");

            topicBuilder.StagedTopics.Should()
                .ContainInOrder(topicsCollection,
                    "because the original collection must be transposed without modifications in the builder");
        }

        /// <summary>
        /// Check the initializations when the constructor is called with a specified stack size
        /// </summary>
        [Fact]
        public void TopicBuilder_Constructor_SpecifiedQueueSize()
        {
            // Arrange
            var queueSize = _fixture.Create<int>();

            // Act
            var builder = new TopicBuilder(queueSize);

            // Assert
            builder.IsEmpty.Should()
                .BeTrue("because invoke the default constructor with the queue's size must result in an empty queue");

            builder.Level.Should()
                .Be(0, "because the empty builder shouldn't be of any level");
        }
    }
}
