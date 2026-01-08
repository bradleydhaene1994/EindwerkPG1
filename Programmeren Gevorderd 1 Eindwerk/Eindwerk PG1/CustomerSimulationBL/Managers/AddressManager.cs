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
        //used to pick random addressses
        private static readonly Random _random = new Random();

        public AddressManager(IAddressRepository addressRepo)
        {
            _addressRepo = addressRepo;
        }
        //uploads addresses to db
        public void UploadAddress(IEnumerable<Address> addresses, int countryVersionID, IProgress<int> progress)
        {
            _addressRepo.UploadAddress(addresses, countryVersionID, progress);
        }
        //gets addresses from the db depending on the countryversionid
        public List<Address> GetAddressesByCountryVersionID(int countryVersionId, List<Municipality> municipalities)
        {
            var addresses = _addressRepo.GetAddressesByCountryVersionID(countryVersionId, municipalities);
            return addresses;
        }
        //gets a random address according to a municipality
        public Address GetRandomAddressByMunicipality(List<Address> addresses, Municipality municipality)
        {
            //check if municipality is null, if so throw error
            if (municipality == null)
            {
                throw new ArgumentNullException(nameof(municipality));
            }

            //this LINQ only keeps addresses belonging to given municipality and stores them in a list
            var filtered = addresses.Where(a => a.Municipality == municipality).ToList();

            var source = filtered.Count > 0 ? filtered : addresses;

            //picks random address according to the index of the filtered list
            return source[_random.Next(source.Count)];
        }
        public Address GetRandomAddress(List<Address> addresses)
        {
            if (addresses == null || addresses.Count == 0)
                throw new InvalidOperationException("No addresses available.");

            return addresses[_random.Next(addresses.Count)];
        }
        //gets addresses from the db using a simulationdataid
        public List<Address> GetAddressesBySimulationDataID(int simulationDataId)
        {
            List<Address> addresses = _addressRepo.GetAddressesBySimulationDataID(simulationDataId);
            return addresses;
        }
        public List<Address> GetAllAddressesByCountryVersionID(int countryVersionId)
        {
            return _addressRepo.GetAllAddressesByCountryVersionID(countryVersionId);
        }
    }
}
