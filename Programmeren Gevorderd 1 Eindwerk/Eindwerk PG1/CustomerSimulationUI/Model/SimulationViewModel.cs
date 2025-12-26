using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.DTOs;
using CustomerSimulationBL.Managers;
using CustomerSimulationBL.Mappers;

namespace CustomerSimulationUI.Model
{
    public class SimulationViewModel : INotifyPropertyChanged
    {
        private readonly GenerateCustomerService _generateCustomerService;
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
        public ObservableCollection<int> SelectedMunicipalityIds { get; } = new ObservableCollection<int>();

        public ObservableCollection<CustomerDTO> Customers { get; } = new ObservableCollection<CustomerDTO>();

        public void GenerateCustomers(int countryVersionId)
        {
            //Create DTO from UI
            var settingsDTO = new SimulationSettingsDTO(SelectedMunicipalityIds.ToList(), TotalCustomers, MinAge, MaxAge, MinNumber, MaxNumber, HasLetters, PercentageLetters);

            //Map DTO -> Domain
            var settings = SimulationSettingsMapper.ToDomain(settingsDTO);

            //Run customer simulation
            var generatedCustomers = _generateCustomerService.GenerateCustomers(settings, countryVersionId);

            //Update UI
            Customers.Clear();
            foreach (var customer in generatedCustomers)
            {
                Customers.Add(customer);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
