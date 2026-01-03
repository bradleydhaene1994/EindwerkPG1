using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Interfaces;
using CustomerSimulationBL.Managers;
using CustomerSimulationDL.Repositories;

namespace CustomerSimulationUtils
{
    public class RepositoryFactory
    {
        public IAddressRepository GetAddressRepository(string connectionstring)
        {
            return new AddressRepository(connectionstring);
        }
        public ICountryVersionRepository GetCountryVersionRepository(string connectionstring)
        {
            return new CountryVersionRepository(connectionstring);
        }
        public ICustomerRepository GetCustomerRepository(string connectionstring)
        {
            return new CustomerRepository(connectionstring);
        }
        public IMunicipalityRepository GetMunicipalityRepository(string connectionstring)
        {
            return new MunicipalityRepository(connectionstring);
        }
        public INameRepository GetNameRepository(string connectionstring)
        {
            return new NameRepository(connectionstring);
        }
        public ISimulationRepository GetSimulationRepository(string connectionstring)
        {
            return new SimulationRepository(connectionstring);
        }
    }
}
