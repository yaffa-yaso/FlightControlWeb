using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private ServerManager serverManager = new ServerManager();

        // GET: api/servers
        [HttpGet]
        public IEnumerable<Server> Get()
        {
            return serverManager.GetAllServers();
        }

        // POST: api/servers
        [HttpPost]
        public void Post([FromBody] Server s)
        {
            serverManager.AddServer(s);
        }

        // DELETE: api/servers/{id}
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            serverManager.DeleteServer(id);
        }
    }
}
