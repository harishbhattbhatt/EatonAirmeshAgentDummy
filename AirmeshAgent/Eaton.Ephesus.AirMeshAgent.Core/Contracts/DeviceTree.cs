using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Eaton.Ephesus.AirMeshAgent.Core.Contracts
{
    public class DeviceTree
    {
        [JsonProperty(PropertyName = "d")]
        public Device Device { get; set; }
    }

    public class Device
    {
        [JsonProperty(PropertyName = "d")]
        public string DeviceId { get; set; }

        [JsonProperty(PropertyName = "profile")]
        public string Profile { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "serial")]
        public string Serial { get; set; }

        [JsonProperty(PropertyName = "asset")]
        public string Asset { get; set; }

        [JsonProperty(PropertyName = "mac")]
        public string MAC { get; set; }

        [JsonProperty(PropertyName = "ds")]
        public List<Device> SubDevices { get; set; }
    }
}
