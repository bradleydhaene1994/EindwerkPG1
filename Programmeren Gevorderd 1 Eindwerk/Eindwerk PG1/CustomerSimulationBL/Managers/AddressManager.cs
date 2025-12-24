using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Interfaces;

namespace CustomerSimulationBL.Managers
{
    public class AddressManager
    {
        private IAddressRepository _addressRepo;
        public AddressManager(IAddressRepository addressRepo)
        {
            _addressRepo = addressRepo;
        }

        public void UploadAddress(IEnumerable<Address> addresses, int countryVersionID)
        {
            _addressRepo.UploadAddress(addresses, countryVersionID);
        }

        public List<Address> GetAddressesByCountryVersionID(int countryVersionId)
        {
            var addresses = _addressRepo.GetAddressesByCountryVersionID(countryVersionId);
            return addresses;
        }
        public Address GetRandomAddress(List<Address> addresses)
        {
            throw new NotImplementedException();
        }
    }
}
