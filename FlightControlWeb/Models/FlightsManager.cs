using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class FlightsManager: IFlightsManager
    {

        ConcurrentDictionary<string, FlightPlan> flights = new ConcurrentDictionary<string, FlightPlan>();

       

        //public FlightPlan GetFlightById(int id)
        //{
        //    FlightPlan flight;
        //    if(flights.TryGetValue(id, out flight))
        //    {
        //        // doesn't exist
        //        throw new Exception("flight not found");
               
        //    }
        //    return flight;
        //}

        public void AddFlight(FlightPlan f)
        {
           bool item =  flights.TryAdd(GenerateNewRandom(), f);
        }

        //public void UpdateFight(Flight f)
        //{
        //    Flight flight;
        //    if (flights.TryGetValue(GenerateNewRandom(), out flight))
        //    {
        //        // doesn't exist
        //        throw new Exception("flight not found");
        //    }
        //    flight.id = f.id;
        //    flight.is_external = f.is_external;
        //    flight.latitude = f.latitude;
        //    flight.longitude = f.longitude;
        //    flight.passengers = f.passengers;

        //}

        public void DeleteFlight(string id)
        {
            FlightPlan removedItem;
            bool result = flights.TryRemove(id, out removedItem);
        }
        public IEnumerable<FlightPlan> GetAllFlights()
        {
            return flights.Values.ToList();
        }
        private static string GenerateNewRandom()
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateNewRandom();
            }
            return r;
        }
    }
}
