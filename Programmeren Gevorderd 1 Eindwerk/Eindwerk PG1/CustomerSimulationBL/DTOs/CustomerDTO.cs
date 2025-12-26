using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.DTOs
{
    public class CustomerDTO
    {
        public CustomerDTO(int id, string name, string firstName, string lastName, string municipality, string street, DateTime birthDate, string houseNumber)
        {
            Id = id;
            Name = name;
            FirstName = firstName;
            LastName = lastName;
            Municipality = municipality;
            Street = street;
            BirthDate = birthDate;
            HouseNumber = houseNumber;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Municipality { get; set; }
        public string Street { get; set; }
        public DateTime BirthDate { get; set; }
        public string HouseNumber { get; set; }
    }
}
