using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Interfaces;

namespace CustomerSimulationBL.Managers
{
    public class NameManager
    {
        private INameRepository _nameRepo;

        public NameManager(INameRepository nameRepo)
        {
            _nameRepo = nameRepo;
        }
        public void UploadFirstName(IEnumerable<FirstName> firstNames, CountryVersion countryVersion)
        {
            _nameRepo.UploadFirstName(firstNames, countryVersion);
        }
        public void UploadLastName(IEnumerable<LastName> lastNames, CountryVersion countryVersion)
        {
            _nameRepo.UploadLastName(lastNames, countryVersion);
        }
        public List<FirstName> GetFirstNamesByCountryVersionID(int countryVersionID)
        {
            var firstNames = _nameRepo.GetFirstNamesByCountryVersionID(countryVersionID);
            return firstNames;
        }
        public List<LastName> GetLastNamesByCountryVersionID(int countryVersionID)
        {
            var lastNames = _nameRepo.GetLastNamesByCountryVersionID(countryVersionID);
            return lastNames;
        }
        public FirstName GetRandomFirstName(List<FirstName> firstNames)
        {
            throw new NotImplementedException();
        }
        public LastName GetRandomLastNames(List<LastName> lastNames)
        {
            throw new NotImplementedException();
        }
    }
}
