using System;
using Xunit;
using FlightControlWeb.Models;
using FlightControlWeb.Controllers;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FlightControlWebTest
{
    public class FlightPlanControllerTest
    {
        [Fact]
        public void InvalidPostCompanyName()
        {
            var mockRepo = new Mock<IFlightPlanManager>();
            var flightPlanController = new FlightPlanController(mockRepo.Object);
            Assert.Throws<ArgumentException>(() => flightPlanController.Post(GetFligtPlanTest(120, null, true)));
        }

        [Fact]
        public void InvalidPostPassengers()
        {
            var mockRepo = new Mock<IFlightPlanManager>();
            var flightPlanController = new FlightPlanController(mockRepo.Object);
            Assert.Throws<ArgumentOutOfRangeException>(() => flightPlanController.Post(GetFligtPlanTest(-20, "SwissAir", true)));
        }

        [Fact]
        public void InvalidPostSegments()
        {
            var mockRepo = new Mock<IFlightPlanManager>();
            var flightPlanController = new FlightPlanController(mockRepo.Object);
            Assert.Throws<ArgumentException>(() => flightPlanController.Post(GetFligtPlanTest(120, "SwissAir", false)));
        }

        [Fact]
        public void invalidGetId() {
            var mockRepo = new Mock<IFlightPlanManager>();
            var flightPlanController = new FlightPlanController(mockRepo.Object);
            mockRepo.Setup(p => p.GetFlight("111111")).Throws<ArgumentException>();
            Assert.Throws<ArgumentException>(() => flightPlanController.Get("111111"));
        }

        private FlightPlan GetFligtPlanTest(int passengersT, string companyT, bool segmentsT)
        {
            if (segmentsT == true)
            {
                return new FlightPlan
                {
                    passengers = passengersT,
                    company_name = companyT,
                    initial_location = new InitialLocation { longitude = 20.0, latitude = 30.2, date_time = "2020-12-27T01:56:21Z" },
                    segments = new List<Segment> { new Segment { longitude = 33.23, latitude = 31.56, timespan_seconds = 850.0 } }
                };
            }
            else
            {
               return new FlightPlan
                {
                    passengers = passengersT,
                    company_name = companyT,
                    initial_location = new InitialLocation { longitude = 20.0, latitude = 30.2, date_time = "2020-12-27T01:56:21Z" },
                };
            }
            
        }
    }
}
