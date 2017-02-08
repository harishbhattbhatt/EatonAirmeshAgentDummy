using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaton.Ephesus.AirMeshAgent.Core
{
    public static class Settings
    {
        public static string Environment { get { return ConfigurationManager.AppSettings["Environment"]; } }

        public static string EatonRMQ { get { return ConfigurationManager.ConnectionStrings["EatonRMQ"].ConnectionString; } }

        public static string AzureWebJobsStorage { get { return ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ConnectionString; } }

        public static string AzureWebJobsDashboard { get { return ConfigurationManager.ConnectionStrings["AzureWebJobsDashboard"].ConnectionString; } }

        public static string AirMeshLogin { get { return ConfigurationManager.AppSettings["AirMeshLogin"]; } }
        public static string AirMeshLPwd { get { return ConfigurationManager.AppSettings["AirMeshLPwd"]; } }

        public static string MaxStatsToRequest { get { return ConfigurationManager.AppSettings["MaxStatsToRequest"]; } }
        public static string SnapLightingBaseAddress { get { return ConfigurationManager.AppSettings["SnapLightingBaseAddress"]; } }

        public static string AirMeshDeviceProfileUUID { get { return ConfigurationManager.AppSettings["AirMeshDeviceProfileUUID"]; } }
    }
}
