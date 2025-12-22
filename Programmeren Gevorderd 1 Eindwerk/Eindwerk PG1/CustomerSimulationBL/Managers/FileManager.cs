using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Interfaces;

namespace CustomerSimulationBL.Managers
{
    public class FileManager
    {
        private IFirstNameReader _firstNameReader;
        private ILastNameReader _lastNameReader;
        private IMunicipalityReader _municipalityReader;
        private IAddressReader _addressReader;

        public FileManager(IFirstNameReader firstNameReader, ILastNameReader lastNameReader, IMunicipalityReader municipalityReader, IAddressReader addressReader)
        {
            _firstNameReader = firstNameReader;
            _lastNameReader = lastNameReader;
            _municipalityReader = municipalityReader;
            _addressReader = addressReader;
        }

        public IEnumerable<FirstName> ReadFirstNames(string path)
        {
            IEnumerable<FirstName> firstNames = _firstNameReader.ReadFirstNames(path);
            return firstNames;
        }
        public IEnumerable<LastName> ReadLastNames(string path)
        {
            IEnumerable<LastName> lastNames = _lastNameReader.ReadLastNames(path);
            return lastNames;
        }
        public IEnumerable<Municipality> ReadMunicipalities(string path)
        {
            IEnumerable<Municipality> municipalities = _municipalityReader.ReadMunicipalities(path);
            return municipalities;
        }
        public IEnumerable<Address> ReadAddresses(string path)
        {
            IEnumerable<Address> addresses = _addressReader.ReadAddresses(path);
            return addresses;
        }
    }
}
