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
using CustomerSimulationBL.Mappers;
using CustomerSimulationBL.Services;

namespace CustomerSimulationBL.Managers
{
    public class SimulationService
    {
        private readonly AddressManager _addressManager;
        private readonly MunicipalityManager _municipalityManager;
        private readonly NameManager _nameManager;
        private readonly CustomerManager _customerManager;
        private readonly SimulationDataManager _simulationDataManager;
        private readonly SimulationStatisticsService _statisticsService;
        private readonly CustomerMappingService _mappingService;

        public SimulationService(AddressManager addressManager, MunicipalityManager municipalityManager, NameManager nameManager, CustomerManager customerManager, SimulationDataManager simulationDataManager, SimulationStatisticsService statisticsService, CustomerMappingService mappingService)
        {
            _addressManager = addressManager;
            _municipalityManager = municipalityManager;
            _nameManager = nameManager;
            _customerManager = customerManager;
            _simulationDataManager = simulationDataManager;
            _statisticsService = statisticsService;
            _mappingService = mappingService;
        }

        public void RunSimulation(SimulationData simData, SimulationSettings simSettings, int countryVersionId, List<Municipality>? allowedMunicipalities)
        {
            int simulationDataId = _simulationDataManager.UploadSimulationData(simData, countryVersionId);

            int houseNumberRulesId = _simulationDataManager.UploadHouseNumberRules(simSettings);

            int simulationSettingsId = _simulationDataManager.UploadSimulationSettings(simSettings, simulationDataId, houseNumberRulesId);

            if (simSettings.SelectedMunicipalities != null)
            {
                _simulationDataManager.UploadSelectedMunicipalities(simulationSettingsId, simSettings.SelectedMunicipalities);
            }

            List<Municipality> allMunicipalities = _municipalityManager.GetMunicipalityByCountryVersionID(countryVersionId);

            List<Municipality> effectiveMunicipalities = allowedMunicipalities != null && allowedMunicipalities.Any() ? allowedMunicipalities : allMunicipalities;

            List<Address> addresses = _addressManager.GetAddressesByCountryVersionID( countryVersionId, effectiveMunicipalities);

            List<CustomerDTO> customers = GenerateCustomers(simSettings, countryVersionId, effectiveMunicipalities, addresses);

            _customerManager.UploadCustomer(customers, simulationDataId, countryVersionId);

            SimulationStatistics statistics = _statisticsService.CalculateStatistics(customers, simData.DateCreated);

            int statsId = _simulationDataManager.UploadSimulationStatistics(statistics, simulationDataId);

            var municipalityStats = _statisticsService.CalculateCustomersPerMunicipality(customers, effectiveMunicipalities);

            _simulationDataManager.UploadMunicipalityStatistics(statsId, municipalityStats);
        }

        private List<CustomerDTO> GenerateCustomers(SimulationSettings settings, int countryVersionId, List<Municipality> municipalities, List<Address> addresses)
        {
            List<CustomerDTO> customers = new();

            var firstNames = _nameManager.GetFirstNamesByCountryVersionID(countryVersionId);
            var lastNames = _nameManager.GetLastNamesByCountryVersionID(countryVersionId);

            var selectedMunicipalities = settings.SelectedMunicipalities?.Where(m => m.IsSelected).ToList();

            for (int i = 0; i < settings.TotalCustomers; i++)
            {
                Municipality municipality = selectedMunicipalities == null || !selectedMunicipalities.Any() ? _municipalityManager.GetRandomMunicipality(municipalities) : _municipalityManager.GetRandomMunicipalityBySpecifiedList(selectedMunicipalities);

                Address address = _addressManager.GetRandomAddressByMunicipality(addresses, municipality);

                FirstName first = _nameManager.GetRandomFirstName(firstNames);
                LastName last = _nameManager.GetRandomLastName(lastNames);

                Gender gender = _nameManager.GetGenderByFirstName(first.Name);

                customers.Add(new CustomerDTO(first.Name, last.Name, municipality.Name, address.Street, _customerManager.GetRandomBirthdate(settings), _customerManager.GetRandomHouseNumber(settings), gender));
            }
            return customers;
        }

        public SimulationExport BuildSimulationExport(int simulationDataId, int countryVersionId)
        {
            SimulationData simData = _simulationDataManager.GetSimulationDataById(simulationDataId);
            SimulationSettings simSettings = _simulationDataManager.GetSimulationSettingsBySimulationDataID(simulationDataId);
            SimulationStatisticsResult stats = _statisticsService.BuildStatisticsResult(simulationDataId, countryVersionId);

            return new SimulationExport(simData, simSettings, stats);
        }

        public List<CustomerDTO> GetCustomerDTOBySimulationDataId(int simulationDataId)
        {
            return _mappingService.GetCustomerDTOBySimulationDataId(simulationDataId);
        }
    }
}