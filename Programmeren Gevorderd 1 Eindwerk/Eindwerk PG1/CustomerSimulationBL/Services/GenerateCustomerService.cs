using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.DTOs;
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
        public void RunSimulation(SimulationData simData, SimulationSettings simSettings, int countryVersionId)
        {
            //Save SimulationData
            int simulationDataId = _simulationDataManager.UploadSimulationData(simData, countryVersionId);

            //Save HouseNumberRules
            int houseNumberRulesId = _simulationDataManager.UploadHouseNumberRules(simSettings);

            //Save SimulationSettings
            _simulationDataManager.UploadSimulationSettings(simSettings, simulationDataId, houseNumberRulesId);

            //Generate Customers
            List<CustomerDTO> customerDTOs = GenerateCustomers(simData, simSettings, countryVersionId);

            //Save Customers
            _customermanager.UploadCustomer(customerDTOs, simulationDataId);

            //Calculate statistics
            SimulationStatistics stats = CalculateStatistics(customerDTOs);

            //Save Statistics
            _simulationDataManager.UploadSimulationStatistics(stats, simulationDataId);
        }
        private List<CustomerDTO> GenerateCustomers(SimulationData simulationData, SimulationSettings settings, int countryVersionId)
        {   
            List<CustomerDTO> customers = new List<CustomerDTO>();

            var addresses = _addressmanager.GetAddressesByCountryVersionID(countryVersionId);
            var municipalities = _municipalitymanager.GetMunicipalityByCountryVersionID(countryVersionId);
            var firstNames = _namemanager.GetFirstNamesByCountryVersionID(countryVersionId);
            var lastNames = _namemanager.GetLastNamesByCountryVersionID(countryVersionId);

            for (int i = 0; i < settings.TotalCustomers; i++)
            {
                Address randomAddres = _addressmanager.GetRandomAddress(addresses);
                string addressStreet = randomAddres.Street;

                Municipality randomMunicipality = _municipalitymanager.GetRandomMunicipality(municipalities);
                string municipalityName = randomMunicipality.Name;
                FirstName randomFirstName = _namemanager.GetRandomFirstName(firstNames);
                string nameFirst = randomFirstName.Name;

                LastName randomLastName = _namemanager.GetRandomLastName(lastNames);
                string nameLast = randomLastName.Name;

                DateTime randomBirthday = _customermanager.GetRandomBirthdate(settings);
                string houseNumber = _customermanager.GetRandomHouseNumber(settings);

                CustomerDTO customer = new CustomerDTO(nameFirst, nameLast, municipalityName, addressStreet, randomBirthday, houseNumber);

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
            double averageAgeToday= agesToday.Average();
            int youngestAge = agesToday.Min();
            int oldestAge = agesToday.Max();

            SimulationStatistics simStatistics = new SimulationStatistics(totalCustomers, null, averageAgeSimulationDate, averageAgeToday, youngestAge, oldestAge);

            return simStatistics;
        }
        private int CalculateAge(DateTime birthDate, DateTime referenceDate)
        {
            int age = referenceDate.Year - birthDate.Year;
            
            if(referenceDate < birthDate.AddYears(age))
            {
                age--;
            }

            return age;
        }
    }
}
