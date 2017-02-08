using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eaton.Ephesus.AirMeshAgent.Core;
using Microsoft.Azure.WebJobs;

namespace Eaton.Ephesus.AirMeshAgent.Sites
{
    class Program
    {
        static void Main()
        {
            var config = new JobHostConfiguration();
            config.UseTimers();
            config.NameResolver = new QueueNameResolver();

            if(config.IsDevelopment)
            {
                config.UseDevelopmentSettings();
            }

            var host = new JobHost(config);
            host.RunAndBlock();
        }
    }
}
