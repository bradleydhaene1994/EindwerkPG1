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
        void UploadFirstName(IEnumerable<FirstName> lirstNames, Country country);
        void UploadLastName(IEnumerable<LastName> lastNames, Country country);
    }
}
