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

        // GET: api/FlightPlans/5
        [HttpGet("{id}", Name = "Get")]
        public FlightPlan Get(string id)
        {
            FlightPlan fp = flightsManager.GetFlight(id);
            return fp;
        }

        // POST: api/FlightPlans
        [HttpPost]
        public string Post([FromBody] FlightPlan fp)
        {
            return flightsManager.AddFlight(fp);
        }
    }
}
