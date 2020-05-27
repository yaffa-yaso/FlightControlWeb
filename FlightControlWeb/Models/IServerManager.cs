using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public interface IServerManager
    {
        public void AddServer(Server s);
        public IEnumerable<Server> GetAllServers();
        public void DeleteServer(string id);
    }
}
