using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;

namespace CustomerSimulationBL.Interfaces
{
    public interface IJsonReader
    {
        IEnumerable<FirstName> ReadFirstNames(string path);
        IEnumerable<LastName> ReadLastNames(string path);
        IEnumerable<Address> ReadAddresses(string path);
        IEnumerable<Municipality> ReadMunicipalities(string path);
    }
}
