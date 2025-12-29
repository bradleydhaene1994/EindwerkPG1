using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for SelectMunicipalities.xaml
    /// </summary>
    public partial class SelectMunicipalities : Window
    {
        public List<MunicipalitySelection> Result { get; private set; }
        public ObservableCollection<MunicipalitySelection> MunicipalitySelections { get; }
        public List<Municipality> AllMunicipalities { get; }
        public SelectMunicipalities(List<Municipality> allMunicipalities, List<MunicipalitySelection>? existingSelections)
        {
            InitializeComponent();

            MunicipalitySelections = new ObservableCollection<MunicipalitySelection>();

            AllMunicipalities = allMunicipalities;

            foreach(var municipality in AllMunicipalities)
            {
                var existing = existingSelections?.FirstOrDefault(s => s.Municipality.Id == municipality.Id);

                bool isSelected = existing != null;
                int percentage = existing?.Percentage ?? 0;

                MunicipalitySelection municipalitySelection = new MunicipalitySelection(municipality, percentage, isSelected);

                MunicipalitySelections.Add(municipalitySelection);
            }

            DataContext = this;
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            var selected = MunicipalitySelections.Where(m => m.IsSelected).ToList();

            if(selected.Count == 0)
            {
                Result = null;
                DialogResult = true;
                return;
            }

            int total = selected.Sum(m => m.Percentage);

            if(total != 100)
            {
                MessageBox.Show("Total percentage does not equal 100");
                return;
            }

            Result = selected;
            DialogResult = true;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
