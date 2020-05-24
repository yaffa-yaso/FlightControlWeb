using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class Flight
    {
        
        public string id{get; set;}
        public double longitude{get; set;}
        public double latitude { get; set; }
        public int passengers { get; set; }
        public string company_name { get; set; }
        public string date_time { get; set; }

        public bool is_external { get; set; }

        public Flight(string id, double longitude, double latitude, int passengers, string company_name, string date_time)
        {
            this.id = id;
            this.longitude = longitude;
            this.latitude = latitude;
            this.passengers = passengers;
            this.company_name = company_name;
            this.date_time = date_time;
        }

    }
}
