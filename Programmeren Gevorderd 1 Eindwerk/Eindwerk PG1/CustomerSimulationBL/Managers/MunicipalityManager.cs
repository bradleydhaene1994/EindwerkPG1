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
        //uploads municipalities to the db (only used if municipalities aren't specifically linked to streets)
        public void UploadMunicipality(IEnumerable<Municipality> municipalities, int countryVersionId)
        {
            _municipalityRepo.UploadMunicipality(municipalities, countryVersionId);
        }
        // gets municipality by the countryversionId;
        public List<Municipality> GetMunicipalityByCountryVersionID(int countryVersionID)
        {
            var municipalities = _municipalityRepo.GetMunicipalityByCountryVersionID(countryVersionID);
            return municipalities;
        }
        //returns random municipality according to a given list of municipalities
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
        //gets municipalities from the db according to the countryversionid
        public void GetMunicipalitiesByCountryVersionID(int countryVersionId)
        {
            _municipalityRepo.GetMunicipalityByCountryVersionID(countryVersionId);
        }
        //gets a random municipality from a list of specified municipalities
        public Municipality GetRandomMunicipalityBySpecifiedList(IReadOnlyList<MunicipalitySelection> municipalitySelection)
        {
            if(municipalitySelection == null || municipalitySelection.Count == 0)
            {
                return null;
            }

            int randomValue = _random.Next(0, 100);
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
