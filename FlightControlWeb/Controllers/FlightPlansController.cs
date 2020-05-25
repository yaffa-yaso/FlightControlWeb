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

        /*       public FlightPlansController(IFlightsManager manager)
               {
                   this.flightsManager = manager;
               }*/

        // GET: api/FlightPlans
        [HttpGet("{id}", Name = "Get")]
        public FlightPlan Get(string id)
        {
            FlightPlan fp = flightsManager.GetFlight(id);
            if (fp == null)
            {

            }

            return fp;
        }

        // POST: api/FlightPlans
        [HttpPost]
        public FlightPlan Post([FromBody] FlightPlan fp)
        {
            flightsManager.AddFlight(fp);
            return fp;
        }
    }
}
