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

        public Address(int id, Municipality? municipality, string street)
        {
            Id = id;
            Municipality = municipality;
            Street = street;
        }

        public int Id { get; set; }
        public Municipality? Municipality { get; set; }
        public string Street { get; set; }
        public override string ToString()
        {
            return $"{Municipality?.Name}, {Street}";
        }
    }
}
