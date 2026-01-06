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
        //uploads generated customers to the db
        public void UploadCustomer(IEnumerable<CustomerDTO> customers, int simulationDataId, int countryVersionId)
        {
            _customerRepo.UploadCustomer(customers, simulationDataId, countryVersionId);
        }
        //gets customer from the db by using a simulationdataid;
        public List<Customer> GetCustomerBySimulationDataID(int simulationDataID)
        {
            var customers = _customerRepo.GetCustomerBySimulationDataID(simulationDataID);
            return customers;
        }
        //generates a randombirthdate according to the specifications in the settings
        public DateTime GetRandomBirthdate(SimulationSettings settings)
        {
            //get current date
            DateTime today = DateTime.Now;

            //get what the earliest and latest birthdate should be
            DateTime earliestBirthDate = today.AddYears(-settings.MaxAge);
            DateTime latestBirthDate = today.AddYears(-settings.MinAge);

            //calculate total number of days inbetween dates
            int dayRange = (latestBirthDate - earliestBirthDate).Days;

            //get a random number of days according to the daterange
            int randomDays = _random.Next(dayRange + 1);

            //add the random number of days to the earliestbirthdate in order to get a valid random birthdate
            return earliestBirthDate.AddDays(randomDays);
        }
        //generate a random housenumber according to the specifications in settings
        public string GetRandomHouseNumber(SimulationSettings settings)
        {
            //get random number according to settings
            int number = _random.Next(settings.MinNumber , settings.MaxNumber + 1);

            //checks if a letter should be added according to the settings
            bool addLetter = settings.HasLetters && _random.Next(100) < settings.PercentageLetters;

            //if addletter is false, return only number
            if(!addLetter)
            {
                return number.ToString();
            }

            //if a letter has to be added, pick a random letter using ASCII arithmetics
            char letter = (char)('A' + _random.Next(26));

            return $"{number}{letter}";
        }
    }
}
