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
using CustomerSimulationBL.Managers;
using Microsoft.Extensions.Primitives;

namespace CustomerSimulationUI
{
    /// <summary>
    /// Interaction logic for SimulationInformation.xaml
    /// </summary>
    public partial class SimulationSettingsWindow : Window
    {
        public SimulationSettings Settings { get; }

        private readonly SimulationDataManager _simulationDataManager;
        public string SelectedMunicipalitiesSummary => Settings.SelectedMunicipalities == null ? "All municipalities" : string.Join(", ", Settings.SelectedMunicipalities.Select(m => $"{m.Municipality.Name} {m.Percentage}"));
        public SimulationSettingsWindow(SimulationSettings settings, SimulationDataManager dataManager, List<Municipality> municipalities)
        {
            InitializeComponent();

            _simulationDataManager = dataManager;

            var selections = _simulationDataManager.GetSelectedMunicipalities(settings.Id, municipalities);

            settings.SelectedMunicipalities = selections.Count == 0 ? null : selections;

            Settings = settings;

            DataContext = this;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
