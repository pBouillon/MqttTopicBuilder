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

namespace MqttTopicBuilder.Core.Constants
{
    /// <summary>
    /// References MQTT's wildcard Constants
    /// </summary>
    public class Wildcards
    {
        /// <summary>
        /// MQTT's multi-level wildcard
        /// </summary>
        public const string MultiLevel = "#";

        /// <summary>
        /// MQTT's single-level wildcard
        /// </summary>
        public const string SingleLevel = "+";
    }
}
