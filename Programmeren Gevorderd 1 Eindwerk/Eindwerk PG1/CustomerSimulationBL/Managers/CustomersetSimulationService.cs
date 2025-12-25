using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;

namespace CustomerSimulationBL.Managers
{
    public class CustomersetSimulationService
    {
        private readonly AddressManager _addressmanager;
        private readonly MunicipalityManager _municipalitymanager;
        private readonly NameManager _namemanager;
        private readonly CustomerManager _customermanager;

        private readonly Random _random = new Random();

        public List<Customer> CustomerSetSimulation(SimulationSettings simulationSettings, int countryVersionId, int minNumber, int maxNumber, bool hasLetters, int percentageLetters)
        {
            int customerAmount = simulationSettings.NumberCustomers;
            
            List<Customer> customers = new List<Customer>();

            for(int i = 0; i < customerAmount; i++)
            {
                var addresses = _addressmanager.GetAddressesByCountryVersionID(countryVersionId);
                Address randomAddres = _addressmanager.GetRandomAddress(addresses);
                string addressStreet = randomAddres.Street;

                var municipalities = _municipalitymanager.GetMunicipalityByCountryVersionID(countryVersionId);
                Municipality randomMunicipality = _municipalitymanager.GetRandomMunicipality(municipalities);
                string municipalityName = randomMunicipality.Name;

                var firstNames = _namemanager.GetFirstNamesByCountryVersionID(countryVersionId);
                FirstName randomFirstName = _namemanager.GetRandomFirstName(firstNames);
                string nameFirst = randomFirstName.Name;

                var lastNames = _namemanager.GetLastNamesByCountryVersionID(countryVersionId);
                LastName randomLastName = _namemanager.GetRandomLastName(lastNames);
                string nameLast = randomLastName.Name;

                DateTime randomBirthday = _customermanager.GetRandomBirthdate(simulationSettings.MinAge, simulationSettings.MaxAge);
                string houseNumber = _customermanager.GetRandomHouseNumber(minNumber, maxNumber, hasLetters, percentageLetters);

                Customer customer = new Customer(nameFirst, nameLast, municipalityName, addressStreet, randomBirthday, houseNumber);

                customers.Add(customer);
            }
            return customers;
        }
    }
}
