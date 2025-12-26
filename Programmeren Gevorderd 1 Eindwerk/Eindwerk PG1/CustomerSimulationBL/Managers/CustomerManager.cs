using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.DTOs;
using CustomerSimulationBL.Interfaces;

namespace CustomerSimulationBL.Managers
{
    public class CustomerManager
    {
        private ICustomerRepository _customerRepo;
        private static readonly Random _random = new Random();

        public CustomerManager(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public void UploadCustomer(IEnumerable<Customer> customers, int simulationDataId)
        {
            _customerRepo.UploadCustomer(customers, simulationDataId);
        }
        public List<Customer> GetCustomerBySimulationDataID(int simulationDataID)
        {
            var customers = _customerRepo.GetCustomerBySimulationDataID(simulationDataID);
            return customers;
        }

        public DateTime GetRandomBirthdate(SimulationSettings settings)
        {
            DateTime today = DateTime.Now;

            DateTime earliestBirthDate = today.AddYears(-settings.MaxAge);
            DateTime latestBirthDate = today.AddYears(-settings.MinAge);

            int dayRange = (latestBirthDate - earliestBirthDate).Days;

            int randomDays = _random.Next(dayRange + 1);

            return earliestBirthDate.AddDays(randomDays);
        }

        public string GetRandomHouseNumber(SimulationSettings settings)
        {
            int number = _random.Next(settings.MinAge , settings.MaxAge + 1);

            bool addLeter = settings.HasLetters && _random.Next(100) < settings.PercentageLetters;

            if(!addLeter)
            {
                return number.ToString();
            }

            char letter = (char)('A' + _random.Next(26));

            return $"{number}{letter}";
        }
    }
}
