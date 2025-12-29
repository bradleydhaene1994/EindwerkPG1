using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.DTOs;

namespace CustomerSimulationBL.Interfaces
{
    public interface ICustomerRepository
    {
        void UploadCustomer(IEnumerable<CustomerDTO> customers, int simulationDataId, int countryVersionId);
        List<Customer> GetCustomerBySimulationDataID(int simulationDataID);
    }
}
