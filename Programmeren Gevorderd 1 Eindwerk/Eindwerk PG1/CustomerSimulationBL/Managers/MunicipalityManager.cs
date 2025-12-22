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

        public void UploadMunicipality(IEnumerable<Municipality> municipalities, int countryId)
        {
            _municipalityRepo.UploadMunicipality(municipalities, countryId);
        }
    }
}
