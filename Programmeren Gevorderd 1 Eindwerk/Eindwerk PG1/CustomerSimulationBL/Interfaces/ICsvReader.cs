using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;

namespace CustomerSimulationBL.Interfaces
{
    public interface ICsvReader
    {
        IEnumerable<Address> ReadAddresses(string path);
        IEnumerable<FirstName> ReadFirstNames(string path);
        IEnumerable<LastName> ReadLastNames(string path);
    }
}
