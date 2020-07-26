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

using System.Text;
using MqttTopicBuilder.Constants;
using MqttTopicBuilder.Exceptions.Classes;

namespace MqttTopicBuilder.Validators.Rules.RawTopicRules
{
    /// <summary>
    /// Rule to ensure that a topic is correctly encoded using UTF-8
    /// as specified in the documentation:
    /// https://docs.oasis-open.org/mqtt/mqtt/v5.0/os/mqtt-v5.0-os.html#_UTF-8_Encoded_String
    /// </summary>
    public class MustBeUtf8 : BaseRawTopicRule
    {
        /// <inheritdoc cref="Rule{T}.IsValid"/>
        protected override bool IsValid(string value)
            => Encoding.ASCII.GetByteCount(value) == Encoding.UTF8.GetByteCount(value);

        /// <inheritdoc cref="Rule{T}.OnError"/>
        protected override void OnError()
            => throw new InvalidTopicException(
                $"A topic should not contains the MQTT separator \"{Mqtt.Topic.Separator}\"");
    }
}
