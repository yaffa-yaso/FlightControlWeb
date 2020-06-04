using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
   public interface IFlightPlanManager
    {
        IEnumerable<FlightPlan> GetAllFlightPlans();

        string AddFlight(FlightPlan f);

        void DeleteFlight(string id);

        FlightPlan GetFlight(string id);

        string GetId(FlightPlan f);
    }
}
