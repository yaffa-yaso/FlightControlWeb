using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class FlightPlan
    {
        [JsonProperty, JsonPropertyName("passengers")]
        public int passengers { get; set; }
        
        [JsonProperty, JsonPropertyName("company_name")]
        public string company_name { get; set; }
        
        [JsonProperty, JsonPropertyName("initial_location")]
        public InitialLocation initial_location { get; set; }
        
        [JsonProperty, JsonPropertyName("segments")]
        public IEnumerable<Segment> segments { get; set; }
    }
}
