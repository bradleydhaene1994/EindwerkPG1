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
            NameRepository nameRepo = new NameRepository(connectionstring);
            SimulationRepository simRepo = new SimulationRepository(connectionstring);
            
            Country Belgium = new Country(1, "Belgium");
            CountryVersion Belgium2024 = new CountryVersion(2024);
            Belgium2024.Id = 6;

            var simData = simRepo.GetAllSimulationData();
            var simSettings = simRepo.GetSimulationSettingsBySimulationDataID(1);
            var simStatistics = simRepo.GetSimulationStatisticsBySimulationDataID(1);

            Console.WriteLine($"{simStatistics.TotalCustomers}");

        }
    }
}
