using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Eaton.Ephesus.AirMeshAgent.Core
{
    public static class Helpers
    {
        public static string FormatMessage(HttpResponseMessage responseMessage)
        {
            var toReturn = new StringBuilder();

            toReturn.Append("StatusCode: ");
            toReturn.Append(responseMessage.StatusCode);
            toReturn.Append(" ReasonPhrase: ");
            toReturn.Append(responseMessage.ReasonPhrase);

            return toReturn.ToString();
        }
        /// <summary>
        /// Format a Air mesh unique id to Eaton IoT format 8-4-4-4-12
        /// </summary>
        /// <param name="unformatedUUID"></param>
        /// <returns></returns>
        public static string FormatUUID(string unformatedUUID)
        {
            var toReturn = new StringBuilder();

            for (int i = 0; i < unformatedUUID.Length; i++)
            {
                toReturn.Append(unformatedUUID[i]);
                switch(i)
                {
                    case 8:
                    case 12:
                    case 16:
                    case 20:
                        toReturn.Append("-");
                        break;
                }
            }

            return toReturn.ToString();
        }
    }
}
