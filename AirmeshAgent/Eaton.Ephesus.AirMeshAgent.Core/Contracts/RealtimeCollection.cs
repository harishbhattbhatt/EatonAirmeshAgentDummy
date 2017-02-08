using Newtonsoft.Json;
using System.Collections.Generic;

namespace Eaton.Ephesus.Ingress.Contracts
{
    public class RealtimeCollection
    {
        [JsonProperty(PropertyName = "realtimes")]
        public List<RealTime> RealTimeChannels { get; set; }

        public RealtimeCollection()
        {
            RealTimeChannels = new List<RealTime>();
        }
    }
}
