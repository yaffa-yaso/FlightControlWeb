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

        // GET: api/Servers
        [HttpGet]
        public IEnumerable<Server> Get()
        {
            return serverManager.GetAllServers();
        }

        // POST: api/Servers
        [HttpPost]
        public void Post([FromBody] Server s)
        {
            serverManager.AddServer(s);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            serverManager.DeleteServer(id);
        }
    }
}
