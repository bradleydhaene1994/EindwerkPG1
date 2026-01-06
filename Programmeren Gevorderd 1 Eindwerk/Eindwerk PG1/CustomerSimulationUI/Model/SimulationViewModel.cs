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
        private readonly GenerateCustomerService _generateCustomerService;
        public ObservableCollection<SimulationOverviewDTO> Simulations { get; set; }
        public SimulationViewModel(GenerateCustomerService generateCustomerService)
        {
            _generateCustomerService = generateCustomerService;
        }
        public int TotalCustomers { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int MinNumber { get; set; }
        public int MaxNumber { get; set; }
        public bool HasLetters { get; set; }
        public int PercentageLetters { get; set; }
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
