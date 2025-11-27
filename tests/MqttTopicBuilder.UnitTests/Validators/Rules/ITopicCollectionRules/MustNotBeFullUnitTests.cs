using FakeItEasy;

using MqttTopicBuilder.Collection;
using MqttTopicBuilder.Exceptions;
using MqttTopicBuilder.Validators.Rules.ITopicCollectionRules;
using Shouldly;
using Xunit;

namespace MqttTopicBuilder.UnitTests.Validators.Rules.ITopicCollectionRules;

/// <summary>
/// Unit test suite for <see cref="MustNotBeFull"/>
/// </summary>
public class MustNotBeFullUnitTests
{

    /// <summary>
    /// Ensure that a collection that is full
    /// will not pass the rule
    /// </summary>
    [Fact]
    public void Validate_OnFullCollection()
    {
        var topicCollection = A.Fake<ITopicCollection>();

        A.CallTo(() => topicCollection.Levels).Returns(3);
        A.CallTo(() => topicCollection.MaxLevel).Returns(3);

        var validatingCollectionStatus = () => new MustNotBeFull().Validate(topicCollection);

        validatingCollectionStatus.ShouldThrow<TooManyTopicsAppendingException>();
    }


    /// <summary>
    /// Ensure that a collection that is not full will pass the rule
    /// </summary>
    [Fact]
    public void Validate_OnNotFullCollection()
    {
        var topicCollection = A.Fake<ITopicCollection>();

        A.CallTo(() => topicCollection.Levels).Returns(3);
        A.CallTo(() => topicCollection.MaxLevel).Returns(5);

        var validatingCollectionStatus = () => new MustNotBeFull().Validate(topicCollection);

        validatingCollectionStatus.ShouldNotThrow();
    }
}
