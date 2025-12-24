using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Domein
{
    public class SimulationData
    {
        public SimulationData(string client, DateTime dateCreated)
        {
            Client = client;
            DateCreated = dateCreated;
        }

        public SimulationData(int id, string client, DateTime dateCreated)
        {
            Id = id;
            Client = client;
            DateCreated = dateCreated;
        }

        public int Id { get; set; }
        public string Client { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
