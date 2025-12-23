using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Domein
{
    public class SimulationData
    {
        public SimulationData(CountryVersion countryVersion, string client, DateTime dateCreated)
        {
            CountryVersion = countryVersion;
            Client = client;
            DateCreated = dateCreated;
        }

        public int Id { get; set; }
        public CountryVersion CountryVersion { get; set; }
        public string Client { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
