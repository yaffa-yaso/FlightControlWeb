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
    public class ServersControllerTest
    {
        [Fact]
        public void InvalidPostServerId()
        {
            var mockRepo = new Mock<IServerManager>();
            var serverController = new ServersController(mockRepo.Object);
            Assert.Null(serverController.Post(GetServerTest(null, "http://localhost:58493")));
        }

        [Fact]
        public void InvalidPostServerURL()
        {
            var mockRepo = new Mock<IServerManager>();
            var serverController = new ServersController(mockRepo.Object);
            Assert.Null(serverController.Post(GetServerTest("12345", null)));
        }

        [Fact]
        public void invalidDeleteId()
        {
            var mockRepo = new Mock<IServerManager>();
            var serverController = new ServersController(mockRepo.Object);
            mockRepo.Setup(p => p.DeleteServer("111111")).Throws<InvalidOperationException>();
            Assert.Throws<InvalidOperationException>(() => serverController.Delete("111111"));
        }

        private Server GetServerTest(string id, string URL)
        {
            return new Server { ServerId = id, ServerURL = URL};

        }
    }
}
