using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FlightControlWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private IFlightsManager flightsManager;
        private IServerManager serverManager;

        public FlightsController(IFlightsManager FManager, IServerManager SManager)
        {
            this.flightsManager = FManager;
            this.serverManager = SManager;
        }

        // GET: api/Flights
        [HttpGet]
        public async Task<IEnumerable<Flight>> GetAllFlight([FromQuery(Name = "relative_to")]string relative_to, [FromQuery(Name = "sync_all")]string sync_all)
        {
            DateTime data = DateTime.ParseExact(relative_to, "yyyy-MM-ddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture);
            List<Flight> flights = new List<Flight>();
            IEnumerable<FlightPlan> flightPlan = flightsManager.GetAllFlights();

            foreach (FlightPlan item in flightPlan)
            {
                DateTime startTime = DateTime.ParseExact(item.initial_location.date_time, "yyyy-MM-ddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture);
                DateTime timespan = startTime;

                foreach (Segment segment in item.segments.ToList())
                {
                    timespan = timespan.AddSeconds(segment.timespan_seconds);
                    if (startTime <= data && timespan >= data)
                    {
                        Flight flight = new Flight();
                        flight.passengers = item.passengers;
                        flight.company_name = item.company_name;
                        flight.flight_id = flightsManager.GetId(item);
                        flight.longitude = segment.longitude;
                        flight.latitude = segment.latitude;
                        flight.date_time = item.initial_location.date_time;
                        flight.is_external = false;

                        flights.Add(flight);
                        break;
                    }
                }
            }

            if (Request.QueryString.ToString().Contains("sync_all"))
            {
                IEnumerable<Server> servers = serverManager.GetAllServers();
                if (servers == null)
                {
                    return flights;
                }

                HttpClient client = new HttpClient();
                foreach (Server item in servers)
                {
                    string result = await client.GetStringAsync(item.ServerURL + "/api/Flights?relative_to=" + relative_to);
                    dynamic flight = JsonConvert.DeserializeObject(result);

                    flights.Add(flight);
                }
            } 
            return flights;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            flightsManager.DeleteFlight(id);
        }
    }
}
