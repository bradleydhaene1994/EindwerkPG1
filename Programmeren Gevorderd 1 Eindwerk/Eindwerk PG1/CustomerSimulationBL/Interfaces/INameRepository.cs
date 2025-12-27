using CustomerSimulationBL.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Interfaces
{
    public interface INameRepository
    {
        void UploadFirstName(IEnumerable<FirstName> firstNames, int countryVersionId);
        void UploadLastName(IEnumerable<LastName> lastNames, int countryVersionId);
        List<FirstName> GetFirstNamesByCountryVersionID(int countryVersionID);
        List<LastName> GetLastNamesByCountryVersionID(int countryVersionID);
    }
}
