using Newtonsoft.Json;

namespace Eaton.Ephesus.Ingress.Contracts
{
    /// <summary>
    /// Realtime data structure
    /// </summary>
    public class RealTime
    {
        /// <summary>
        /// Channel Tag - unique integer id registered on platform
        /// </summary>
        [JsonProperty(PropertyName = "c")]
        public int Tag { get; set; }

        /// <summary>
        /// Channel value
        /// </summary>
        [JsonProperty(PropertyName = "v")]
        public string Value { get; set; }
        
        /// <summary>
        /// Time in epoch format
        /// </summary>
        [JsonProperty(PropertyName = "t")]
        public int Time { get; set; }

        /// <summary>
        /// Milli seconds
        /// </summary>
        [JsonProperty(PropertyName = "t_ms")]
        public int TimeInMiliseconds { get; set; }
    }
}
