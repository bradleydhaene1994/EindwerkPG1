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
        private static readonly Random _random = new Random();

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
            if(addresses == null)
            {
                throw new ArgumentNullException(nameof(addresses));
            }
            if(addresses.Count == 0)
            {
                throw new ArgumentException("Address list can not be empty.", nameof(addresses));
            }

            int index = _random.Next(addresses.Count);
            return addresses[index];
        }
    }
}
