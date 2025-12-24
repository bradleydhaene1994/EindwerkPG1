using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;

namespace CustomerSimulationBL.Interfaces
{
    public interface ITxtReader
    {
        IEnumerable<FirstName> ReadFirstNames(string path);
        IEnumerable<LastName> ReadLastNames(string path);
    }
}
