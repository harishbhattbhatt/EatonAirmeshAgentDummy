using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eaton.Ephesus.AirMeshAgent.Core.Contracts;
using Eaton.Ephesus.Ingress.Contracts;

namespace Eaton.Ephesus.AirMeshAgent.Core.Models
{
    public class SimplySnapControllerStatus
    {
        public string UUID { get; private set; }
        public SimplySnapController Controller { get; private set; }
        public SimplySnapStatus Status { get; private set; }

        public SimplySnapControllerStatus(SimplySnapController controller, SimplySnapStatus status)
        {
            Controller = controller;
            Status = status;
            UUID = Helpers.FormatUUID(controller.controller_id);
        }

        public DeviceTree GetDeviceTree()
        {
            var device = new Device()
            {
                DeviceId = UUID,
                Profile = Settings.AirMeshDeviceProfileUUID,
                Name = Controller.name
            };

            var toReturn = new DeviceTree()
            {
                Device = device
            };

            return toReturn;
        }

        public RealtimeCollection GetRealTimes()
        {
            var toReturn = new RealtimeCollection();

            // TODO: Lopp this up?
            var reatime = new RealTime()
            {
                Tag = 925,
                Value = Status.active_power.ToString(),
                Time = 1456198800 // TODO: Convert Timestamp to this number format
            };
            toReturn.RealTimeChannels.Add(reatime);

            reatime = new RealTime()
            {
                Tag = 926,
                Value = Status.max_level.ToString(),
                Time = 1456198800 // TODO: Convert Timestamp to this number format
            };
            toReturn.RealTimeChannels.Add(reatime);

            reatime = new RealTime()
            {
                Tag = 162,
                Value = Status.voltage.ToString(),
                Time = 1456198800 // TODO: Convert Timestamp to this number format
            };
            toReturn.RealTimeChannels.Add(reatime);

            reatime = new RealTime()
            {
                Tag = 1000,
                Value = Status.current.ToString(),
                Time = 1456198800 // TODO: Convert Timestamp to this number format
            };
            toReturn.RealTimeChannels.Add(reatime);

            reatime = new RealTime()
            {
                Tag = 1006,
                Value = Status.peak_current.ToString(),
                Time = 1456198800 // TODO: Convert Timestamp to this number format
            };
            toReturn.RealTimeChannels.Add(reatime);

            reatime = new RealTime()
            {
                Tag = 720,
                Value = Status.ic_temperature.ToString(),
                Time = 1456198800 // TODO: Convert Timestamp to this number format
            };
            toReturn.RealTimeChannels.Add(reatime);

            reatime = new RealTime()
            {
                Tag = 502,
                Value = Status.peak_ic_temperature.ToString(),
                Time = 1456198800 // TODO: Convert Timestamp to this number format
            };
            toReturn.RealTimeChannels.Add(reatime);

            reatime = new RealTime()
            {
                Tag = 392,
                Value = Status.humidity.ToString(),
                Time = 1456198800 // TODO: Convert Timestamp to this number format
            };
            toReturn.RealTimeChannels.Add(reatime);

            reatime = new RealTime()
            {
                Tag = 717,
                Value = Status.external_temperature.ToString(),
                Time = 1456198800 // TODO: Convert Timestamp to this number format
            };
            toReturn.RealTimeChannels.Add(reatime);

            reatime = new RealTime()
            {
                Tag = 959,
                Value = Status.uptime.ToString(),
                Time = 1456198800 // TODO: Convert Timestamp to this number format
            };
            toReturn.RealTimeChannels.Add(reatime);

            return toReturn;
        }

        public TrendCollection GetTrends()
        {
            var toReturn = new TrendCollection();

            // TODO: Lopp this up?
            var trend = new Trend()
            {
                Tag = 925,
                Value = Status.active_power.ToString(),
                Time = 1456198800 // TODO: Convert Timestamp to this number format
            };
            toReturn.RealTimeChannels.Add(trend);

            trend = new Trend()
            {
                Tag = 926,
                Value = Status.max_level.ToString(),
                Time = 1456198800 // TODO: Convert Timestamp to this number format
            };
            toReturn.RealTimeChannels.Add(trend);

            trend = new Trend()
            {
                Tag = 162,
                Value = Status.voltage.ToString(),
                Time = 1456198800 // TODO: Convert Timestamp to this number format
            };
            toReturn.RealTimeChannels.Add(trend);

            trend = new Trend()
            {
                Tag = 1000,
                Value = Status.current.ToString(),
                Time = 1456198800 // TODO: Convert Timestamp to this number format
            };
            toReturn.RealTimeChannels.Add(trend);

            trend = new Trend()
            {
                Tag = 1006,
                Value = Status.peak_current.ToString(),
                Time = 1456198800 // TODO: Convert Timestamp to this number format
            };
            toReturn.RealTimeChannels.Add(trend);

            trend = new Trend()
            {
                Tag = 720,
                Value = Status.ic_temperature.ToString(),
                Time = 1456198800 // TODO: Convert Timestamp to this number format
            };
            toReturn.RealTimeChannels.Add(trend);

            trend = new Trend()
            {
                Tag = 502,
                Value = Status.peak_ic_temperature.ToString(),
                Time = 1456198800 // TODO: Convert Timestamp to this number format
            };
            toReturn.RealTimeChannels.Add(trend);

            trend = new Trend()
            {
                Tag = 392,
                Value = Status.humidity.ToString(),
                Time = 1456198800 // TODO: Convert Timestamp to this number format
            };
            toReturn.RealTimeChannels.Add(trend);

            trend = new Trend()
            {
                Tag = 717,
                Value = Status.external_temperature.ToString(),
                Time = 1456198800 // TODO: Convert Timestamp to this number format
            };
            toReturn.RealTimeChannels.Add(trend);

            trend = new Trend()
            {
                Tag = 959,
                Value = Status.uptime.ToString(),
                Time = 1456198800 // TODO: Convert Timestamp to this number format
            };
            toReturn.RealTimeChannels.Add(trend);

            return toReturn;
        }
    }
}
