using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class ServerManager
    {
        List<Server> Servers = new List<Server>();
        public void AddServer(Server s)
        {
            Servers.Add(s);
        }
        public IEnumerable<Server> GetAllServers()
        {
            return Servers;
        }
        public void DeleteServer(string id)
        {
            Server s = Servers.Where(x => x.ServerId == id).FirstOrDefault();
            Servers.Remove(s);
        }
    }
}
