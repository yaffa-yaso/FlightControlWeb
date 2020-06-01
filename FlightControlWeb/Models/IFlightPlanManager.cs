﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
   public interface IFlightPlanManager
    {
        IEnumerable<FlightPlan> GetAllFlightPlans();
        //FlightPlan GetFlightById(int id);
        string AddFlight(FlightPlan f);
        //void UpdateFight(FlightPlan f);
        void DeleteFlight(string id);
        FlightPlan GetFlight(string id);
        string GetId(FlightPlan f);
    }
}
