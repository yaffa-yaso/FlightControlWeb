using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public interface IServerManager
    {
        public Server AddServer(Server s);
        public IEnumerable<Server> GetAllServers();
        public void DeleteServer(string id);
    }
}
