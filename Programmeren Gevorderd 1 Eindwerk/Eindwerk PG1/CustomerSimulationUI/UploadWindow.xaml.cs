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

            List<Country> countries = _countryVersionRepository.GetAllCountries();

            SelectCountry.ItemsSource = countries;
            SelectCountry.SelectedIndex = 0;
            SelectCountry.DisplayMemberPath = "Name";
        }

        private void ButtonSelectFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonUploadFile_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
