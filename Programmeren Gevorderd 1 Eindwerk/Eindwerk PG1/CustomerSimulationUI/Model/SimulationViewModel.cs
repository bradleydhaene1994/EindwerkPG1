using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.DTOs;
using CustomerSimulationBL.Exceptions;
using CustomerSimulationBL.Managers;
using CustomerSimulationBL.Mappers;

namespace CustomerSimulationUI.Model
{
    public class SimulationViewModel : INotifyPropertyChanged
    {
        private readonly SimulationService _generateCustomerService;
        public ObservableCollection<SimulationOverviewDTO> Simulations { get; set; }

        private int _totalCustomers;
        private int _minAge;
        private int _maxAge;
        private int _minNumber;
        private int _maxNumber;
        private bool _hasLetters;
        private int _percentageLetters;
        public SimulationViewModel(SimulationService generateCustomerService)
        {
            _generateCustomerService = generateCustomerService;
        }
        public int TotalCustomers
        {
            get => _totalCustomers;
            set
            {
                if(_totalCustomers != value)
                {
                    _totalCustomers = value;
                    OnPropertyChanged(nameof(TotalCustomers));
                }
            }
        }
        public int MinAge
        {
            get => _minAge;
            set
            {
                if (_minAge != value)
                {
                    _minAge = value;
                    OnPropertyChanged(nameof(MinAge));
                }
            }
        }
        public int MaxAge
        {
            get => _maxAge;
            set
            {
                if (_maxAge != value)
                {
                    _maxAge = value;
                    OnPropertyChanged(nameof(MaxAge));
                }
            }
        }
        public int MinNumber
        {
            get => _minNumber;
            set
            {
                if (_minNumber != value)
                {
                    _minNumber = value;
                    OnPropertyChanged(nameof(MinNumber));
                }
            }
        }
        public int MaxNumber
        {
            get => _maxNumber;
            set
            {
                if (_maxNumber != value)
                {
                    _maxNumber = value;
                    OnPropertyChanged(nameof(MaxNumber));
                }
            }
        }

        public bool HasLetters
        {
            get => _hasLetters;
            set
            {
                if (_hasLetters != value)
                {
                    _hasLetters = value;
                    OnPropertyChanged(nameof(HasLetters));
                }
            }
        }
        public int PercentageLetters
        {
            get => _percentageLetters;
            set
            {
                if (_percentageLetters != value)
                {
                    _percentageLetters = value;
                    OnPropertyChanged(nameof(PercentageLetters));
                }
            }
        }
        public ObservableCollection<MunicipalitySelection> SelectedMunicipalities { get; } = new ObservableCollection<MunicipalitySelection>();

        public ObservableCollection<CustomerDTO> Customers { get; } = new ObservableCollection<CustomerDTO>();

        public void RunSimulation(int countryVersionId, string clientName, List<Municipality> municipalities)
        {
            //Create SimulationData
            var simulationData = new SimulationData(clientName, DateTime.Now);

            //Create SimulationSettingsDTO
            var settingsDTO = new SimulationSettingsDTO(SelectedMunicipalities.ToList(), TotalCustomers, MinAge, MaxAge, MinNumber, MaxNumber, HasLetters, PercentageLetters);

            if (TotalCustomers <= 0)
                throw new SimulationException("Total customers must be greater than 0");

            if (MinAge < 0 || MaxAge < MinAge)
                throw new SimulationException("Invalid age range");

            if (MinNumber <= 0 || MaxNumber < MinNumber)
                throw new SimulationException("Invalid house number range");

            if (PercentageLetters < 0 || PercentageLetters > 100)
                throw new SimulationException("Percentage letters must be between 0 and 100");

            //Map DTO => Domain
            var settings = SimulationSettingsMapper.ToDomain(settingsDTO);

            _generateCustomerService.RunSimulation(simulationData, settings, countryVersionId, municipalities);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
