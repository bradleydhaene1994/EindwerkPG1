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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonUpload_Click(object sender, RoutedEventArgs e)
        {
            var services = App.ServiceProvider;

            UploadWindow uploadWindow = new UploadWindow(services.UploadService, services.CountryVersionRepository);
            uploadWindow.Show();
        }

        private void ButtionSimulation_Click(object sender, RoutedEventArgs e)
        {
            var services = App.ServiceProvider;

            SimulationWindow simulationWindow = new SimulationWindow(services.CountryVersionRepository, services.SimulationService, services.MunicipalityManager, services.SimulationDataManager, services.SimulationStatisticsService, services.SimulationExportService);
            simulationWindow.Show();
        }
    }
}