using CustomerSimulationDL.FileReaders;
using CustomerSimulationBL.Domein;
using CustomerSimulationDL.Repositories;

namespace Debugger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionstring = "Data Source=BRADLEY\\SQLEXPRESS;Initial Catalog=SimulationCustomer;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            AddressRepository addressRepo = new AddressRepository(connectionstring);
            CustomerRepository customerRepo = new CustomerRepository(connectionstring);
            
            Country Belgium = new Country(1, "Belgium");
            CountryVersion Belgium2024 = new CountryVersion(2024, Belgium);
            Belgium2024.Id = 6;

            SimulationData simulationData = new SimulationData(Belgium2024, "Bradley", DateTime.Now);
            simulationData.Id = 1;

            var addresses = addressRepo.GetAddressesByCountryVersionID(Belgium2024.Id);
            var customers = customerRepo.GetCustomerBySimulationDataID(simulationData.Id);

            foreach(var customer in customers)
            {
                Console.WriteLine($"{customer.FirstName} {customer.LastName}");
            }
        }
    }
}
