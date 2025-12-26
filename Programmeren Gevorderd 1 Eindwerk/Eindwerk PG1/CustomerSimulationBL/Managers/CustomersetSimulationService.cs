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

        public List<CustomerDTO> CustomerSetSimulation(SimulationSettings settings, int countryVersionId)
        {   
            List<CustomerDTO> customers = new List<CustomerDTO>();

            for(int i = 0; i < settings.TotalCustomers; i++)
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

                DateTime randomBirthday = _customermanager.GetRandomBirthdate(settings);
                string houseNumber = _customermanager.GetRandomHouseNumber(settings);

                CustomerDTO customer = new CustomerDTO(nameFirst, nameLast, municipalityName, addressStreet, randomBirthday, houseNumber);

                customers.Add(customer);
            }
            return customers;
        }
    }
}
