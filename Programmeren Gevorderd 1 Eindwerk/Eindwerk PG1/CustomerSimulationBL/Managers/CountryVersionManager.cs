using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Interfaces;

namespace CustomerSimulationBL.Managers
{
    public class CountryVersionManager
    {
        private ICountryVersionRepository _countryVersionRepo;

        public CountryVersionManager(ICountryVersionRepository countryVersionRepo)
        {
            _countryVersionRepo = countryVersionRepo;
        }

        public void UploadCountryVersion(Country country, int version)
        {
            _countryVersionRepo.UploadCountryVersion(country, version);
        }
    }
}
