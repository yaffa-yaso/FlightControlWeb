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
        private IFlightPlanManager flightsManager;
        private IServerManager serverManager;

        public FlightsController(IFlightPlanManager FManager, IServerManager SManager)
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

                List<Segment> flightSegments = item.segments.ToList();
                for (var i = 0; i < flightSegments.Count; i++)
                {
                    timespan = timespan.AddSeconds(flightSegments[i].timespan_seconds);
                    if (startTime <= data && timespan >= data)
                    {
                        double x1, y1, x;
                        if (i == 0)
                        {
                            y1 = item.initial_location.longitude - flightSegments[i].longitude;
                            x1 = item.initial_location.latitude - flightSegments[i].latitude;
                            x = item.initial_location.latitude + (x1 / flightSegments[i].timespan_seconds);
                        }
                        else
                        {
                            y1 = flightSegments[i - 1].longitude - flightSegments[i].longitude;
                            x1 = flightSegments[i - 1].latitude - flightSegments[i].latitude;
                            x = flightSegments[i-1].latitude + (x1 / flightSegments[i].timespan_seconds);
                        }
                        double m = y1 / x1;
                        double y = m*(x - x1) + y1;

                        Flight flight = new Flight
                        {
                            passengers = item.passengers,
                            company_name = item.company_name,
                            flight_id = flightsManager.GetId(item),
                            longitude = x,
                            latitude = y,
                            date_time = item.initial_location.date_time,
                            is_external = false
                        };

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
                    flight.is_external = true;

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
