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

namespace MqttTopicBuilder.Constants
{
    /// <summary>
    /// Hold MQTT constants
    /// </summary>
    public class Mqtt
    {
        /// <summary>
        /// References MQTT topic constants
        /// </summary>
        public class Topic
        {
            /// <summary>
            /// Default maximum topic length (level)
            /// </summary>
            public const int MaximumAllowedLevels = 32;

            /// <summary>
            /// Default maximum topic slice length (in char)
            /// </summary>
            /// <see href="https://www.ibm.com/support/knowledgecenter/en/SSFKSJ_7.5.0/com.ibm.mq.dev.doc/q029120_.htm"/>
            /// <remarks>
            /// A topic "slice" is a part of an MQTT topic between two separators. 
            /// e.g: in "a/b/c", "b" is a "slice" of the topic
            /// </remarks>
            public const int MaxSubTopicLength = 10_240;

            /// <summary>
            /// Encoding of the forbidden null character
            /// </summary>
            public const char NullCharacter = (char) 0;

            /// <summary>
            /// Default MQTT topic's separator
            /// </summary>
            public const char Separator = '/';
        }

        /// <summary>
        /// References MQTT topic wildcards
        /// </summary>
        public class Wildcard
        {
            /// <summary>
            /// MQTT multi-level wildcard
            /// </summary>
            public const char MultiLevel = '#';

            /// <summary>
            /// MQTT single-level wildcard
            /// </summary>
            public const char SingleLevel = '+';
        }
    }
}
