using AutoFixture;

using FluentAssertions;

using Moq;

using MqttTopicBuilder.Collection;
using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.Validators.Rules.ITopicCollectionRules;

using Xunit;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.ITopicCollectionRules;

/// <summary>
/// Unit test suite for <see cref="MustNotBeFull"/>
/// </summary>
public class MustNotBeFullUnitTests
{
    /// <summary>
    /// Private instance of <see cref="IFixture"/> for test data generation purposes
    /// </summary>
    private static readonly IFixture Fixture = new Fixture();

    /// <summary>
    /// Ensure that a collection that is full
    /// will not pass the rule
    /// </summary>
    [Fact]
    public void Validate_OnFullCollection()
    {
        // Ensure that this level is at least 1
        var maxLevel = Fixture.Create<int>() + 1;
        var level = maxLevel;

        var topicCollectionMock = new Mock<ITopicCollection>();

        topicCollectionMock.Setup(_ => _.Levels)
            .Returns(level);

        topicCollectionMock.Setup(_ => _.MaxLevel)
            .Returns(maxLevel);

        var topicCollection = topicCollectionMock.Object;

        var validatingCollectionStatus = () => new MustNotBeFull()
                .Validate(topicCollection);

        validatingCollectionStatus
            .Should()
            .Throw<TooManyTopicsAppendingException>("because the collection has as many levels as it can contains");
    }

    /// <summary>
    /// Ensure that a collection that is not full will pass the rule
    /// </summary>
    [Fact]
    public void Validate_OnNotFullCollection()
    {
        // Ensure that this level is at least 1
        var maxLevel = Fixture.Create<int>() + 1;
        var level = maxLevel - 1;

        var topicCollectionMock = new Mock<ITopicCollection>();

        topicCollectionMock.Setup(_ => _.Levels)
            .Returns(level);

        topicCollectionMock.Setup(_ => _.MaxLevel)
            .Returns(maxLevel);

        var topicCollection = topicCollectionMock.Object;

        var validatingCollectionStatus = () => new MustNotBeFull()
                .Validate(topicCollection);

        validatingCollectionStatus
            .Should()
            .NotThrow<TooManyTopicsAppendingException>("because the collection has less levels than it can contains");
    }
}
