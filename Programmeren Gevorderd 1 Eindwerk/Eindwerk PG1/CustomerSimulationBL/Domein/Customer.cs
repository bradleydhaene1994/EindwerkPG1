using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Domein
{
    public class Customer
    {
        public Customer(CountryVersion countryVersion, string firstName, string lastName, string municipality, string street, SimulationData simulationData, DateTime birthDate, string houseNumber)
        {
            CountryVersion = countryVersion;
            FirstName = firstName;
            LastName = lastName;
            Municipality = municipality;
            Street = street;
            this.simulationData = simulationData;
            BirthDate = birthDate;
            HouseNumber = houseNumber;
        }

        public int Id { get; set; }
        public CountryVersion CountryVersion { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Municipality { get; set; }
        public string Street { get; set; }
        public SimulationData simulationData { get; set; }
        public DateTime BirthDate { get; set; }
        public string HouseNumber { get; set; }
    }
}
