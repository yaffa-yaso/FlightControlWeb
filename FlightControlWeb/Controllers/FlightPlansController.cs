using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightPlansController : ControllerBase
    {
        private IFlightsManager flightsManager = new FlightsManager();

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
                    Flight flight = new Flight {
                        id = 1,
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

        // GET: api/FlightPlan/{id}
        [HttpGet("{id}", Name = "Get")]
        public FlightPlan Get(string id)
        {
            FlightPlan fp = flightsManager.GetFlight(id);
            if (fp == null)
            {

            }

            return fp;
        }

        // POST: api/FlightPlan
        [HttpPost]
        public FlightPlan Post([FromBody] FlightPlan fp)
        {
            flightsManager.AddFlight(fp);
            return fp;
        }

        // DELETE: api/Flights/{id}
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            flightsManager.DeleteFlight(id);
        }

        ////// תוספות של אהרון:
        // POST: api/FlightPlan
        [HttpPost]
        public FlightPlan GET([FromBody] FlightPlan fp)
        {
            flightsManager.AddFlight(fp);
            return fp;
        }
    }
}
