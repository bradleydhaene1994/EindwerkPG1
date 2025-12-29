using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.Configuration;
using CustomerSimulationBL.Interfaces;
using CustomerSimulationUtils;
using System.IO;
using CustomerSimulationBL.Services;
using CustomerSimulationBL.Managers;

namespace CustomerSimulationUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ICountryVersionRepository _countryVersionRepository;
        private readonly IUploadService _uploadService;
        private readonly GenerateCustomerService _generateCustomerService;
        private readonly MunicipalityManager _municipalityManager;
        private readonly SimulationDataManager _simulationDataManager;
        public MainWindow()
        {
            var builder = new ConfigurationBuilder().
                              SetBasePath(AppContext.BaseDirectory).
                              AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            string connectionString = configuration.GetSection("ConnectionStrings")["SQLserver"];

            RepositoryFactory repositoryFactory = new RepositoryFactory();
            FileReaderFactory readerFactory = new FileReaderFactory();

            _countryVersionRepository = repositoryFactory.GetCountryVersionRepository(connectionString);

            IAddressRepository addressRepository = repositoryFactory.GetAddressRepository(connectionString);
            IMunicipalityRepository municipalityRepository = repositoryFactory.GetMunicipalityRepository(connectionString);
            INameRepository nameRepository = repositoryFactory.GetNameRepository(connectionString);
            ICustomerRepository customerRepository = repositoryFactory.GetCustomerRepository(connectionString);
            ISimulationRepository simulationRepo = repositoryFactory.GetSimulationRepository(connectionString);
            ICsvReader csvReader = readerFactory.GetCsvReader();
            IJsonReader jsonReader = readerFactory.GetJsonReader();
            ITxtReader txtReader = readerFactory.GetTxtReader();

            var addressManager = new AddressManager(addressRepository);
            _municipalityManager = new MunicipalityManager(municipalityRepository);
            var nameManager = new NameManager(nameRepository);
            var customerManager = new CustomerManager(customerRepository);
            _simulationDataManager = new SimulationDataManager(simulationRepo);

            _generateCustomerService = new GenerateCustomerService(addressManager, _municipalityManager, nameManager, customerManager, _simulationDataManager);

            _uploadService = new UploadService(addressRepository, municipalityRepository, nameRepository, _countryVersionRepository, csvReader, txtReader, jsonReader);
        }

        private void ButtonUpload_Click(object sender, RoutedEventArgs e)
        {
            UploadWindow uploadWindow = new UploadWindow(_uploadService, _countryVersionRepository);
            uploadWindow.Show();
        }

        private void ButtionSimulation_Click(object sender, RoutedEventArgs e)
        {
            SimulationWindow simulationWindow = new SimulationWindow(_countryVersionRepository, _generateCustomerService, _municipalityManager, _simulationDataManager);
            simulationWindow.Show();
        }
    }
}