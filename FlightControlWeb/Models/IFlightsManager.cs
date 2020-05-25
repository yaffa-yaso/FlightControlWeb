using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
   public interface IFlightsManager
    {
        IEnumerable<FlightPlan> GetAllFlights();
        //FlightPlan GetFlightById(int id);
        void AddFlight(FlightPlan f);
        //void UpdateFight(FlightPlan f);
        void DeleteFlight(string id);
        FlightPlan GetFlight(string id);
        string GetId(FlightPlan f);
    }
}
