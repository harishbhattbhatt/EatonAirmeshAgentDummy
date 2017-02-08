using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eaton.IoT.DataIO.Contracts;
using Eaton.IoT.DataIO.Core;
using Newtonsoft.Json;

namespace Eaton.Ephesus.AirMeshAgent.Core.Services
{
    /// <summary>
    /// Ingress Service Interface
    /// </summary>
    public class IngressService
    {
        private readonly TextWriter log;

        private const string DEVICETREE_API = "DeviceTree";
        private const string REALTIMES_API = "Realtimes";
        private const string TRENDS_API = "Trends";

        public const string IOTHUB_NAME = "AirMeshAgent";

        public IngressService(TextWriter log)
        {
            this.log = log;
        }

        public IDataIOConnection GetConnection()
        {
            var factory = new DataIOFactory();
            
            factory.SetConnectionString(Settings.EatonRMQ);
            var connection = factory.CreateConnection();
            
            if (connection != null && connection.IsOpen())
            {
                return connection;
            }
            
            return null;
        }

        public void PushDataToPlatform<T>(D2CAPI api, string deviceId, T message)
        {
            var toSend = JsonConvert.SerializeObject(message);

            using(var connection = GetConnection())
            using (var ingress = connection.CreateDataIngress())
            {
                ingress.Push(IOTHUB_NAME, GetAPIName(api), deviceId, Encoding.UTF8.GetBytes(toSend));
            }
        }

        private string GetAPIName(D2CAPI api)
        {
            switch(api)
            {
                case D2CAPI.DeviceTree:
                    return DEVICETREE_API;
                case D2CAPI.Realtimes:
                    return REALTIMES_API;
                case D2CAPI.Trends:
                    return TRENDS_API;
                default:
                    throw new Exception(string.Format("Undefined D@CAPI {0}", api));
            }
        }
    }

    public enum D2CAPI
    {
        DeviceTree,
        Realtimes,
        Trends
    }
}
