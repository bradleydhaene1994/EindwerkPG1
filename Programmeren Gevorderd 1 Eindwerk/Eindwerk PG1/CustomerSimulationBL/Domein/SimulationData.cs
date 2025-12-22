using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Domein
{
    public class SimulationData
    {
        public int Id { get; set; }
        public Country Country { get; set; }
        public string ClientName { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
