﻿/*
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
using MqttTopicBuilder.Exceptions.Classes;

namespace MqttTopicBuilder.Validators.Rules.RawTopicRules
{
    /// <summary>
    /// Rule to ensure that a single raw topic does not contains any separator
    /// <see cref="Mqtt.Topic.Separator"/>
    /// </summary>
    public class MustNotHaveSeparator : BaseRawTopicRule
    {
        /// <inheritdoc cref="Rule{T}.IsValid"/>
        protected override bool IsValid(string value)
            => ! value.Contains(Mqtt.Topic.Separator.ToString());

        /// <inheritdoc cref="Rule{T}.OnError"/>
        protected override void OnError()
            => throw new InvalidTopicException(
                $"A topic should not contains the MQTT separator \"{Mqtt.Topic.Separator}\"");
    }
}
