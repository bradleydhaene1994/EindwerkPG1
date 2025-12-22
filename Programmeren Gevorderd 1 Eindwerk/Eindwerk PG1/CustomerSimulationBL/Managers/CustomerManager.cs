using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Interfaces;

namespace CustomerSimulationBL.Managers
{
    public class CustomerManager
    {
        private ICustomerRepository _customerRepo;

        public CustomerManager(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public void UploadCustomer(IEnumerable<Customer> customers)
        {
            _customerRepo.UploadCustomer(customers);
        }
    }
}
