using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.DTOs;
using CustomerSimulationBL.Enumerations;

namespace CustomerSimulationBL.Mappers
{
    public static class CustomerMapper
    {
        public static CustomerDTO ToDTO(string firstName, string lastName, string municipality, string street, DateTime birthDate, string houseNumber, Gender gender)
        {
            CustomerDTO cDto = new CustomerDTO(firstName, lastName, municipality, street, birthDate, houseNumber, gender);

            return cDto;
        }
    }
}
