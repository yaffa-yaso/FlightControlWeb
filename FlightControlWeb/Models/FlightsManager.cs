using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class FlightsManager: IFlightsManager
    {

       private static ConcurrentDictionary<string, FlightPlan> flightPlans = new ConcurrentDictionary<string, FlightPlan>();
        private static ConcurrentDictionary<string, FlightPlan> flights = new ConcurrentDictionary<string, FlightPlan>();




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
            FlightPlan x = f;
            string key = GenerateNewRandom();
            bool item = flightPlans.TryAdd(key, f);

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

       public FlightPlan GetFlight(string id)
        {
            FlightPlan flight;
            if (flightPlans.TryGetValue(id, out flight))
            {
                // doesn't exist
                throw new Exception("flight not found");
            }
            else
            {
                return flight;
            }

        }


        public void DeleteFlight(string id)
        {
            FlightPlan removedItem;
            bool result = flightPlans.TryRemove(id, out removedItem);
        }
        public IEnumerable<FlightPlan> GetAllFlights()
        {
            return flightPlans.Values.ToArray();
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
