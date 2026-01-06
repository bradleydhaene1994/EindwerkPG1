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
        //uploads countryversion to db and returns the inserted id
        public int UploadCountryVersion(int countryId, int simulationDataId)
        {
            int countryVersionId = _countryVersionRepo.GetOrUploadCountryVersion(countryId, simulationDataId);
            return countryVersionId;
        }
        //gets country version from the db using an id
        public CountryVersion GetCountryVersionById(int countryVersionId)
        {
            return _countryVersionRepo.GetCountryVersionById(countryVersionId);
        }
    }
}
