using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.DTOs;

namespace CustomerSimulationBL.Mappers
{
    public static class CustomerMapper
    {
        public static CustomerDTO ToDTO(string firstName, string lastName, string municipality, string street, DateTime birthDate, string houseNumber)
        {
            CustomerDTO cDto = new CustomerDTO(firstName, lastName, municipality, street, birthDate, houseNumber);

            return cDto;
        }

    }
}
