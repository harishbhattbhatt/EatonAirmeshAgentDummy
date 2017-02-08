using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eaton.Ephesus.AirMeshAgent.Core;
using Microsoft.Azure.WebJobs;

namespace Eaton.Ephesus.AirMeshAgent.Realtime
{
    class Program
    {
        static void Main()
        {
            var config = new JobHostConfiguration();
            config.Queues.BatchSize = 5;
            config.NameResolver = new QueueNameResolver();

            if (config.IsDevelopment)
            {
                // Just load pull one item off the queue at a time in development mode...
                config.Queues.BatchSize = 1;
                config.UseDevelopmentSettings();
            }

            var host = new JobHost(config);
            host.RunAndBlock();
        }
    }
}
