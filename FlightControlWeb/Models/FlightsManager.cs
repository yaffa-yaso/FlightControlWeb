using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class FlightsManager: IFlightsManager
    {

       private static ConcurrentDictionary<string, FlightPlan> flights = new ConcurrentDictionary<string, FlightPlan>();

        public void AddFlight(FlightPlan f)
        {
            FlightPlan x = f;
            Random rnd = new Random();
            string key = rnd.Next(9).ToString() + rnd.Next(9).ToString() + rnd.Next(9).ToString() + rnd.Next(9).ToString();
            key += (char)rnd.Next('A', 'Z') + (char)rnd.Next('a', 'z');
            bool item =  flights.TryAdd(key, f);
        }

       public FlightPlan GetFlight(string id)
        {
            FlightPlan flight;
            if (flightPlans.TryGetValue(id, out flight))
            {
                return flight;
            }
            else
            {
                // doesn't exist
                return null;
            }

        }

        public void DeleteFlight(string id)
        {
            FlightPlan removedItem;
            flights.TryRemove(id, out removedItem);
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
