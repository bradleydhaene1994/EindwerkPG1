using CustomerSimulationDL.FileReaders;
using CustomerSimulationBL.Domein;
using CustomerSimulationDL.Repositories;

namespace Debugger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string BelgiumAddressPath = "C:\\Users\\dhaen\\EindwerkPG1\\SourceData\\België\\belgium_streets2.csv";
            string BelgiumFirstNameMalePath = "C:\\Users\\dhaen\\EindwerkPG1\\SourceData\\België\\mannennamen_belgie.csv";
            string BelgiumFirstNameFemalePath = "C:\\Users\\dhaen\\EindwerkPG1\\SourceData\\België\\vrouwennamen_belgie.csv";
            string BelgiumLastNamePath = "C:\\Users\\dhaen\\EindwerkPG1\\SourceData\\België\\Familienamen_2024_Belgie.csv";

            DateTime dateTime = DateTime.Now;
            DateTime BradleyBirthday = new DateTime(1994, 11, 14);
            DateTime JamesBirthday = new DateTime(1965, 11, 15);
            DateTime ElkeBirthday = new DateTime(1969, 02, 26);
            Country Belgium = new Country(1, "Belgium");
            CountryVersion Belgium2024 = new CountryVersion(2024, Belgium);
            SimulationData simulationData = new SimulationData(Belgium2024, "Bradley", dateTime);
            SimulationSettings simulationSettings = new SimulationSettings(simulationData, null, 15, 5, 15, "Test");
            SimulationStatistics simulationStatistics = new SimulationStatistics(simulationData, 15, null, 8, 8, 5, 15);
            Customer customer1 = new Customer(Belgium2024, "Bradley", "D'Haene", "Kluisbergen", "Berchemstraat", simulationData, BradleyBirthday, "46");
            Customer customer2 = new Customer(Belgium2024, "James", "D'Haene", "Kluisbergen", "Berchemstraat", simulationData, JamesBirthday, "46");
            Customer customer3 = new Customer(Belgium2024, "Elke", "Ongena", "Kluisbergen", "Berchemstraat", simulationData, ElkeBirthday, "46");

            List<Customer> customers = new List<Customer>();

            customers.Add(customer1);
            customers.Add(customer2);
            customers.Add(customer3);

            CsvFileReader csvFileReader = new CsvFileReader();

            var Addresses = csvFileReader.ReadAddresses(BelgiumAddressPath);
            var FirstNamesMale = csvFileReader.ReadFirstNames(BelgiumFirstNameMalePath);
            var FirstNamesFemale = csvFileReader.ReadFirstNames(BelgiumFirstNameFemalePath);
            var LastNames = csvFileReader.ReadLastNames(BelgiumLastNamePath);


        }
    }
}
