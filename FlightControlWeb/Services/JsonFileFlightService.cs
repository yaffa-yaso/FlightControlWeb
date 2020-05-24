using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using FlightControlWeb.Models;
using FlightControlWeb.Services;
using Microsoft.AspNetCore.Hosting;

namespace FlightControlWeb.Services
{
    public class JsonFileFlightService
    {
        public JsonFileFlightService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "flight.json"); }
        }

        public IEnumerable<FlightPlan> GetProducts()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<FlightPlan[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

    }

}