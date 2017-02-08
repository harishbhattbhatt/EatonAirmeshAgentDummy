using Newtonsoft.Json;

namespace Eaton.Ephesus.Ingress.Contracts
{
    /// <summary>
    /// Trend data structure
    /// </summary>
    public class Trend
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
        /// Min. Channel value in interval (i.e. 5/15 min)
        /// </summary>
        [JsonProperty(PropertyName = "min")]
        public string Min { get; set; }

        /// <summary>
        /// Max. channel value in interval (i.e. 5/15 min)
        /// </summary>
        [JsonProperty(PropertyName = "max")]
        public string Max { get; set; }

        /// <summary>
        /// Average channel value in interval
        /// </summary>
        [JsonProperty(PropertyName = "avg")]
        public string Average { get; set; }

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
