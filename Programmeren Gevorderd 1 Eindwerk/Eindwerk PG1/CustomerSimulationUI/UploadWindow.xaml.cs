using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Enumerations;
using CustomerSimulationBL.Interfaces;
using CustomerSimulationBL.Services;
using Microsoft.Win32;

namespace CustomerSimulationUI
{
    /// <summary>
    /// Interaction logic for UploadWindow.xaml
    /// </summary>
    public partial class UploadWindow : Window
    {
        private readonly IUploadService _uploadService;
        private readonly ICountryVersionRepository _countryVersionRepo;
        public UploadWindow(IUploadService uploadService, ICountryVersionRepository countryVersionRepo)
        {
            InitializeComponent();

            _uploadService = uploadService;
            _countryVersionRepo = countryVersionRepo;

            List<Country> countries = _countryVersionRepo.GetAllCountries();

            SelectCountry.ItemsSource = countries;
            SelectCountry.SelectedIndex = 0;
            SelectCountry.DisplayMemberPath = "Name";
            SelectCountry.SelectedValuePath = "ID";
        }

        private void ButtonSelectFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a file to upload";
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|JSON files (*.json)|*.json|TXT files (*.txt)|*.txt";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Multiselect = false;

            bool? result = openFileDialog.ShowDialog();

            if(result == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                FilePath.Text = selectedFilePath;
            }
        }

        private async void ButtonUploadFile_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(FilePath.Text))
            {
                MessageBox.Show("Please select a file for upload.");
                return;
            }
            if(!int.TryParse(FieldYear.Text, out int year))
            {
                MessageBox.Show("Invalid Year");
                return;
            }

            var selectedCountry = SelectCountry.SelectedItem as Country;

            if(selectedCountry == null)
            {
                MessageBox.Show("Please select a country.");
                    return;
            }

            string filePath = FilePath.Text;
            string countryName = selectedCountry.Name;

            int countryId = selectedCountry.Id;

            UploadDataType dataType = GetSelectedDataType();

            //Create and show progress window
            var progressWindow = new ProgressWindow();
            progressWindow.Owner = this;
            progressWindow.Show();

            var progress = new Progress<int>(value => { progressWindow.ReportProgress(value); });

            try
            {
                await Task.Run(() => _uploadService.Upload(filePath, year, dataType, countryId, progress, countryName));
            }
            finally
            {
                progressWindow.Close();
            }

            MessageBox.Show("Upload Completed");
        }

        private UploadDataType GetSelectedDataType()
        {
            var selected = (SelectDatatype.SelectedItem as ComboBoxItem)?.Content?.ToString();

            return selected switch
            {
                "Address" => UploadDataType.Address,
                "Municipality" => UploadDataType.Municipality,
                "First Name" => UploadDataType.FirstName,
                "Last Name" => UploadDataType.LastName,
                _ => throw new InvalidOperationException("Invalid Datatype")
            };
        }
    }
}
