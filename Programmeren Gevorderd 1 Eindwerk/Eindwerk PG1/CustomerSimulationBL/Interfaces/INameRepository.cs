using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Interfaces
{
    public interface INameRepository
    {
        void UploadFirstName(IEnumerable<FirstName> firstNames, int countryVersionId, IProgress<int> progress);
        void UploadLastName(IEnumerable<LastName> lastNames, int countryVersionId, IProgress<int> progress);
        List<FirstName> GetFirstNamesByCountryVersionID(int countryVersionID);
        List<LastName> GetLastNamesByCountryVersionID(int countryVersionID);
        Gender GetGenderByFirstName(string firstName); 
    }
}
