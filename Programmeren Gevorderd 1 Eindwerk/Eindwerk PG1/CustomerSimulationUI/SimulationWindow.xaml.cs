using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Interfaces;
using CustomerSimulationBL.Managers;
using Microsoft.Identity.Client;

namespace CustomerSimulationUI
{
    /// <summary>
    /// Interaction logic for SimulationWindow.xaml
    /// </summary>
    public partial class SimulationWindow : Window
    {
        private readonly ICountryVersionRepository _countryVersionRepository;
        private readonly GenerateCustomerService _generateCustomerService;
        public SimulationWindow(ICountryVersionRepository countryVersionRepo, GenerateCustomerService genCustomer)
        {
            InitializeComponent();

            _countryVersionRepository = countryVersionRepo;
            _generateCustomerService = genCustomer;

            List<CountryVersion> countryVersions = _countryVersionRepository.GetAllCountryVersions();

            SelectedCountryVersion.ItemsSource = countryVersions;
            SelectedCountryVersion.SelectedIndex = 0;
        }

        private void ButtonSimulation_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(ClientName.Text))
            {
                MessageBox.Show("Please fill in your name");
                return;
            }
            if(!int.TryParse(CustomerNumber.Text, out int totalCustomers))
            {
                MessageBox.Show("Please fill in how many customers you would like to simulate");
            }

            int minAge = int.Parse(MinimumAge.Text);
            int maxAge = int.Parse(MaximumAge.Text);
            int minHouseNumber = int.Parse(MinHouseNumber.Text);
            int maxHouseNumber = int.Parse(MaxHouseNumber.Text);
            bool hasLetters = HasLetters.IsChecked == true;
            int percentageLetters = int.Parse(PercentageLetters.Text);
            var selectedCountryVersion = SelectedCountryVersion.SelectedItem as CountryVersion;
            var countryVersionId = selectedCountryVersion.Id;

            var simulationData = new SimulationData(ClientName.Text, DateTime.Now);
            var simulationSettings = new SimulationSettings(null, totalCustomers, minAge, maxAge, minHouseNumber, maxHouseNumber, hasLetters, percentageLetters);

            _generateCustomerService.RunSimulation(simulationData, simulationSettings, countryVersionId);

            MessageBox.Show("Simulation created succesfully.");
        }
    }
}
