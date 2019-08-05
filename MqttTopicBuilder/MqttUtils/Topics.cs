/**
 * Author
 *      Pierre Bouillon - https://github.com/pBouillon
 *
 * Repository
 *      MqttTopicBuilder - https://github.com/pBouillon/MqttTopicBuilder
 *
 * License
 *      MIT - https://github.com/pBouillon/MqttTopicBuilder/blob/master/LICENSE
 */

namespace MqttTopicBuilder.MqttUtils
{
    /// <summary>
    /// References MQTT topic's constants
    /// </summary>
    public class Topics
    {
        /// <summary>
        /// Default maximum topic length (level)
        /// </summary>
        public const int MaxLength = 32;

        /// <summary>
        /// Default MQTT topic's separator
        /// </summary>
        public const char Separator = '/';
    }
}
