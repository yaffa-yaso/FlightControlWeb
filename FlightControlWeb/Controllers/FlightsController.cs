using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private IFlightsManager flightsManager;

        public FlightsController(IFlightsManager manager)
        {
            this.flightsManager = manager;
        }

        // GET: api/Flights
        [HttpGet("{relative_to}")]
        public IEnumerable<Flight> GetMyFlight(DateTime relative_to)
        {
            List<Flight> flights = new List<Flight>();
            IEnumerable<FlightPlan> flightPlan = flightsManager.GetAllFlights();
            foreach (FlightPlan item in flightPlan)
            {
                double airTime =  item.segments.First<Segment>().timespan_seconds;
                DateTime startTime = DateTime.ParseExact(item.initial_location.date_time, "yyyy-MM-ddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture);
                DateTime finishTime = startTime.AddSeconds(airTime);

                if (startTime < relative_to && finishTime > relative_to) {
                    Flight flight = new Flight (flightsManager.GetId(item), item.initial_location.longitude, item.initial_location.latitude,
                        item.passengers, item.company_name, item.initial_location.date_time, false);

                flights.Add(flight);
                }
            }
            return flights;
        }

        // GET: api/Flights
        [HttpGet]
        public IEnumerable<FlightPlan> GetAllFlight()
        {
            return flightsManager.GetAllFlights();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            flightsManager.DeleteFlight(id);
        }
    }
}
