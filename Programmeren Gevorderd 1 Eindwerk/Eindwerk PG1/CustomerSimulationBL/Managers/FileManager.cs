using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Interfaces;

namespace CustomerSimulationBL.Managers
{
    public class FileManager
    {
        private ICsvReader _csvReader;
        private ITxtReader _txtReader;
        private IJsonReader _jsonReader;

        public FileManager(ICsvReader csvReader, ITxtReader txtReader, IJsonReader jsonReader)
        {
            _csvReader = csvReader;
            _txtReader = txtReader;
            _jsonReader = jsonReader;
        }

        public IEnumerable<FirstName> ReadFirstNames(string path)
        {
            IEnumerable<FirstName> firstNames;

            if(path.Contains(".csv"))
            {
                firstNames = _csvReader.ReadFirstNames(path);
            }
            else if(path.Contains("json"))
            {
                firstNames = _jsonReader.ReadFirstNames(path);
            }
            else if(path.Contains(".txt"))
            {
                firstNames = _txtReader.ReadFirstNames(path);
            }
            else
            {
                throw new Exception("Unsupported File Type");
            }

            return firstNames;
        }
        public IEnumerable<LastName> ReadLastNames(string path)
        {
            IEnumerable<LastName> lastNames;

            if (path.Contains(".csv"))
            {
                lastNames = _csvReader.ReadLastNames(path);
            }
            else if (path.Contains("json"))
            {
                lastNames = _jsonReader.ReadLastNames(path);
            }
            else if (path.Contains(".txt"))
            {
                lastNames = _txtReader.ReadLastNames(path);
            }
            else
            {
                throw new Exception("Unsupported File Type");
            }

            return lastNames;
        }
        public IEnumerable<Municipality> ReadMunicipalities(string path)
        {
            IEnumerable<Municipality> municipalities = _jsonReader.ReadMunicipalities(path);
            return municipalities;
        }
        public IEnumerable<Address> ReadAddresses(string path)
        {
            IEnumerable<Address> addresses;

            if(path.Contains(".csv"))
            {
                addresses = _csvReader.ReadAddresses(path);
            }
            else if(path.Contains("json"))
            {
                addresses = _jsonReader.ReadAddresses(path);
            }
            else
            {
                throw new Exception("Unsupported File Type");
            }

            return addresses;
        }
    }
}
