using Newtonsoft.Json;
using System.Collections.Generic;

namespace Eaton.Ephesus.Ingress.Contracts
{
    public class TrendCollection
    {
        [JsonProperty(PropertyName = "trends")]
        public List<Trend> RealTimeChannels { get; set; }

        public TrendCollection()
        {
            RealTimeChannels = new List<Trend>();
        }
    }
}
