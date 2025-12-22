using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Domein
{
    public class Customer
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public SimulationData simulationData { get; set; }
        public DateTime BirthDate { get; set; }
        public string HouseNumber { get; set; }
    }
}
