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
    }
}
