using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Exceptions;

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
        private int _id;
        public int Id
        {
            get => _id;
            private set
            {
                _id = value;
            }
        }
        private Municipality? _municipality;
        public Municipality? Municipality
        {
            get => _municipality;
            set
            {
                _municipality = value;
            }
        }
        private string _street;
        public string Street
        {
            get => _street;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new AddressException("Street name cannot be empty.");
                else _street = value.Trim();
            }
        }
        public override string ToString()
        {
            return $"{Municipality?.Name}, {Street}";
        }
    }
}
