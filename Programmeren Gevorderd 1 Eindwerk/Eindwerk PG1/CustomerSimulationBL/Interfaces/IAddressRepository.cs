using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;

namespace CustomerSimulationBL.Interfaces
{
    public interface IAddressRepository
    {
        void UploadAddress(Address address, int countryVersionID);
        List<Address> GetAddressesByCountryVersionID(int countryVersionId);
    }
}
