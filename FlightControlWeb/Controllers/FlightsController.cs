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

       // GET: api/Flights?relative_to=<DATE_TIME>
        [HttpGet]
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
                    Flight flight = new Flight
                    {
                        flight_id = "1",
                        longitude = item.initial_location.longitude,
                        latitude = item.initial_location.latitude,
                        passengers = item.passengers,
                        company_name = item.company_name,
                        date_time = item.initial_location.date_time,
                        is_external = false
                    };

                flights.Add(flight);
                }
            }
            return flights;
        }

        // GET: api/Flights?relative_to=<DATE_TIME>&sync_all
        [HttpGet]
        public IEnumerable<FlightPlan> GetAllFlight()
        {
            return flightsManager.GetAllFlights();
        }

        // DELETE: api/Flights/{id}
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            flightsManager.DeleteFlight(id);
        }
    }
}
