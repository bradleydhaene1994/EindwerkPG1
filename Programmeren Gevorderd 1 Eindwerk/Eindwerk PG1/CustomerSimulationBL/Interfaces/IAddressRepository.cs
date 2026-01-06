using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using Microsoft.Data.SqlClient;

namespace CustomerSimulationBL.Interfaces
{
    public interface IAddressRepository
    {
        void UploadAddress(IEnumerable<Address> addresses, int countryVersionIDn, IProgress<int> progress);
        List<Address> GetAddressesByCountryVersionID(int countryVersionId, List<Municipality> municipalities);
        List<Address> GetAddressesBySimulationDataID(int simulationDataId);
        bool HasAddresses(int countryVersionId);
    }
}
