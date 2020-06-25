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
    /// References MQTT topic's constants
    /// </summary>
    public class Topics
    {
        /// <summary>
        /// Default maximum topic length (level)
        /// </summary>
        public const int MaximumAllowedDepth = 32;

        /// <summary>
        /// Default maximum topic slice length (in char)
        /// </summary>
        /// <see href="https://www.ibm.com/support/knowledgecenter/en/SSFKSJ_7.5.0/com.ibm.mq.dev.doc/q029120_.htm"/>
        public const int MaxSliceLength = 10_240;

        /// <summary>
        /// Encoding of the forbidden null character
        /// </summary>
        public const char NullCharacter = (char) 0;

        /// <summary>
        /// Default MQTT topic's separator
        /// </summary>
        public const char Separator = '/';
    }
}
