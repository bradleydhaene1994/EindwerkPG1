using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Enumeration;
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
using CustomerSimulationUI.Model;
using Microsoft.Identity.Client;
using Microsoft.Win32;

namespace CustomerSimulationUI
{
    /// <summary>
    /// Interaction logic for SimulationWindow.xaml
    /// </summary>
    public partial class SimulationWindow : Window
    {
        private readonly ICountryVersionRepository _countryVersionRepository;
        private readonly GenerateCustomerService _generateCustomerService;
        private readonly MunicipalityManager _municipalityManager;
        private readonly SimulationDataManager _simulationDataManager;
        private readonly CustomerManager _customerManager;
        private readonly SimulationViewModel _simulationViewModel;
        public ObservableCollection<SimulationOverviewDTO> Simulations { get; set; }
        public SimulationWindow(ICountryVersionRepository countryVersionRepo, GenerateCustomerService genCustomer, MunicipalityManager municipalityManager, SimulationDataManager simulationDataManager)
        {
            InitializeComponent();

            _countryVersionRepository = countryVersionRepo;
            _municipalityManager = municipalityManager;
            _simulationDataManager = simulationDataManager;

            _simulationViewModel = new SimulationViewModel(genCustomer);
            DataContext = _simulationViewModel;

            SelectedCountryVersion.ItemsSource = _countryVersionRepository.GetAllCountryVersions();
            Simulations = new ObservableCollection<SimulationOverviewDTO>(_simulationDataManager.GetSimulationOverview());
        }

        private void ButtonSimulation_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(ClientName.Text))
            {
                MessageBox.Show("Please fill in your name.");
                return;
            }
            if(SelectedCountryVersion.SelectedValue == null)
            {
                MessageBox.Show("Please select a country version.");
                return;
            }

            int countryVersionId = (int)SelectedCountryVersion.SelectedValue;
            string clientName = ClientName.Text;
            List<Municipality> allowedMunicipalities = _simulationViewModel.SelectedMunicipalities.Any() ? _simulationViewModel.SelectedMunicipalities.Select(m => m.Municipality).ToList()
                                                                                                         : _municipalityManager.GetMunicipalityByCountryVersionID(countryVersionId);

            _simulationViewModel.RunSimulation(countryVersionId, clientName, allowedMunicipalities);

            MessageBox.Show("Simulation created successfully");

            Simulations.Clear();

            foreach(var sim in _simulationDataManager.GetSimulationOverview())
            {
                Simulations.Add(sim);
            }
        }
        private void SelectMunicipalities_Click(object sender, EventArgs e)
        {   
            if(_simulationViewModel == null || SelectedCountryVersion.SelectedValue == null)
            {
                MessageBox.Show("Please select a country version first.");
            }

            int countryVersionId = (int)SelectedCountryVersion.SelectedValue;

            //load municipalities from BL
            List<Municipality> allMunicipalities = _municipalityManager.GetMunicipalityByCountryVersionID(countryVersionId);

            //Open dialog with current selection from viewModel
            var dialog = new SelectMunicipalities(allMunicipalities, _simulationViewModel.SelectedMunicipalities.ToList());

            if(dialog.ShowDialog() == true && dialog.Result != null)
            {
                //update ViewModel state
                _simulationViewModel.SelectedMunicipalities.Clear();

                foreach(var selection in dialog.Result)
                {
                    _simulationViewModel.SelectedMunicipalities.Add(selection);
                }
            }
        }
        private void SelectedCountryVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(_simulationViewModel == null || SelectedCountryVersion.SelectedValue == null)
            {
                return;
            }

            int countryVersionId = (int)SelectedCountryVersion.SelectedValue;

            //load municipalities for UI purposes
            List<Municipality> municipalities = _municipalityManager.GetMunicipalityByCountryVersionID(countryVersionId);

            //clear previous selections in ViewModel
            _simulationViewModel.SelectedMunicipalities.Clear();
        }
        private void ViewSettings_Click(object sender, RoutedEventArgs e)
        {
            
            if(DataGridSimulatedCustomersets.SelectedItem is not SimulationOverviewDTO selected)
            {
                MessageBox.Show("Please select a simulation");
                return;
            }

            int simulationDataId = selected.SimulationDataId;
            int countryVersionId = selected.CountryVersionId;

            MessageBox.Show($"{simulationDataId}, {countryVersionId}");

            SimulationSettings settings = _simulationDataManager.GetSimulationSettingsBySimulationDataID(simulationDataId);

            var window = new SimulationSettingsWindow(countryVersionId, settings, _simulationDataManager, _municipalityManager);
            window.Show();
        }
        private void ViewStatistics_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridSimulatedCustomersets.SelectedItem is not SimulationOverviewDTO selected)
            {
                MessageBox.Show("Please select a simulation");
                return;
            }

            SimulationStatisticsResult simStatResults = _generateCustomerService.BuildStatisticsResult(selected.SimulationDataId, selected.CountryVersionId);

            var window = new SimulationStatisticsWindow(simStatResults);

            window.Show();
        }

        private void ButtonCustomerDataExportTxt_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridSimulatedCustomersets.SelectedItem is not SimulationOverviewDTO selected)
            {
                MessageBox.Show("Please select a simulation");
                return;
            }

            string title = "Export Customer Data";
            string filter = "Text file (*.txt)|*.txt";
            string fileName = $"CustomerData_Simulation_{selected.SimulationDataId}";
            string defaultExt = ".txt";

            SaveFileDialog dialog = new SaveFileDialog
            {
                Title = title,
                Filter = filter,
                FileName = fileName,
                DefaultExt = defaultExt
            };

            if(dialog.ShowDialog() != true)
            {
                return;
            }

            SimulationData simulationData = _simulationDataManager.GetSimulationDataById(selected.SimulationDataId);
            List<CustomerDTO> customers = _generateCustomerService.GetCustomerDTOBySimulationDataId(selected.SimulationDataId);
            CountryVersion countryVersion = _countryVersionRepository.GetCountryVersionById(selected.CountryVersionId);

            _generateCustomerService.ExportCustomerDataToTxt(simulationData, customers, dialog.FileName, countryVersion);

            MessageBox.Show("Customer Data exported successfully.");
        }

        private void ButtonSimulationDataExportTxt_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridSimulatedCustomersets.SelectedItem is not SimulationOverviewDTO selected)
            {
                MessageBox.Show("Please select a simulation");
                return;
            }

            string title = "Export Simulation Data";
            string filter = "Text file (*.txt)|*.txt";
            string fileName = $"SimulationData_Simulation_{selected.SimulationDataId}";
            string defaultExt = ".txt";

            SaveFileDialog dialog = new SaveFileDialog
            {
                Title = title,
                Filter = filter,
                FileName = fileName,
                DefaultExt = defaultExt
            };

            if (dialog.ShowDialog() != true)
            {
                return;
            }

            SimulationExport export = _generateCustomerService.BuildSimulationExport(selected.SimulationDataId, selected.CountryVersionId);

            CountryVersion countryVersion = _countryVersionRepository.GetCountryVersionById(selected.CountryVersionId);

            _generateCustomerService.ExportStatisticsToTxt(export, dialog.FileName, countryVersion);

            MessageBox.Show("Simulation Data export successfull");
        }
    }
}
