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
        public void Test1()
        {
            // Arrange
            var mockRepo = new Mock<IFlightPlanManager>();
            mockRepo.Setup(repo => repo.AddFlight(It.IsAny<FlightPlan>()))
                .Verifiable();
            var controller = new FlightPlanController(mockRepo.Object);
            var newFlightPlan = GetTestFlightPlan();

            // Act
            var key = controller.Post(newFlightPlan);
            var result = controller.Get(key);

            // Assert
            var fpResult = Assert.IsType<FlightPlan>(result);
            Assert.Equal("SwissAir", fpResult.company_name);
            Assert.Equal(100, fpResult.passengers);
            Assert.Equal(20.0, fpResult.initial_location.longitude);
            Assert.Equal(30.2, fpResult.initial_location.latitude);
            Assert.Equal("2020-12-27T01:56:21Z", fpResult.initial_location.date_time);
            mockRepo.Verify();
        }

        private FlightPlan GetTestFlightPlan()
        {
            return new FlightPlan{
                passengers = 100,
                company_name = "SwissAir",
                initial_location = new InitialLocation{ longitude = 20.0, latitude = 30.2, date_time = "2020-12-27T01:56:21Z"},
                segments = new List<Segment>{ new Segment { longitude = 33.23, latitude = 31.56, timespan_seconds = 850.0} }
            };
        }
    }
}
