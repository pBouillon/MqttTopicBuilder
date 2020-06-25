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

using MqttTopicBuilder.Core.Constants;

namespace MqttTopicBuilder.Core
{
    /// <summary>
    /// Stores data and logic belonging to MQTT topics
    /// </summary>
    public class Topic
    {
        /// <summary>
        /// Check if the topic is empty
        /// </summary>
        public bool IsPathEmpty
            => Path.Length == 0;

        /// <summary>
        /// Get the path level (0 if empty)
        /// </summary>
        public int Level
            => IsPathEmpty
                ? 0
                : Path.Split(Topics.Separator).Length;

        /// <summary>
        /// Get the MQTT topic path
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="rawTopic">The raw MQTT topic</param>
        public Topic(string rawTopic)
        {
            EnsureRawTopicValidity(rawTopic);

            Path = rawTopic;
        }

        /// <summary>
        /// Checks if the raw topic provided is a valid topic or not
        ///
        /// Checks are:
        /// - No addition after a global wildcard <see cref="Wildcards.MultiLevel"/>
        /// - No part of the topic is a null level topic or an empty one
        /// - The topic does not contain a part that is longer than the <see cref="Topics.MaxSliceLength"/>
        /// </summary>
        ///
        /// <remarks>
        /// This method considers "/" as a valid topic
        /// </remarks>
        ///
        /// <param name="rawTopic">The raw MQTT topic to check</param>
        private static void EnsureRawTopicValidity(string rawTopic)
        {
            // Allows most basic (and deprecated) MQTT topic: "/"
            if (rawTopic == Topics.Separator.ToString())
            {
                return;
            }

            // Looking for the forbidden null character
            if (rawTopic.Contains(Topics.NullCharacter))
            {
                throw new EmptyTopicException("Illegal null character found");
            }

            // Starting checks
            var isMultiLevelWildcardEncountered = false;

            // Analyzing each slice of the provided topic
            foreach (var slice in rawTopic.Split(Topics.Separator))
            {
                // Ensuring that no addition is made after a multilevel wildcard
                if (isMultiLevelWildcardEncountered)
                {
                    throw new IllegalTopicConstructionException(ExceptionMessages.TopicAfterWildcard);
                }

                // Checking if the topic contains either two consecutive separators or only spaces
                if (string.IsNullOrWhiteSpace(slice))
                {
                    throw new EmptyTopicException();
                }

                // A topic slice can not be made of more than `Topics.MaxSliceLength` char
                if (slice.Length > Topics.MaxSliceLength)
                {
                    throw new TooLongTopicException($"Attempted to add '{slice}'");
                }

                // Keeping track of encountered wildcards
                if (slice == Wildcards.MultiLevel.ToString())
                {
                    isMultiLevelWildcardEncountered = true;
                }
            }
        }

        /// <summary>
        /// Remove all illegal characters in a topic
        /// </summary>
        /// <param name="toNormalize">Topic to format</param>
        /// <returns>The sanitized topic</returns>
        public static string Normalize(string toNormalize)
        {
            return toNormalize.Replace(" ", string.Empty)
                .Replace(Topics.Separator.ToString(), string.Empty)
                .Replace(Wildcards.MultiLevel.ToString(), string.Empty)
                .Replace(Wildcards.SingleLevel.ToString(), string.Empty);
        }

        /// <summary>
        /// Create a <see cref="Topic"/> from a raw string
        /// </summary>
        /// <param name="rawTopic">The raw MQTT topic</param>
        /// <returns></returns>
        public static Topic Parse(string rawTopic)
        {
            return new Topic(rawTopic);
        }

        /// <summary>
        /// Return the topic's path
        /// </summary>
        /// <returns>The topic's path</returns>
        public override string ToString()
        {
            return Path;
        }

        /// <summary>
        /// Attempt to create a <see cref="Topic"/> from a raw string
        /// </summary>
        /// <param name="rawTopic">The raw MQTT topic</param>
        /// <param name="result">A <see cref="Topic"/> instance to override with a newly created Topic from the rawString parameter</param>
        /// <returns>True on success; false otherwise</returns>
        public static bool TryParse(string rawTopic, out Topic result)
        {
            result = null;

            try
            {
                result = new Topic(rawTopic);
            }
            catch (BaseException)
            {
                // No action performed
            }

            return result != null;
        }
    }
}
