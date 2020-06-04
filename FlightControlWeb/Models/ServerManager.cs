using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class ServerManager: IServerManager
    {
        private List<Server> servers = new List<Server>();
        public Server AddServer(Server s)
        {
            servers.Add(s);
            return s;
        }
        public IEnumerable<Server> GetAllServers()
        {
            return servers;
        }
        public void DeleteServer(string id)
        {
            Server s = servers.Where(x => x.ServerId == id).First();
            servers.Remove(s);
        }
    }
}
