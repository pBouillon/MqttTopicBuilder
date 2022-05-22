using System;
using System.Linq;

using AutoFixture;

using MqttTopicBuilder.Constants;

namespace MqttTopicBuilder.UnitTests.Utils;

/// <summary>
/// Provide various methods to ease unit testing
/// </summary>
public static class TestUtils
{
    /// <summary>
    /// Private instance of <see cref="IFixture"/> for test data generation purposes
    /// </summary>
    private static readonly IFixture Fixture = new Fixture();

    /// <summary>
    /// Generate a valid topic with its number of topics up to
    /// <paramref name="levels"/> using <see cref="IFixture"/>
    /// </summary>
    /// <param name="levels">
    /// Number of levels in the generated topic; the default value is the maximum allowed
    /// </param>
    /// <returns>The valid topic generated</returns>
    /// <remarks>
    /// <see cref="IFixture"/> generates <see cref="Guid"/> when generating a new
    /// <see cref="string"/>. Since strings can be used in MQTT topic, the generated
    /// topic will be valid too
    /// </remarks>
    public static string GenerateValidTopic(int levels = Mqtt.Topic.MaximumAllowedLevels - 1)
        => string.Join(Mqtt.Topic.Separator.ToString(),
            Fixture.Create<string[]>().Take(levels));

    /// <summary>
    /// Generates a valid topic of one level
    /// </summary>
    /// <returns>The valid topic generated</returns>
    public static string GenerateSingleValidTopic()
        => GenerateValidTopic(1);
}
