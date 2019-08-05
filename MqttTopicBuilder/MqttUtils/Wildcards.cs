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
    /// References MQTT's wildcard constants
    /// </summary>
    public class Wildcards
    {
        /// <summary>
        /// MQTT's multi-level wildcard
        /// </summary>
        public const char MultiLevel = '#';

        /// <summary>
        /// MQTT's single-level wildcard
        /// </summary>
        public const char SingleLevel = '+';
    }
}
