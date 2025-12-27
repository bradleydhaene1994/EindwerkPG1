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

namespace CustomerSimulationUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICountryVersionRepository _countryVersionRepository;
        private readonly IMunicipalityRepository _municipalityRepository;
        private readonly INameRepository _nameRepository;
        private readonly ISimulationRepository _simulationRepository;
        private readonly ITxtReader _textReader;
        private readonly IJsonReader _jsonReader;
        private readonly ICsvReader _csvReader;
        public MainWindow()
        {
            RepositoryFactory repoFactory = new RepositoryFactory();
            FileReaderFactory fileReaderFactory = new FileReaderFactory();
            var builder = new ConfigurationBuilder().
                              SetBasePath(AppContext.BaseDirectory).
                              AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            string connectionString = configuration.GetSection("ConnectionStrings")["SQLserver"];
            
            _addressRepository = repoFactory.GetAddressRepository(connectionString);
            _customerRepository = repoFactory.GetCustomerRepository(connectionString);
            _countryVersionRepository = repoFactory.GetCountryVersionRepository(connectionString);
            _municipalityRepository = repoFactory.GetMunicipalityRepository(connectionString);
            _nameRepository = repoFactory.GetNameRepository(connectionString);
            _simulationRepository = repoFactory.GetSimulationRepository(connectionString);
            _textReader = fileReaderFactory.GetTxtReader();
            _jsonReader = fileReaderFactory.GetJsonReader();
            _csvReader = fileReaderFactory.GetCsvReader();
        }

        private void ButtonUpload_Click(object sender, RoutedEventArgs e)
        {
            UploadWindow uploadWindow = new UploadWindow(_addressRepository, _customerRepository, _countryVersionRepository, _municipalityRepository, _nameRepository, _textReader, _jsonReader, _csvReader);
            uploadWindow.Show();
        }

        private void ButtionSimulation_Click(object sender, RoutedEventArgs e)
        {
            //SimulationWindow simulationWindow = new SimulationWindow(_addressRepository, _customerRepository, _countryVersionRepository, _municipalityRepository, _nameRepository, _simulationRepository);
            //simulationWindow.Show();
        }
    }
}