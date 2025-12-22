using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;

namespace CustomerSimulationBL.Interfaces
{
    public interface IMunicipalityRepository
    {
        void UploadMunicipality(IEnumerable<Municipality> Municipalities, int countryId);
    }
}
