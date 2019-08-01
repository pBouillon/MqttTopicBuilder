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

namespace MqttTopicBuilder
{
    using MqttUtils;
    
    /// <summary>
    /// TODO: doc
    /// </summary>
    public class Topic
    {
        /// <summary>
        /// TODO: doc
        /// </summary>
        public bool IsPathEmpty
            => Path.Length == 0;

        /// <summary>
        /// TODO: doc
        /// </summary>
        public int Level
            => IsPathEmpty
                ? 0
                : Path.Split(Topics.Separator).Length;

        /// <summary>
        /// TODO: doc
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// TODO: doc
        /// </summary>
        public Topic(string path)
        {
            Path = path;
        }

        /// <summary>
        /// TODO: doc
        /// </summary>
        /// <param name="toNormalize"></param>
        /// <returns></returns>
        public static string Normalize(string toNormalize)
        {
            return toNormalize.Replace(" ", string.Empty)
                .Replace(Topics.Separator.ToString(), string.Empty)
                .Replace(Wildcards.MultiLevel.ToString(), string.Empty)
                .Replace(Wildcards.SingleLevel.ToString(), string.Empty);
        }

        /// <summary>
        /// TODO: doc
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Path;
        }
    }
}
