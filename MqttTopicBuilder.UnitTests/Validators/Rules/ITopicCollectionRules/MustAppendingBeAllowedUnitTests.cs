using FluentAssertions;

using Moq;

using MqttTopicBuilder.Collection;
using MqttTopicBuilder.Validators.Rules.ITopicCollectionRules;

using System;
using MqttTopicBuilder.Exceptions;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.ITopicCollectionRules;

/// <summary>
/// Unit test suite for <see cref="MustAppendingBeAllowed"/>
/// </summary>
public class MustAppendingBeAllowedUnitTests
{
    /// <summary>
    /// Ensure that a collection that is able to contain another value
    /// will pass the rule
    /// </summary>
    [Fact]
    public void Validate_OnAppendingAllowed()
    {
        // Arrange
        var topicCollectionMock = new Mock<ITopicCollection>();
        topicCollectionMock.Setup(_ => _.IsAppendingAllowed)
            .Returns(true);

        var topicCollection = topicCollectionMock.Object;

        // Act
        Action validatingCollectionAppendingAllowance = () =>
            new MustAppendingBeAllowed()
                .Validate(topicCollection);

        // Assert
        validatingCollectionAppendingAllowance.Should()
            .NotThrow<IllegalTopicConstructionException>(
                "because the collection can accept another topic");
    }

    /// <summary>
    /// Ensure that a collection that is not able to contain another value
    /// will not pass the rule
    /// </summary>
    [Fact]
    public void Validate_OnAppendingNotAllowed()
    {
        // Arrange
        var topicCollectionMock = new Mock<ITopicCollection>();
        topicCollectionMock.Setup(_ => _.IsAppendingAllowed)
            .Returns(false);

        var topicCollection = topicCollectionMock.Object;

        // Act
        Action validatingCollectionAppendingAllowance = () =>
            new MustAppendingBeAllowed()
                .Validate(topicCollection);

        // Assert
        validatingCollectionAppendingAllowance.Should()
            .Throw<IllegalTopicConstructionException>(
                "because the collection cannot accept another topic");
    }
}