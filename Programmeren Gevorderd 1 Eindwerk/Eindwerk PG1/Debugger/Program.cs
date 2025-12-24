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
            
            Country Belgium = new Country(1, "Belgium");
            CountryVersion Belgium2024 = new CountryVersion(2024, Belgium);
            Belgium2024.Id = 6;

            var addresses = addressRepo.GetAddressesByCountryVersionID(Belgium2024.Id);

            foreach(var address in addresses)
            {
                Console.WriteLine(address.ToString());
            }
        }
    }
}
