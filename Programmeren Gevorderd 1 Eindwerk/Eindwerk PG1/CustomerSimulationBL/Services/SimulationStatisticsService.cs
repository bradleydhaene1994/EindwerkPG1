using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.DTOs;
using CustomerSimulationBL.Enumerations;
using CustomerSimulationBL.Managers;

namespace CustomerSimulationBL.Services
{
    public class SimulationStatisticsService
    {
        private readonly SimulationDataManager _simulationDataManager;
        private readonly CustomerManager _customerManager;
        private readonly MunicipalityManager _municipalityManager;
        private readonly AddressManager _addressManager;
        private readonly NameManager _nameManager;

        public SimulationStatisticsService(SimulationDataManager simulationDataManager, CustomerManager customerManager, MunicipalityManager municipalityManager, AddressManager addressManager, NameManager nameManager)
        {
            _simulationDataManager = simulationDataManager;
            _customerManager = customerManager;
            _municipalityManager = municipalityManager;
            _addressManager = addressManager;
            _nameManager = nameManager;
        }

        public SimulationStatistics CalculateStatistics(List<CustomerDTO> customers, DateTime simulationDate)
        {
            var agesAtSimulation = customers.Select(c => CalculateAge(c.BirthDate, simulationDate)).ToList();

            var agesToday = customers.Select(c => CalculateAge(c.BirthDate, DateTime.Today)).ToList();

            return new SimulationStatistics(customers.Count, agesAtSimulation.Average(), agesToday.Average(), agesToday.Min(), agesToday.Max());
        }

        private int CalculateAge(DateTime birthDate, DateTime referenceDate)
        {
            int age = referenceDate.Year - birthDate.Year;
            if (referenceDate < birthDate.AddYears(age))
            {
                age--;
            } 
            return age;
        }

        public List<MunicipalityStatistics> CalculateCustomersPerMunicipality(List<CustomerDTO> customers, List<Municipality> municipalities)
        {
            return customers
                .GroupBy(c => c.Municipality)
                .Select(g =>
                {
                    Municipality municipality =
                        municipalities.First(m => m.Name == g.Key);
                    return new MunicipalityStatistics(municipality, g.Count());
                })
                .ToList();
        }

        public List<MunicipalityStatistics> CalculateStreetsPerMunicipality(List<Address> addresses, List<Municipality> municipalities)
        {
            var result = new List<MunicipalityStatistics>();

            foreach(var municipality in municipalities)
            {
                int streetCount = addresses.Where(a => a.Municipality?.Name == municipality.Name)
                                           .Select(a => a.Street)
                                           .Distinct()
                                           .Count();

                if(streetCount == 0)
                {
                    continue;
                }

                MunicipalityStatistics municipalityStatistics = new MunicipalityStatistics(municipality, streetCount);

                result.Add(municipalityStatistics);
            }

            return result;
        }

        public List<NameStatistics> CalculateNameStatistics(IEnumerable<string> names)
        {
            return names
                .GroupBy(n => n)
                .Select(g => new NameStatistics(g.Key, g.Count()))
                .OrderByDescending(n => n.Count)
                .ToList();
        }

        public SimulationStatisticsResult BuildStatisticsResult(int simulationDataId, int countryVersionId)
        {
            SimulationStatistics general = _simulationDataManager.GetSimulationStatisticsBySimulationDataID(simulationDataId);
            List<Customer> domainCustomers = _customerManager.GetCustomerBySimulationDataID(simulationDataId);
            foreach(var customer in domainCustomers)
            {
                Gender gender = _nameManager.GetGenderByFirstName(customer.FirstName);
                customer.Gender = gender;
            }
            List<CustomerDTO> customers = domainCustomers.Select(ToDTO).ToList();
            List<Municipality> municipalities = _municipalityManager.GetMunicipalityByCountryVersionID(countryVersionId);
            List<Address> addresses = _addressManager.GetAddressesBySimulationDataID(simulationDataId);

            return new SimulationStatisticsResult(
                general,
                CalculateCustomersPerMunicipality(customers, municipalities),
                CalculateStreetsPerMunicipality(addresses, municipalities),
                CalculateNameStatistics(customers.Where(c => c.Gender == Gender.Male).Select(c => c.FirstName)),
                CalculateNameStatistics(customers.Where(c => c.Gender == Gender.Female).Select(c => c.FirstName)),
                CalculateNameStatistics(customers.Select(c => c.LastName)));
        }

        private CustomerDTO ToDTO(Customer customer)
        {
            return new CustomerDTO(customer.FirstName, customer.LastName, customer.Municipality, customer.Street, customer.BirthDate, customer.HouseNumber, customer.Gender);
        }
    }
}
