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

namespace CustomerSimulationUI
{
    /// <summary>
    /// Interaction logic for SimulationStatisticsWindow.xaml
    /// </summary>
    public partial class SimulationStatisticsWindow : Window
    {
        public SimulationStatisticsWindow(SimulationStatisticsResult result)
        {
            InitializeComponent();
            DataContext = result;
        }
    }
}
