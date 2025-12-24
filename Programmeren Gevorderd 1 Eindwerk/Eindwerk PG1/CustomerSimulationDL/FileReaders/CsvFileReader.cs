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
    public class CsvFileReader : ICsvReader
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

                    string RawMunicipalityName = parts[0].Trim();
                    Municipality Municipality;

                    if (RawMunicipalityName == "(unknown)")
                    {
                        Municipality = null;
                    }
                    else
                    {
                        string MunicipalityName = RawMunicipalityName.Replace("Kommune", "").Replace("kommun", "").Trim();
                        Municipality = new Municipality(MunicipalityName);
                    }

                    string StreetName = parts[1].Trim();
                    string HighwayType = parts[2].Trim();

                    Address Address = new Address(Municipality, StreetName);

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
