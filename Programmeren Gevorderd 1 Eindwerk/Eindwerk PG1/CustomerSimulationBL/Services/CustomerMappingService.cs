using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.DTOs;
using CustomerSimulationBL.Managers;

namespace CustomerSimulationBL.Services
{
    public class CustomerMappingService
    {
        private readonly CustomerManager _customerManager;

        public CustomerMappingService(CustomerManager customerManager)
        {
            _customerManager = customerManager;
        }

        public List<CustomerDTO> GetCustomerDTOBySimulationDataId(int simulationDataId)
        {
            List<Customer> customers = _customerManager.GetCustomerBySimulationDataID(simulationDataId);

            return customers.Select(c => new CustomerDTO(c.FirstName, c.LastName, c.Municipality, c.Street, c.BirthDate, c.HouseNumber, c.Gender)).ToList();
        }
    }
}
