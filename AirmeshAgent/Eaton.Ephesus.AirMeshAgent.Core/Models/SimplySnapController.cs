using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaton.Ephesus.AirMeshAgent.Core.Models
{
    public class SimplySnapController
    {
        public double? jitter { get; set; }
        public string zones { get; set; }
        public string antenna_compensation { get; set; }
        public string location_id { get; set; }
        public string id { get; set; }
        public int? connected_devices { get; set; }
        public string snapaddr { get; set; }
        public double? power_on_level { get; set; }
        public string alarms { get; set; }
        public double? survey_level { get; set; }
        public string type { get; set; }
        public int? slot { get; set; }
        public string description { get; set; }
        public string connected_device_names { get; set; }
        public string name { get; set; }
        public double? level { get; set; }
        public string controller_info { get; set; }
        public string controller_id { get; set; }
        public double? y { get; set; }
        public double? x { get; set; }
        public string alarm_ts { get; set; }
        public string street_address { get; set; }
    }
}
