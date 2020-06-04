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
        private IServerManager serverManager;

        public ServersController(IServerManager SManager)
        {
            this.serverManager = SManager;
        }

        // GET: api/Servers - returns all servers synchronized with
        [HttpGet]
        public IEnumerable<Server> Get()
        {
            return serverManager.GetAllServers();
        }

        // POST: api/Servers
        [HttpPost]
        public Server Post([FromBody] Server s)
        {
            try
            {
                return serverManager.AddServer(s);
            }
            catch (ArgumentException e)
            {
                Console.Out.WriteLine(e);
                return null;
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            serverManager.DeleteServer(id);
        }
    }
}
