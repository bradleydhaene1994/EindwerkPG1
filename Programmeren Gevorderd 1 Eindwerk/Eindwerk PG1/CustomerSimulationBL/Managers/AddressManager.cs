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

        public void UploadAddress(IEnumerable<Address> addresses, int countryVersionID, IProgress<int> progress)
        {
            _addressRepo.UploadAddress(addresses, countryVersionID, progress);
        }

        public List<Address> GetAddressesByCountryVersionID(int countryVersionId, List<Municipality> municipalities)
        {
            var addresses = _addressRepo.GetAddressesByCountryVersionID(countryVersionId, municipalities);
            return addresses;
        }
        public Address GetRandomAddressByMunicipality(List<Address> addresses, Municipality municipality)
        {
            if (municipality == null)
            {
                throw new ArgumentNullException(nameof(municipality));
            }

            var filtered = addresses.Where(a => a.Municipality == municipality).ToList();

            if (filtered.Count == 0)
            {
                throw new InvalidOperationException($"No addresses found for municipality {municipality.Name}");
            }

            return filtered[_random.Next(filtered.Count)];
        }
    }
}
