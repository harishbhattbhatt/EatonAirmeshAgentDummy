using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Eaton.Ephesus.AirMeshAgent.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Eaton.Ephesus.AirMeshAgent.Core.Services
{
    /// <summary>
    /// SimplySnap API Interface
    /// </summary>
    public class AirMeshService
    {
        private const string API_SITES = "api/users/sites";
        private const string API_GATEWAYS = "api/sites/{0}/gateways";
        private const string API_CONNECTIONS = "api/connections/{0}";
        private const string API_POWER = "api/v1/power?count={0}&q={{\"controller_id\":\"{1}\"}}&reverse=true&sortby=timestamp";

        private const string BASE_GATEWAY = "https://{0}/";

        private const string CONTROLLER_VIEW = "api/v1/light_controller_view";

        private const string SITE_ID = "_id";
        private const string GATEWAY_ID = "_id";
        private const string GATEWAY_HOSTNAME = "hostname";
        private const string CONTROLLER_ID = "controller_id";

        private const string URL = "url";
        private const string OBJECT = "object";

        private readonly TextWriter log;

        public AirMeshService(TextWriter log)
        {
            this.log = log;

        }

        /// <summary>
        /// Get a list of Site IDs from SimplySnap API
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetSiteIDs()
        {
            var cookieContainer = new CookieContainer();

            var toReturn = new List<string>();

            using (var clientHandler = new HttpClientHandler() { CookieContainer = cookieContainer, UseCookies = true })
            using (var client = new HttpClient(clientHandler) { BaseAddress = new Uri(Settings.SnapLightingBaseAddress) })
            {
                // Log into Snap Lighting
                Authorize(client);

                var response = client.GetAsync(API_SITES, HttpCompletionOption.ResponseHeadersRead).Result;
                if (response.IsSuccessStatusCode)
                {
                    using (Stream s = response.Content.ReadAsStreamAsync().Result)
                    using (StreamReader sr = new StreamReader(s))
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        var jsonResults = JArray.Load(reader);
                        foreach (JObject siteJson in jsonResults)
                        {
                            toReturn.Add((string)siteJson[SITE_ID]);
                        }
                    }
                }
                else
                {
                    log.Write(string.Format("A Failure response occured while trying to retrieve gateways - {0}", Helpers.FormatMessage(response)));
                }
            }

            return toReturn;
        }

        /// <summary>
        /// For a give list of Site Id's look up gateway ID and hostname
        /// </summary>
        /// <param name="siteIDs">List of Site Ids to find Gateways for</param>
        /// <returns>Matched Gateways for passed SiteIDs</returns>
        public IEnumerable<Gateway> GetGateways(IEnumerable<string> siteIDs)
        {
            var cookieContainer = new CookieContainer();

            var toReturn = new List<Gateway>();
            var sites = new List<string>();

            using (var clientHandler = new HttpClientHandler() { CookieContainer = cookieContainer, UseCookies = true })
            using (var client = new HttpClient(clientHandler) { BaseAddress = new Uri(Settings.SnapLightingBaseAddress) })
            {
                // Log into Snap Lighting
                Authorize(client);

                foreach (var site in siteIDs)
                {
                    string url = string.Format(API_GATEWAYS, site);
                    var response = client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        using (Stream s = response.Content.ReadAsStreamAsync().Result)
                        using (StreamReader sr = new StreamReader(s))
                        using (JsonReader reader = new JsonTextReader(sr))
                        {
                            var jsonResults = JArray.Load(reader);
                            foreach (JObject gatewayJson in jsonResults)
                            {
                                toReturn.Add(new Gateway()
                                {
                                    Id = (string)gatewayJson[GATEWAY_ID],
                                    Hostname = (string)gatewayJson[GATEWAY_HOSTNAME]
                                });
                            }
                        }
                    }
                    else
                    {
                        log.Write(string.Format("A Failure response occured while trying to retrieve sites - {0}", Helpers.FormatMessage(response)));
                    }
                }
            }

            return toReturn;
        }

        /// <summary>
        /// Gets Controller data and Status for give gateway
        /// </summary>
        /// <param name="gateway">Gate to retrieve controller status for</param>
        /// <returns>List of Controllers and assocated statues</returns>
        public IEnumerable<SimplySnapControllerStatus> GetControllerStatusForGateway(Gateway gateway)
        {
            var toReturn = new List<SimplySnapControllerStatus>();
            var controllers = new List<SimplySnapController>();

            var cookieContainer = new CookieContainer();

            using (var clientHandler = new HttpClientHandler() { CookieContainer = cookieContainer, UseCookies = true })
            using (var client = new HttpClient(clientHandler) { BaseAddress = new Uri(Settings.SnapLightingBaseAddress) })
            {
                // Log into Snap Lighting
                Authorize(client);

                // Grab connection URL and token details
                string url = string.Format(API_CONNECTIONS, gateway.Id);
                var response = client.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode)
                {
                    log.Write(string.Format("A Failure response occured while trying to retrieve connection for Gateway {0} ({1}) - {2}", gateway.Hostname, gateway.Id, Helpers.FormatMessage(response)));
                    return toReturn;
                }

                Uri loginUrl = new Uri((string)(JObject.Parse(response.Content.ReadAsStringAsync().Result)[URL]));
                string gatewayBaseAddress = string.Format(BASE_GATEWAY, gateway.Hostname);
                string gatewayToken = loginUrl.Query;

                using (var gatewayClientHandler = new HttpClientHandler() { CookieContainer = cookieContainer, UseCookies = true })
                using (var gatewayClient = new HttpClient(gatewayClientHandler) { BaseAddress = new Uri(gatewayBaseAddress) })
                {
                    // Log in
                    response = gatewayClient.GetAsync(gatewayToken).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        log.Write(string.Format("A Failure response occured while trying to retrieve to connect to Gateway {0} ({1}) - {2}", gateway.Hostname, gateway.Id, Helpers.FormatMessage(response)));
                        return toReturn;
                    }

                    // Send a request to get the list of light controllers
                    url = string.Format(CONTROLLER_VIEW, gateway.Id);
                    response = gatewayClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        using (Stream s = response.Content.ReadAsStreamAsync().Result)
                        using (StreamReader sr = new StreamReader(s))
                        using (JsonReader reader = new JsonTextReader(sr))
                        {
                            var jsonResponse = JObject.Load(reader);
                            var controllersJson = (JArray)jsonResponse[OBJECT];
                            foreach (JObject controllerJson in controllersJson)
                            {
                                if (controllerJson[CONTROLLER_ID] == null)
                                    continue;

                                controllers.Add(controllerJson.ToObject<SimplySnapController>());
                            }
                        }
                    }
                    else
                    {
                        log.Write(string.Format("A Failure response occured while trying to retrieve to light controllers for Gateway {0} ({1}) - {2}", gateway.Hostname, gateway.Id, Helpers.FormatMessage(response)));
                        return toReturn;
                    }

                    foreach (var controller in controllers)
                    {
                        url = Uri.EscapeUriString(string.Format(API_POWER, Settings.MaxStatsToRequest, controller.controller_id));
                        response = gatewayClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            using (Stream s = response.Content.ReadAsStreamAsync().Result)
                            using (StreamReader sr = new StreamReader(s))
                            using (JsonReader reader = new JsonTextReader(sr))
                            {
                                var jsonResponse = JObject.Load(reader);
                                var statusesJson = (JArray)jsonResponse[OBJECT];
                                foreach (JObject statusJson in statusesJson)
                                {
                                    var power = statusJson.ToObject<SimplySnapStatus>();
                                    toReturn.Add(new SimplySnapControllerStatus(controller, power));
                                }
                            }
                        }
                        else
                        {
                            // TODO:  Put a failure message on the queue for this device...
                            log.Write(string.Format("A Failure response occured while trying to retrieve to power infor for Controller{0} from Gateway {1} ({2}) - {3}", controller.controller_id, gateway.Hostname, gateway.Id, Helpers.FormatMessage(response)));
                            return toReturn;
                        }
                    }
                }
            }

            return toReturn;
        }

        public string GetSchema(string classname)
        {
            var cookieContainer = new CookieContainer();

            string toReturn = null;

            using (var clientHandler = new HttpClientHandler() { CookieContainer = cookieContainer, UseCookies = true })
            using (var client = new HttpClient(clientHandler) { BaseAddress = new Uri(Settings.SnapLightingBaseAddress) })
            {
                // Log into Snap Lighting
                Authorize(client);

                string schema = string.Format("api/v1/schema/{0}", classname);

                var response = client.GetAsync(schema, HttpCompletionOption.ResponseHeadersRead).Result;
                if (response.IsSuccessStatusCode)
                {
                    using (Stream s = response.Content.ReadAsStreamAsync().Result)
                    using (StreamReader sr = new StreamReader(s))
                    {
                        toReturn = sr.ReadToEnd();
                    }
                }
                else
                {
                    log.Write(string.Format("A Failure response occured while trying to retrieve gateways - {0}", Helpers.FormatMessage(response)));
                }
            }

            return toReturn;
        }

        public static bool IsNullOrEmpty(JToken token)
        {
            return (token == null) ||
                   (token.Type == JTokenType.Array && !token.HasValues) ||
                   (token.Type == JTokenType.Object && !token.HasValues) ||
                   (token.Type == JTokenType.String && token.ToString() == String.Empty) ||
                   (token.Type == JTokenType.Null);
        }

        private void Authorize(HttpClient client)
        {
            // Log into Snap Lighting
            SnapLightingCredentials loginCredentials = new SnapLightingCredentials()
            {
                email = Settings.AirMeshLogin,
                password = Settings.AirMeshLPwd
            };
            HttpResponseMessage response = client.PostAsJsonAsync("auth/local", loginCredentials).Result;
            string token = (string)(JObject.Parse(response.Content.ReadAsStringAsync().Result)["token"]);

            // Set authorization token
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public class SnapLightingCredentials
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
