using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FlightControlWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightPlanController : ControllerBase
    {
        private IFlightPlanManager flightsManager;
        private IServerManager serverManager;

        public FlightPlanController(IFlightPlanManager manager, IServerManager SManager)
        {
            this.flightsManager = manager;
            this.serverManager = SManager;
        }

        // GET: api/FlightPlan/5 - returns exists FlightPlan by id
        [HttpGet("{id}", Name = "Get")]
        public async Task<FlightPlan> Get(string id)
        {
            //search in internal fligthPlans
            FlightPlan fp = flightsManager.GetFlight(id);

            //search in external fligthPlans
            foreach (Server server in serverManager.GetAllServers())
            {
                //if fp has been found
                if (fp != null) { break; }

                string URL = server.ServerURL;
                if (server.ServerURL[server.ServerURL.Length - 1] == '/')
                {
                    URL = server.ServerURL.Substring(0, server.ServerURL.Length - 1);
                }
                //building Get request to synchronized servers
                string request = URL + ":" + server.ServerId + "/api/FlightPlan/" + id;
                fp = await Task.Run(() => DowonloadWebsite(request));
            }

            return fp;
        }

        //sending Get request to synchronized servers
        private FlightPlan DowonloadWebsite(string request)
        {
            WebClient client = new WebClient();
            //get web page as string
            var output = client.DownloadString(request);
            if (output == null)
            {
                return null;
            }
            else
            {
                //convert from string to Json Object
                return JsonConvert.DeserializeObject<FlightPlan>(output);
            }
        }

        // POST: api/FlightPlan
        [HttpPost]
        public string Post([FromBody] FlightPlan fp)
        {
            try
            {
                return flightsManager.AddFlight(fp);
            }
            catch(ArgumentException e)
            {
                Console.Out.WriteLine(e);
                return null;
            }
        }
    }
}
