using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Interfaces;
using CustomerSimulationBL.Managers;
using CustomerSimulationBL.Services;
using CustomerSimulationUtils;

namespace CustomerSimulationUI
{
    public class ServiceProvider
    {
        public IUploadService UploadService { get; }
        public SimulationService SimulationService { get; }
        public ICountryVersionRepository CountryVersionRepository { get; }
        public MunicipalityManager MunicipalityManager { get; }
        public SimulationDataManager SimulationDataManager { get; }
        public SimulationStatisticsService SimulationStatisticsService { get; }
        public SimulationExportService SimulationExportService { get; }

        public ServiceProvider(string connectionString)
        {
            var repositoryFactory = new RepositoryFactory();
            var fileReaderFactory = new FileReaderFactory();

            var countryVersionRepo = repositoryFactory.GetCountryVersionRepository(connectionString);
            var addressRepo = repositoryFactory.GetAddressRepository(connectionString);
            var municipalityRepo = repositoryFactory.GetMunicipalityRepository(connectionString);
            var nameRepo = repositoryFactory.GetNameRepository(connectionString);
            var customerRepo = repositoryFactory.GetCustomerRepository(connectionString);
            var simulationRepo = repositoryFactory.GetSimulationRepository(connectionString);

            var csvReader = fileReaderFactory.GetCsvReader();
            var jsonReader = fileReaderFactory.GetJsonReader();
            var txtReader = fileReaderFactory.GetTxtReader();

            var addressManager = new AddressManager(addressRepo);
            var municipalityManager = new MunicipalityManager(municipalityRepo);
            var nameManager = new NameManager(nameRepo);
            var customerManager = new CustomerManager(customerRepo);
            var simulationDataManager = new SimulationDataManager(simulationRepo);

            var statisticsService = new SimulationStatisticsService(simulationDataManager, customerManager, municipalityManager, addressManager, nameManager);
            var customerMappingService = new CustomerMappingService(customerManager);

            SimulationService = new SimulationService(addressManager, municipalityManager, nameManager, customerManager, simulationDataManager, statisticsService, customerMappingService);

            UploadService = new UploadService(addressRepo, municipalityRepo, nameRepo, countryVersionRepo, csvReader, txtReader, jsonReader);

            CountryVersionRepository = countryVersionRepo;
            MunicipalityManager = municipalityManager;
            SimulationDataManager = simulationDataManager;
            SimulationStatisticsService = statisticsService;
            SimulationExportService = new SimulationExportService();
        }
    }
}
