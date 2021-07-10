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

namespace MqttTopicBuilder.Builder
{
    /// <summary>
    /// Represent the different possible consumers of a topic to be build
    /// using <see cref="ITopicBuilder"/>
    /// </summary>
    public enum TopicConsumer
    {
        /// <summary>
        /// Indicate that the topic is intended to be used on an MQTT PUBLISH
        /// </summary>
        /// <remarks>
        /// When on PUBLISH mode, MQTT does not allow for wildcards to be used
        /// </remarks>
        Publisher,

        /// <summary>
        /// Indicate that the topic is intended to be used on an MQTT SUBSCRIBE
        /// </summary>
        Subscriber,
    }
}
