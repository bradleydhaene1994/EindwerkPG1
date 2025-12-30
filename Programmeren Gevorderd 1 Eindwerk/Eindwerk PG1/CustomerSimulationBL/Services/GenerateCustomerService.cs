using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.DTOs;
using CustomerSimulationBL.Enumerations;
using CustomerSimulationBL.Interfaces;

namespace CustomerSimulationBL.Managers
{
    public class GenerateCustomerService : ISimulationService
    {
        private readonly AddressManager _addressmanager;
        private readonly MunicipalityManager _municipalitymanager;
        private readonly NameManager _namemanager;
        private readonly CustomerManager _customermanager;
        private readonly SimulationDataManager _simulationDataManager;
        public GenerateCustomerService(AddressManager addressmanager, MunicipalityManager municipalitymanager, NameManager namemanager, CustomerManager customermanager, SimulationDataManager simulationDataManager)
        {
            _addressmanager = addressmanager;
            _municipalitymanager = municipalitymanager;
            _namemanager = namemanager;
            _customermanager = customermanager;
            _simulationDataManager = simulationDataManager;
        }
        public void RunSimulation(SimulationData simData, SimulationSettings simSettings, int countryVersionId, List<Municipality>? allowedMunicipalities)
        {
            //Save SimulationData
            int simulationDataId = _simulationDataManager.UploadSimulationData(simData, countryVersionId);

            //Save HouseNumberRules
            int houseNumberRulesId = _simulationDataManager.UploadHouseNumberRules(simSettings);

            //Save SimulationSettings
            int simulationSettingsId = _simulationDataManager.UploadSimulationSettings(simSettings, simulationDataId, houseNumberRulesId);

            //Save Selected Municipalities
            if(simSettings.SelectedMunicipalities != null)
            {
                _simulationDataManager.UploadSelectedMunicipalities(simulationSettingsId, simSettings.SelectedMunicipalities);
            }

            //Determine municipalities for simulation
            List<Municipality> municipalities = allowedMunicipalities ?? _municipalitymanager.GetMunicipalityByCountryVersionID(countryVersionId);

            //load addresses of municipality
            List<Address> addresses = _addressmanager.GetAddressesByCountryVersionID(countryVersionId, municipalities);

            //Generate Customers
            List<CustomerDTO> customerDTOs = GenerateCustomers(simData, simSettings, countryVersionId, municipalities, addresses);

            //Save Customers
            _customermanager.UploadCustomer(customerDTOs, simulationDataId, countryVersionId);

            //Calculate statistics
            SimulationStatistics stats = CalculateStatistics(customerDTOs);

            //Save Statistics
            int simulationStatsId = _simulationDataManager.UploadSimulationStatistics(stats, simulationDataId);

            //Calculate Municipality Statistics
            var municipalityStatistics = CalculateCustomersPerMunicipality(customerDTOs, allowedMunicipalities);

            //upload Municipality Statistics
            _simulationDataManager.UploadMunicipalityStatistics(simulationStatsId, municipalityStatistics);
        }
        private List<CustomerDTO> GenerateCustomers(SimulationData simulationData, SimulationSettings settings, int countryVersionId, List<Municipality> municipalities, List<Address> addresses)
        {
            List<CustomerDTO> customers = new List<CustomerDTO>();

            var firstNames = _namemanager.GetFirstNamesByCountryVersionID(countryVersionId);
            var lastNames = _namemanager.GetLastNamesByCountryVersionID(countryVersionId);

            for (int i = 0; i < settings.TotalCustomers; i++)
            {
                Municipality municipality;

                if (settings.SelectedMunicipalities == null)
                {
                    municipality = _municipalitymanager.GetRandomMunicipality(municipalities);
                }
                else
                {
                    Municipality selected = _municipalitymanager.GetRandomMunicipalityByPercentage(settings.SelectedMunicipalities);

                    municipality = municipalities.First(m => m.Id == selected.Id);
                }

                Address randomAddres = _addressmanager.GetRandomAddressByMunicipality(addresses, municipality);
                string addressStreet = randomAddres.Street;

                string municipalityName = municipality.Name; ;

                FirstName randomFirstName = _namemanager.GetRandomFirstName(firstNames);
                string nameFirst = randomFirstName.Name;
                Gender gender = randomFirstName.Gender;

                LastName randomLastName = _namemanager.GetRandomLastName(lastNames);
                string nameLast = randomLastName.Name;

                DateTime randomBirthday = _customermanager.GetRandomBirthdate(settings);
                string houseNumber = _customermanager.GetRandomHouseNumber(settings);

                CustomerDTO customer = new CustomerDTO(nameFirst, nameLast, municipalityName, addressStreet, randomBirthday, houseNumber, gender);

                customers.Add(customer);
            }
            return customers;
        }

