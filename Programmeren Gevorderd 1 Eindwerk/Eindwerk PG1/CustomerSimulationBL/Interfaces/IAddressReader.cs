using CustomerSimulationBL.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Interfaces
{
    public interface IAddressReader
    {
        IEnumerable<Address> ReadAddresses(string path);
    }
}
