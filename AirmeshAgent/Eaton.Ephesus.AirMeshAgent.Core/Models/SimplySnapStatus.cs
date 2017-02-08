using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaton.Ephesus.AirMeshAgent.Core.Models
{
    public class SimplySnapStatus
    {
        public double? external_temperature { get; set; }
        public double? active_power { get; set; }
        public double? voltage { get; set; }
        public double? barometric_pressure { get; set; }
        public string id { get; set; }
        public double? peak_ic_temperature { get; set; }
        public double? mcu_supply_voltage { get; set; }
        public int? uptime { get; set; }
        public double? apparent_power { get; set; }
        public double? current { get; set; }
        public double? reference_voltage { get; set; }
        public bool? removable { get; set; }
        public double? mcu_temperature { get; set; }
        public double? sensor_b_input_voltage { get; set; }
        public int? interval_15 { get; set; }
        public double? max_level { get; set; }
        public DateTime? timestamp { get; set; }
        public int error { get; set; }
        public double? line_frequency { get; set; }
        public double? lifetime_power_load { get; set; }
        public double? sensor_a_input_voltage { get; set; }
        public double? peak_current { get; set; }
        public double? reactive_power { get; set; }
        public double? power_factor { get; set; }
        public string controller_id { get; set; }
        public double? humidity { get; set; }
        public double? ic_temperature { get; set; }
    }
}
