using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace Eaton.Ephesus.AirMeshAgent.Core
{
    /// <summary>
    /// Resolves Azure Queuenames - Appends the App.Config Environment setting to base queue name to prevent crossing posting to queues
    /// </summary>
    public class QueueNameResolver : INameResolver
    {
        public string Resolve(string name)
        {
            return GetQueueEnvironmentName(name);
        }

        public static string GetQueueEnvironmentName(string name)
        {
            var toReturn = new StringBuilder();
            
            toReturn.Append(name.ToLower());
            toReturn.Append(Settings.Environment.ToLower());

            return toReturn.ToString();
        }
    }
}
