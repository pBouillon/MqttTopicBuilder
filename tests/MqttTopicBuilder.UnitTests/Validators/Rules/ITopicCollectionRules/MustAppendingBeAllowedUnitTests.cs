using FakeItEasy;

using MqttTopicBuilder.Collection;
using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.Validators.Rules.ITopicCollectionRules;
using Shouldly;
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
        var topicCollection = A.Fake<ITopicCollection>();

        A.CallTo(() => topicCollection.IsAppendingAllowed).Returns(true);

        var validatingCollectionAppendingAllowance = () => new MustAppendingBeAllowed().Validate(topicCollection);

        validatingCollectionAppendingAllowance.ShouldNotThrow();
    }

    /// <summary>
    /// Ensure that a collection that is not able to contain another value
    /// will not pass the rule
    /// </summary>
    [Fact]
    public void Validate_OnAppendingNotAllowed()
    {
        var topicCollection = A.Fake<ITopicCollection>();

        A.CallTo(() => topicCollection.IsAppendingAllowed).Returns(false);

        var validatingCollectionAppendingAllowance = () => new MustAppendingBeAllowed().Validate(topicCollection);

        validatingCollectionAppendingAllowance.ShouldThrow<IllegalTopicConstructionException>();
    }
}
