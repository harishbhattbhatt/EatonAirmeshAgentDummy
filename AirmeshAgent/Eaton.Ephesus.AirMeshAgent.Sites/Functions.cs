using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eaton.Ephesus.AirMeshAgent.Core;
using Eaton.Ephesus.AirMeshAgent.Core.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace Eaton.Ephesus.AirMeshAgent.Sites
{
    public class Functions
    {
        /// <summary>
        /// Initiates a call to pull available gateways from Air Mesh.  All found gateways will be placed on an Azure Queue to be picked up by 
        /// the Controllers Web Job
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="log"></param>
        public static void PollGateways([TimerTrigger("00:15:00", RunOnStartup = true, UseMonitor = true)]TimerInfo timer, TextWriter log)
        {
            try
            {
                // Fire up Air Mesh API...
                var airMesh = new AirMeshService(log);

                // Get the available sites
                var siteIDs = airMesh.GetSiteIDs();

                // Get the Gateways for each site
                var gateways = airMesh.GetGateways(siteIDs);

                // Get the Azure Queue...
                var queue = QueueService.GetQueue(Constants.REAL_TIME_DEVICE_QUEUE);

                // Push the Gateways onto the queue
                foreach (var gateway in gateways)
                {
                    queue.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(gateway)));
                }
            }
            catch (Exception ex)
            {
                // TODO: Format logging
                log.WriteAsync(ex.ToString());
            }
        }
    }
}
