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
        void UploadFirstName(List<FirstName> lirstNames, int countryId);
        void UploadLastName(List<LastName> lastNames, int countryId);
    }
}
