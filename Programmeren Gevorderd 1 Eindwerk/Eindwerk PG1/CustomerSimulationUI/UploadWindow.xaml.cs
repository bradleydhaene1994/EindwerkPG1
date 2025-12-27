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
        private readonly IAddressRepository _addressRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICountryVersionRepository _countryVersionRepository;
        private readonly IMunicipalityRepository _municipalityRepository;
        private readonly INameRepository _nameRepository;
        private readonly ITxtReader _textReader;
        private readonly IJsonReader _jsonReader;
        private readonly ICsvReader _csvReader;
        private readonly IUploadService _uploadService;
        public UploadWindow(IAddressRepository adresRepo, ICustomerRepository customerRepo, ICountryVersionRepository cvRepo, IMunicipalityRepository muniRepo, INameRepository nameRepo, ITxtReader txtReader, IJsonReader jsonReader, ICsvReader csvReader)
        {
            InitializeComponent();
            _addressRepository = adresRepo;
            _customerRepository = customerRepo;
            _countryVersionRepository = cvRepo;
            _municipalityRepository = muniRepo;
            _nameRepository = nameRepo;
            _textReader = txtReader;
            _jsonReader = jsonReader;
            _csvReader = csvReader;

            _uploadService = new UploadService(_addressRepository, _municipalityRepository, _nameRepository, _countryVersionRepository, _csvReader, _textReader, _jsonReader);

            List<Country> countries = _countryVersionRepository.GetAllCountries();

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

            int countryId = selectedCountry.Id;

            CountryVersion countryVersion = new CountryVersion(year);

            UploadDataType dataType = GetSelectedDataType();

            UploadProgressBar.Value = 0;
            UploadProgressBar.Visibility = Visibility.Visible;

            var progress = new Progress<int>(value =>
            {
                UploadProgressBar.Value = value;
            });

            await Task.Run(() => _uploadService.Upload(filePath, countryVersion, dataType, countryId, progress));

            UploadProgressBar.Visibility = Visibility.Collapsed;

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
