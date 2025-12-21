using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Enumerations;
using CustomerSimulationBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationDL.FileReaders
{
    public class CsvFileReader : IAddressReader, IFirstNameReader, ILastNameReader
    {
        public IEnumerable<Address> ReadAddresses(string path)
        {
            var Addresses = new List<Address>();

            using (var sr = new StreamReader(path))
            {
                sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var parts = line.Split(',', ';', '\t');

                    string MunicipalityName = parts[0].Trim();
                    string StreetName = parts[1].Trim();
                    string HighwayType = parts[2].Trim();

                    Municipality Municipality = new Municipality(MunicipalityName);

                    Address Address = new Address(Municipality, StreetName, HighwayType);

                    Addresses.Add(Address);
                }
            }
            return Addresses;
        }
        public IEnumerable<FirstName> ReadFirstNames(string path)
        {
            var FirstNames = new List<FirstName>();

            using(var sr = new StreamReader(path))
            {
                sr.ReadLine();
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    var parts = line.Split(',', '\t', ';');

                    string Name = parts[1].Trim();
                    int Frequency = int.Parse(parts[2].Trim());

                    Gender Gender;

                    if(path.Contains("man"))
                    {
                        Gender = Gender.Male;
                    }
                    else
                    {
                        Gender = Gender.Female;
                    }

                    FirstName FirstName = new FirstName(Name, Frequency, Gender);

                    FirstNames.Add(FirstName);
                }
            }
            return FirstNames;
        }
        public IEnumerable<LastName> ReadLastNames(string path)
        {
            var LastNames = new List<LastName>();

            using(var sr = new StreamReader(path))
            {
                sr.ReadLine();
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    var parts = line.Split(',', '\t', ';');

                    string Name = parts[1].Trim();
                    int Frequency = int.Parse(parts[2].Trim());

                    LastName LastName = new LastName(Name, Frequency, null);

                    LastNames.Add(LastName);
                }
                return LastNames;
            }
        }
    }
}
