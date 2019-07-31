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
    using System.Linq;
    
    /// <summary>
    /// TODO: doc
    /// </summary>
    public class Topic
    {
        /// <summary>
        /// TODO: doc
        /// </summary>
        public int Level 
            => Path.Count(_ => _.Equals(Topics.Separator));

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
        /// <returns></returns>
        public override string ToString()
        {
            return Path;
        }
    }
}
