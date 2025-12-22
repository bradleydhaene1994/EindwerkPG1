using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Domein
{
    public class Address
    {
        public Address(Municipality? municipality, string street)
        {
            Municipality = municipality;
            Street = street;
        }

        public int Id { get; set; }
        public Municipality? Municipality { get; set; }
        public string Street { get; set; }

    }
}
