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
        private readonly Random _random = new Random();
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
            if(municipalities == null)
            {
                throw new ArgumentNullException(nameof(municipalities));
            }
            if(municipalities.Count == 0)
            {
                throw new ArgumentException("List of municipalities can not be empty.", nameof(municipalities));
            }

            int index = _random.Next(municipalities.Count);
            return municipalities[index];
        }
        public void GetMunicipalitiesByCountryVersionID(int countryVersionId)
        {
            _municipalityRepo.GetMunicipalityByCountryVersionID(countryVersionId);
        }
        public Municipality GetRandomMunicipalityByPercentage(IReadOnlyList<MunicipalitySelection> municipalitySelection)
        {
            if(municipalitySelection == null || municipalitySelection.Count == 0)
            {
                throw new InvalidOperationException("No municipality selection provided.");
            }

            int randomValue = _random.Next(1, 101);
            int cumulative = 0;

            foreach(var selection in municipalitySelection)
            {
                cumulative += selection.Percentage;

                if(randomValue <= cumulative)
                {
                    return selection.Municipality;
                }
            }

            throw new InvalidOperationException("Selection failed.");
        }
    }
}
