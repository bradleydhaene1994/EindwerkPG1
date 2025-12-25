using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
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

        public DateTime GetRandomBirthdate(int minAge, int maxAge)
        {
            if(minAge < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minAge));
            }
            if(maxAge < minAge)
            {
                throw new ArgumentException("Maximum age must be higher than minimum age.");
            }

            DateTime today = DateTime.Now;

            DateTime earliestBirthDate = today.AddYears(-maxAge);
            DateTime latestBirthDate = today.AddYears(-minAge);

            int dayRange = (latestBirthDate - earliestBirthDate).Days;

            int randomDays = _random.Next(dayRange + 1);

            return earliestBirthDate.AddDays(randomDays);
        }

        public string GetRandomHouseNumber(int minNumber, int maxNumber, bool hasLetters, int percentageLetters)
        {
            if(minNumber < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minNumber));
            }
            if(maxNumber < minNumber)
            {
                throw new ArgumentException("maximum number must be higher than minimum number");
            }
            if(percentageLetters < 0 || percentageLetters > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(percentageLetters));
            }

            int number = _random.Next(minNumber , maxNumber + 1);

            bool addLeter = hasLetters && _random.Next(100) < percentageLetters;

            if(!addLeter)
            {
                return number.ToString();
            }

            char letter = (char)('A' + _random.Next(26));

            return $"{number}{letter}";
        }
    }
}
