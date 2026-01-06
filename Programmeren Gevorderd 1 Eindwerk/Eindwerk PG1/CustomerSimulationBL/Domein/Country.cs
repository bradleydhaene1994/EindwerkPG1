using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationBL.Domein
{
    public class Country
    {
        public Country(string name)
        {
            Name = name;
        }

        public Country(int id, string name)
        {
            Id = id;
            Name = name;
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
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new CountryException("Country name cannot be empty");
                else _name = value.Trim();
            }
        }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
