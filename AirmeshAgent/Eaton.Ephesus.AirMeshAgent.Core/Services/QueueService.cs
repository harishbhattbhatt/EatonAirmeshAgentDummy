using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Eaton.Ephesus.AirMeshAgent.Core.Services
{
    /// <summary>
    /// Wrapper for getting/creating Azure Queues
    /// </summary>
    public static class QueueService
    {
        public static CloudQueue GetQueue(string queueName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Settings.AzureWebJobsStorage);
            System.Net.ServicePoint servicePoint = System.Net.ServicePointManager.FindServicePoint(storageAccount.QueueEndpoint);
            servicePoint.UseNagleAlgorithm = false;

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference(QueueNameResolver.GetQueueEnvironmentName(queueName));
            queue.CreateIfNotExists();
            
            return queue;
        }
    }
}
