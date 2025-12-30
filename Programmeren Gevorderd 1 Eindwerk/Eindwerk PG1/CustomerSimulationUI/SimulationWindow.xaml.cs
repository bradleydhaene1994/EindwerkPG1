using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using CustomerSimulationBL.DTOs;
using CustomerSimulationBL.Interfaces;
using CustomerSimulationBL.Managers;
using Microsoft.Identity.Client;

namespace CustomerSimulationUI
{
    /// <summary>
    /// Interaction logic for SimulationWindow.xaml
    /// </summary>
    public partial class SimulationWindow : Window, INotifyPropertyChanged
    {
        private readonly ICountryVersionRepository _countryVersionRepository;
        private readonly GenerateCustomerService _generateCustomerService;
        private readonly MunicipalityManager _municipalityManager;
        private readonly SimulationDataManager _simulationDataManager;
        private List<Municipality> _allMunicipalities;
        public List<Municipality> AllMunicipalities
        {
            get => _allMunicipalities;
            set
            {
                _allMunicipalities = value;
                OnPropertyChanged(nameof(AllMunicipalities));
            }
        }
        private ObservableCollection<MunicipalitySelection> _selectedMunicipalities;
        public ObservableCollection<MunicipalitySelection> SelectedMunicipalities
        {
            get => _selectedMunicipalities;
            set
            {
                _selectedMunicipalities = value;
                OnPropertyChanged(nameof(SelectedMunicipalities));
            }
        }
        public string SelectedMunicipalitiesSummary
        {
            get
            {
                if(SelectedMunicipalities == null || SelectedMunicipalities.Count == 0)
                {
                    return "All Municipalities";
                }

                return string.Join(", ", SelectedMunicipalities.Select(m => $"{m.Municipality.Name}{m.Percentage}"));
            }
        }
        public ObservableCollection<SimulationOverviewDTO> Simulations { get; set; }
        public SimulationWindow(ICountryVersionRepository countryVersionRepo, GenerateCustomerService genCustomer, MunicipalityManager municipalityManager, SimulationDataManager simulationDataManager)
        {
            InitializeComponent();

            _countryVersionRepository = countryVersionRepo;
            _generateCustomerService = genCustomer;
            _municipalityManager = municipalityManager;
            _simulationDataManager = simulationDataManager;

            List<CountryVersion> countryVersions = _countryVersionRepository.GetAllCountryVersions();
            AllMunicipalities = new List<Municipality>();

            SelectedCountryVersion.ItemsSource = countryVersions;

            SelectedCountryVersion.SelectionChanged += SelectedCountryVersion_SelectionChanged;
            SelectedMunicipalities = new ObservableCollection<MunicipalitySelection>();

            Simulations = new ObservableCollection<SimulationOverviewDTO>(_simulationDataManager.GetSimulationOverview());

            DataContext = this;
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
            if (SelectedCountryVersion.SelectedValue == null)
            {
                MessageBox.Show("Please select a country version.");
                return;
            }

            int minAge = int.Parse(MinimumAge.Text);
            int maxAge = int.Parse(MaximumAge.Text);
            int minHouseNumber = int.Parse(MinHouseNumber.Text);
            int maxHouseNumber = int.Parse(MaxHouseNumber.Text);
            bool hasLetters = HasLetters.IsChecked == true;
            int percentageLetters = int.Parse(PercentageLetters.Text);
            var selectedCountryVersion = SelectedCountryVersion.SelectedItem as CountryVersion;
            var countryVersionId = (int)SelectedCountryVersion.SelectedValue;

            List<MunicipalitySelection>? municipalitySelections = null;
            List<Municipality>? allowedMunicipalities = null;

            if((UseSpecificMunicipalitiesCheckBox.IsChecked == true) && SelectedMunicipalities.Any())
            {
                municipalitySelections = SelectedMunicipalities.ToList();
                allowedMunicipalities = SelectedMunicipalities.Select(s => s.Municipality).ToList();
            }

            var simulationData = new SimulationData(ClientName.Text, DateTime.Now);
            var simulationSettings = new SimulationSettings(municipalitySelections, totalCustomers, minAge, maxAge, minHouseNumber, maxHouseNumber, hasLetters, percentageLetters);

            _generateCustomerService.RunSimulation(simulationData, simulationSettings, countryVersionId, allowedMunicipalities);

            MessageBox.Show("Simulation created succesfully.");

            Simulations.Clear();

            foreach(var sim in _simulationDataManager.GetSimulationOverview())
            {
                Simulations.Add(sim);
            }
        }
        private void SelectMunicipalities_Click(object sender, EventArgs e)
        {   
            if(AllMunicipalities == null || AllMunicipalities.Count == 0)
            {
                MessageBox.Show("Please select a country version first.");
            }
            
            var dialog = new SelectMunicipalities(AllMunicipalities, SelectedMunicipalities.ToList());

            if(dialog.ShowDialog() == true)
            {
                SelectedMunicipalities.Clear();

                if(dialog.Result != null)
                {
                    foreach(var selection in dialog.Result)
                    {
                        SelectedMunicipalities.Add(selection);
                    }
                }
            }
        }
        private void SelectedCountryVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedCountryVersion.SelectedValue == null)
            {
                return;
            }

            int countryVersionId = (int)SelectedCountryVersion.SelectedValue;

            AllMunicipalities = _municipalityManager.GetMunicipalityByCountryVersionID(countryVersionId);

            //clear previous municipality selections
            SelectedMunicipalities.Clear();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void ViewSettings_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridSimulatedCustomersets.SelectedItem is not SimulationOverviewDTO selected)
            {
                MessageBox.Show("Please select a simulation");
                return;
            }

            int simulationDataId = selected.SimulationDataId;

            SimulationSettings settings = _simulationDataManager.GetSimulationSettingsBySimulationDataID(simulationDataId);

            var window = new SimulationSettingsWindow(settings, _simulationDataManager, AllMunicipalities);
            window.Show();
        }
    }
}
