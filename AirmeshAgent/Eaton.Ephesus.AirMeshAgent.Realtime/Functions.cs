using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eaton.Ephesus.AirMeshAgent.Core;
using Eaton.Ephesus.AirMeshAgent.Core.Models;
using Eaton.Ephesus.AirMeshAgent.Core.Services;
using Eaton.Ephesus.Ingress.Contracts;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Eaton.Ephesus.AirMeshAgent.Realtime
{
    public class Functions
    {
        /// <summary>
        /// Listens on the Azure queue realtimedevicequeue[ENVRIONMENT]
        /// </summary>
        /// <param name="gateway">The Gateway to pull controller statuses from</param>
        /// <param name="log">Azure L</param>
        public static void ProcessRealtimeQueueMessage([QueueTrigger(Constants.REAL_TIME_DEVICE_QUEUE_TRIGGER)] Gateway gateway, TextWriter log)
        {
            try
            {
                var ingressService = new IngressService(log);
                var airMeshService = new AirMeshService(log);

                foreach (var controllerStatus in airMeshService.GetControllerStatusForGateway(gateway))
                {
                    // Check if we have registered this device, if not let's do it...
                    if(!RegisteredDeviceService.IsDeviceRegistered(controllerStatus.UUID))
                    {
                        // TODO: Confirm this pattern ->  Should we poll devices at startup, and get them registered before
                        RegisteredDeviceService.RegisterDevice(controllerStatus.UUID);
                        ingressService.PushDataToPlatform(D2CAPI.DeviceTree, controllerStatus.Controller.controller_id, controllerStatus.GetDeviceTree());
                    }

                    // Push Realtimes...
                    ingressService.PushDataToPlatform(D2CAPI.Realtimes, controllerStatus.Controller.controller_id, controllerStatus.GetRealTimes());

                    // Push Trends...
                    ingressService.PushDataToPlatform(D2CAPI.Trends, controllerStatus.Controller.controller_id, controllerStatus.GetTrends());
                }
           }
            catch(Exception ex)
            {
                // TODO: Improve logging text
                log.WriteAsync(ex.ToString());
            }
        }
    }
}
