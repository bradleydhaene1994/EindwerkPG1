using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Interfaces;
using CustomerSimulationDL.Repositories;

namespace CustomerSimulationUtils
{
    public class RepositoryFactory
    {
        public static IAddressRepository GetAddressRepository(string connectionstring)
        {
            return new AddressRepository(connectionstring);
        }
        public static ICountryVersionRepository GetCountryVersionRepository(string connectionstring)
        {
            return new CountryVersionRepository(connectionstring);
        }
        public static ICustomerRepository GetCustomerRepository(string connectionstring)
        {
            return new CustomerRepository(connectionstring);
        }
        public static IMunicipalityRepository GetMunicipalityRepository(string connectionstring)
        {
            return new MunicipalityRepository(connectionstring);
        }
        public static INameRepository GetNameRepository(string connectionstring)
        {
            return new NameRepository(connectionstring);
        }
        public static ISimulationRepository GetSimulationRepository(string connectionstring)
        {
            return new SimulationRepository(connectionstring);
        }
    }
}
