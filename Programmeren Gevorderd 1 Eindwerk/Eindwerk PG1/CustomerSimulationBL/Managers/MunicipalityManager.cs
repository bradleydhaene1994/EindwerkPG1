using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Interfaces;

namespace CustomerSimulationBL.Managers
{
    public class MunicipalityManager
    {
        private IMunicipalityRepository _municipalityRepo;
        public MunicipalityManager(IMunicipalityRepository municipalityRepo)
        {
            _municipalityRepo = municipalityRepo;
        }

        public void UploadMunicipality(IEnumerable<Municipality> municipalities, int countryVersionId)
        {
            _municipalityRepo.UploadMunicipality(municipalities, countryVersionId);
        }
        public List<Municipality> GetMunicipalityByCountryVersionID(int countryVersionID)
        {
            var municipalities = _municipalityRepo.GetMunicipalityByCountryVersionID(countryVersionID);
            return municipalities;
        }
        public Municipality GetRandomMunicipality(List<Municipality> municipalities)
        {
            throw new NotImplementedException();
        }
    }
}
