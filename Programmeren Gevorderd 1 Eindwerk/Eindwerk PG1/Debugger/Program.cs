using CustomerSimulationDL.FileReaders;
using CustomerSimulationBL.Domein;
using CustomerSimulationDL.Repositories;
using CustomerSimulationBL.Managers;

namespace Debugger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "";
            string belgiumAddresses = "";
            CsvFileReader csvReader = new CsvFileReader();
            AddressRepository addressRepo = new AddressRepository(connectionString);

            var addresses = csvReader.ReadAddresses(belgiumAddresses);

            //addressRepo.UploadAddress();

        }
    }
}
