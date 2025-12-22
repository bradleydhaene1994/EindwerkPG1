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
        public void UploadFirstName(IEnumerable<FirstName> firstNames, Country country)
        {
            _nameRepo.UploadFirstName(firstNames, country);
        }
        public void UploadLastName(IEnumerable<LastName> lastNames, Country country)
        {
            _nameRepo.UploadLastName(lastNames, country);
        }
    }
}
