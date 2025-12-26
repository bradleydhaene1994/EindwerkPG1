using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Domein
{
    public class Customer
    {
        public Customer(string firstName, string lastName, string municipality, string street, DateTime birthDate, string houseNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Municipality = municipality;
            Street = street;
            BirthDate = birthDate;
            HouseNumber = houseNumber;
        }

        public Customer(int id, string firstName, string lastName, string municipality, string street, DateTime birthDate, string houseNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Municipality = municipality;
            Street = street;
            BirthDate = birthDate;
            HouseNumber = houseNumber;
        }

        public int Id { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Municipality { get; }
        public string Street { get; }
        public DateTime BirthDate { get; }
        public string HouseNumber { get; }
    }
}
