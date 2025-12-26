using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.DTOs;

namespace CustomerSimulationBL.Managers
{
    public class CustomersetSimulationService
    {
        private readonly AddressManager _addressmanager;
        private readonly MunicipalityManager _municipalitymanager;
        private readonly NameManager _namemanager;
        private readonly CustomerManager _customermanager;

        private readonly Random _random = new Random();

        public List<CustomerDTO> GenerateCustomers(SimulationSettings settings, int countryVersionId)
        {   
            List<CustomerDTO> customers = new List<CustomerDTO>();

            var addresses = _addressmanager.GetAddressesByCountryVersionID(countryVersionId);
            var municipalities = _municipalitymanager.GetMunicipalityByCountryVersionID(countryVersionId);
            var firstNames = _namemanager.GetFirstNamesByCountryVersionID(countryVersionId);
            var lastNames = _namemanager.GetLastNamesByCountryVersionID(countryVersionId);

            for (int i = 0; i < settings.TotalCustomers; i++)
            {
                Address randomAddres = _addressmanager.GetRandomAddress(addresses);
                string addressStreet = randomAddres.Street;

                Municipality randomMunicipality = _municipalitymanager.GetRandomMunicipality(municipalities);
                string municipalityName = randomMunicipality.Name;
                FirstName randomFirstName = _namemanager.GetRandomFirstName(firstNames);
                string nameFirst = randomFirstName.Name;

                LastName randomLastName = _namemanager.GetRandomLastName(lastNames);
                string nameLast = randomLastName.Name;

                DateTime randomBirthday = _customermanager.GetRandomBirthdate(settings);
                string houseNumber = _customermanager.GetRandomHouseNumber(settings);

                CustomerDTO customer = new CustomerDTO(nameFirst, nameLast, municipalityName, addressStreet, randomBirthday, houseNumber);

                customers.Add(customer);
            }
            return customers;
        }
    }
}
