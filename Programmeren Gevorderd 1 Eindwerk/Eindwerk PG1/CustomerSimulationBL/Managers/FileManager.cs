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
        //reads first names according according to the filepath
        public IEnumerable<FirstName> ReadFirstNames(string path, string countryName)
        {
            IEnumerable<FirstName> firstNames;

            if(path.Contains(".csv"))
            {
                firstNames = _csvReader.ReadFirstNames(path);
            }
            else if(Path.GetExtension(path).Equals(".json", StringComparison.OrdinalIgnoreCase))
            {
                firstNames = _jsonReader.ReadFirstNames(path, countryName);
            }
            else if(path.Contains(".txt"))
            {
                firstNames = _txtReader.ReadFirstNames(path, countryName);
            }
            else
            {
                throw new Exception("Unsupported File Type");
            }

            return firstNames;
        }
        //reads last names according to filepath
        public IEnumerable<LastName> ReadLastNames(string path, string countryName)
        {
            IEnumerable<LastName> lastNames;

            if (path.Contains(".csv"))
            {
                lastNames = _csvReader.ReadLastNames(path);
            }
            else if (path.Contains("json"))
            {
                lastNames = _jsonReader.ReadLastNames(path, countryName);
            }
            else if (path.Contains(".txt"))
            {
                lastNames = _txtReader.ReadLastNames(path, countryName);
            }
            else
            {
                throw new Exception("Unsupported File Type");
            }

            return lastNames;
        }
        //reads municipalities and returns a list
        public IEnumerable<Municipality> ReadMunicipalities(string path)
        {
            IEnumerable<Municipality> municipalities = _jsonReader.ReadMunicipalities(path);
            return municipalities;
        }
        //reads addresses ad returns a list
        public IEnumerable<Address> ReadAddresses(string path)
        {
            IEnumerable<Address> addresses;

            if(path.Contains(".csv"))
            {
                addresses = _csvReader.ReadAddresses(path);
            }
            else if(path.Contains(".json"))
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
