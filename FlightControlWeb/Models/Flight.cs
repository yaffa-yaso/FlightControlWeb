using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class Flight
    {
        [JsonProperty, JsonPropertyName("passengers")]
        public int passengers { get; set; }

        [JsonProperty, JsonPropertyName("company_name")]
        public string company_name { get; set; }

        [JsonProperty, JsonPropertyName("flight_id")]
        public string flight_id { get; set;}

        [JsonProperty, JsonPropertyName("longitude")]
        public double longitude { get; set;}

        [JsonProperty, JsonPropertyName("latitude")]
        public double latitude { get; set; }

        [JsonProperty, JsonPropertyName("date_time")]
        public string date_time { get; set; }

        [JsonProperty, JsonPropertyName("is_external")]
        public bool is_external { get; set; }


    }
}
