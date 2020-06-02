using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class FlightPlanManager: IFlightPlanManager
    {

       private static ConcurrentDictionary<string, FlightPlan> flights = new ConcurrentDictionary<string, FlightPlan>();

        public string AddFlight(FlightPlan f)
        {
            string key = GetId(f);
            if (key != null)
            {
                return key;
            }
            FlightPlan x = f;
            Random rnd = new Random();
            key = GenerateNewRandom();
            bool item = flights.TryAdd(key, f);
            return key;
        }

       public FlightPlan GetFlight(string id)
        {
            FlightPlan flight;
            if (flights.TryGetValue(id, out flight))
            {
                return flight;
            }
            else
            {
                // doesn't exist
                throw new ArgumentException("flight id does not exists in records");
            }

        }
       
        public string GetId(FlightPlan f)
        {
           foreach (var display in flights)
           {
               if (display.Value.Equals(f))
               {
                   return display.Key;
               }
           }
           return null;
       }

        public void DeleteFlight(string id)
        {
            GetFlight(id);
            FlightPlan removedItem;
            flights.TryRemove(id, out removedItem);
        }
        public IEnumerable<FlightPlan> GetAllFlightPlans()
        {
            return flights.Values.ToArray();
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
