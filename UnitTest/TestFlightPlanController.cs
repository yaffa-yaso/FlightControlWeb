using System;
using Xunit;
using FlightControlWeb.Models;
using FlightControlWeb.Controllers;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace UnitTest
{
    public class TestFlightPlanController
    {
        [Fact]
        public void InvalidPostCompanyName()
        {
            var mockRepo1 = new Mock<IFlightPlanManager>();
            var mockRepo2 = new Mock<IServerManager>();
            var flightPlanController = new FlightPlanController(mockRepo1.Object, mockRepo2.Object);
            Assert.Null(flightPlanController.Post(GetFligtPlanTest(120, null, true)));
        }

        [Fact]
        public void InvalidPostPassengers()
        {
            var mockRepo1 = new Mock<IFlightPlanManager>();
            var mockRepo2 = new Mock<IServerManager>();
            var flightPlanController = new FlightPlanController(mockRepo1.Object, mockRepo2.Object);
            Assert.Null(flightPlanController.Post(GetFligtPlanTest(-20, "SwissAir", true)));
        }

        [Fact]
        public void InvalidPostSegments()
        {
            var mockRepo1 = new Mock<IFlightPlanManager>();
            var mockRepo2 = new Mock<IServerManager>();
            var flightPlanController = new FlightPlanController(mockRepo1.Object, mockRepo2.Object);
            Assert.Null(flightPlanController.Post(GetFligtPlanTest(120, "SwissAir", false)));
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