        private SimulationStatistics CalculateStatistics(List<CustomerDTO> customers)
        {
            DateTime today = DateTime.Today;
            var agesToday = customers.Select(c => CalculateAge(c.BirthDate, today)).ToList();

            int totalCustomers = customers.Count;
            double averageAgeSimulationDate = agesToday.Average();
            double averageAgeToday = agesToday.Average();
            int youngestAge = agesToday.Min();
            int oldestAge = agesToday.Max();

            SimulationStatistics simStatistics = new SimulationStatistics(totalCustomers, averageAgeSimulationDate, averageAgeToday, youngestAge, oldestAge);

            return simStatistics;
        }
        private int CalculateAge(DateTime birthDate, DateTime referenceDate)
        {
            int age = referenceDate.Year - birthDate.Year;

            if (referenceDate < birthDate.AddYears(age))
            {
                age--;
            }

            return age;
        }
        public List<MunicipalityStatistics> CalculateCustomersPerMunicipality(List<CustomerDTO> customers, List<Municipality> municipalities)
        {
            return customers
                   .GroupBy(c => c.Municipality)
                   .Select(g =>
                   {
                       Municipality municipality = municipalities.First(m => m.Name == g.Key);

                       return new MunicipalityStatistics(municipality, g.Count());
                   })
                   .ToList();
        }
        public List<MunicipalityStatistics> CalculateStreetsPerMunicipality(List<Address> addresses)
        {
            return addresses
                   .Where(a => a.Municipality != null)
                   .GroupBy(a => a.Municipality)
                   .Select(g => new MunicipalityStatistics(g.Key, g.Select(a => a.Street).Distinct().Count()))
                   .ToList();
        }
        public List<NameStatistics> CalculateNameStatistics(IEnumerable<string> names)
        {
            return names
                   .GroupBy(n => n)
                   .Select(g => new NameStatistics(g.Key, g.Count()))
                   .OrderByDescending(n => n.Count)
                   .ToList();
        }
        public SimulationStatisticsResult BuildStatisticsResult(SimulationStatistics general, List<CustomerDTO> customers, List<Address> addresses, List<Municipality> municipalities)
        {
            var municipalityStatistics = CalculateCustomersPerMunicipality(customers, municipalities);
            var streetStatistics = CalculateStreetsPerMunicipality(addresses);
            var maleNameStatistics = CalculateNameStatistics(customers.Where(c => c.Gender == Gender.Male).Select(c => c.FirstName));
            var femaleNameStatistics = CalculateNameStatistics(customers.Where(c => c.Gender == Gender.Female).Select(c => c.FirstName));
            var lastNameStatistics = CalculateNameStatistics(customers.Select(c => c.LastName));

            return new SimulationStatisticsResult(general, municipalityStatistics, streetStatistics, maleNameStatistics, femaleNameStatistics, lastNameStatistics);
        }
        public SimulationExport BuildSimulationExport(SimulationData simData, SimulationSettings simSettings, List<CustomerDTO> customers, List<Address> addresses, List<Municipality> municipalities)
        {
            //Calculate General Statistics
            SimulationStatistics generalStatistics = CalculateStatistics(customers);

            //Build StatisticsResult
            SimulationStatisticsResult statisticsResult = BuildStatisticsResult(generalStatistics, customers, addresses, municipalities);

            //Combine into one export object
            return new SimulationExport(simData, simSettings, statisticsResult);
        }
        public void ExportStatisticsToTxt(SimulationExport export, string filePath, CountryVersion countryVersion)
        {
            using StreamWriter writer = new StreamWriter(filePath);

            //Simulation Data
            writer.WriteLine("=== SIMULATION DATA ===");
            writer.WriteLine($"Client name: {export.SimulationData.Client}");
            writer.WriteLine($"Date created: {export.SimulationData.DateCreated}");
            writer.WriteLine($"Country: {countryVersion.Country}");
            writer.WriteLine($"Year: {countryVersion.Year}");

            //Simulation Settings
            writer.WriteLine("=== SIMULATION SETTINGS");
            writer.WriteLine($"Total customers: {export.SimulationSettings.TotalCustomers}");
            writer.WriteLine($"Age range: {export.SimulationSettings.MinAge} - {export.SimulationSettings.MaxAge}");
            writer.WriteLine($"House number range: {export.SimulationSettings.MinNumber} - {export.SimulationSettings.MaxNumber}");
            writer.WriteLine($"House numbers can have letters: {export.SimulationSettings.HasLetters}");
            writer.WriteLine($"Percentage of house numbers that can have letters: {export.SimulationSettings.PercentageLetters}%");
            writer.WriteLine("Municipalities selected: ");
            if(export.SimulationSettings.SelectedMunicipalities == null)
            {
                writer.WriteLine("No municipalities specified");
            }
            else
            {
                foreach(var sel in export.SimulationSettings.SelectedMunicipalities)
                {
                    writer.WriteLine($"{sel.Municipality.Name}: {sel.Percentage}%");
                }
            }

            //General Statistics
            writer.WriteLine("--- GENERAL STATISTICS ---");
            writer.WriteLine($"Youngest Customer: {export.SimulationStatisticsResult.General.AgeYoungestCustomer}");
            writer.WriteLine($"Oldest Customer: {export.SimulationStatisticsResult.General.AgeOldestCustomer}");
            writer.WriteLine($"Average age at date of simumlation: {export.SimulationStatisticsResult.General.AverageAgeOnSimulationDate}");
            writer.WriteLine($"Average age at current duate: {export.SimulationStatisticsResult.General.AverageAgeOnCurrentDate}");

            //Customers per municipality
            writer.WriteLine("--- CUSTOMERS PER MUNICIPALITY ---");
            foreach(var stat in export.SimulationStatisticsResult.CustomersPerMunicipality)
            {
                writer.WriteLine($"{stat.Municipality.Name}: {stat.Count}");
            }

            //Streets per municipality
            writer.WriteLine("--- STREETS PER MUNICIPALITY ---");
            foreach(var stat in export.SimulationStatisticsResult.StreetsPerMunicipality)
            {
                writer.WriteLine($"{stat.Municipality.Name}: {stat.Count}");
            }

            //Name statistics
            writer.WriteLine("--- MALE NAMES ---");
            foreach(var mn in export.SimulationStatisticsResult.MaleNames)
            {
                writer.WriteLine($"{mn.Name}: {mn.Count}");
            }

            writer.WriteLine("--- FEMALE NAMES ---");
            foreach(var fn in export.SimulationStatisticsResult.FemaleName)
            {
                writer.WriteLine($"{fn.Name}: {fn.Count}");
            }

            writer.WriteLine("--- LAST NAMES ---");
            foreach(var ln in export.SimulationStatisticsResult.LastNames)
            {
                writer.WriteLine($"{ln.Name}: {ln.Count}");
            }
        }
        public void ExportCustomerDataToTxt(SimulationData simulationData, List<CustomerDTO> customers, string filePath, CountryVersion countryVersion)
        {
            using StreamWriter writer = new StreamWriter(filePath);

            //Header data
            writer.WriteLine("--- SIMULATION DATA ---");
            writer.WriteLine($"Client: {simulationData.Client}");
            writer.WriteLine($"Date Created: {simulationData.DateCreated}");
            writer.WriteLine($"Country: {countryVersion.Country}");
            writer.WriteLine($"Year: {countryVersion.Year}");

            //Customer Data
            writer.WriteLine("First Name; Last Name; Municipality; Street; HouseNumber; BirthDate");

            foreach(CustomerDTO c in customers)
            {
                writer.WriteLine($"{c.FirstName}; {c.LastName}, {c.Municipality}; {c.Street}; {c.HouseNumber}; {c.BirthDate}");
            }
        }
    }
}
