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
    public class FlightPlanController : ControllerBase
    {
        private IFlightPlanManager flightsManager;

        public FlightPlanController(IFlightPlanManager manager)
        {
            this.flightsManager = manager;
        }

        // GET: api/FlightPlan/5
        [HttpGet("{id}", Name = "Get")]
        public FlightPlan Get(string id)
        {
            FlightPlan fp = flightsManager.GetFlight(id);
            return fp;
        }

        // POST: api/FlightPlan
        [HttpPost]
        public string Post([FromBody] FlightPlan fp)
        {
            if (fp.company_name == null) {
                throw new ArgumentException("company name must be provided");
            }
            if (fp.passengers < 0) {
                throw new ArgumentOutOfRangeException("number of passengers cannot be less then 0");
            }
            if (fp.segments == null) {
                throw new ArgumentException("there must be at list one segments");
            }

            return flightsManager.AddFlight(fp);
        }
    }
}
