using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;

namespace CustomerSimulationBL.Interfaces
{
    public interface ICountryVersionRepository
    {
        int UploadCountryVersion(CountryVersion countryVersion, int countryId);
        List<Country> GetAllCountries();
        List<CountryVersion> GetAllCountryVersions();
    }
}
