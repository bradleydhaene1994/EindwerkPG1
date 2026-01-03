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
        IEnumerable<FirstName> ReadFirstNames(string path, string countryName);
        IEnumerable<LastName> ReadLastNames(string path, string countryName);
        IEnumerable<Address> ReadAddresses(string path);
        IEnumerable<Municipality> ReadMunicipalities(string path);
    }
}
