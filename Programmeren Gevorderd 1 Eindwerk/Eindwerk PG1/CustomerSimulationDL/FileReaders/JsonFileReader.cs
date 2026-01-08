using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Interfaces;
using CustomerSimulationBL.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomerSimulationDL.FileReaders
{
    public class JsonFileReader : IJsonReader
    {
        public IEnumerable<FirstName> ReadFirstNames(string path, string countryName)
        {
            var FirstNames = new List<FirstName>();
            string JsonString = File.ReadAllText(path);

            using(JsonDocument doc = JsonDocument.Parse(JsonString))
            {
                JsonElement root = doc.RootElement;
                JsonElement nameSection = root.GetProperty("name");

                if(countryName == "Poland")
                {
                    foreach (var item in nameSection.GetProperty("first_name_male").EnumerateArray())
                    {
                        FirstName FirstName = new FirstName(item.GetString(), null, Gender.Male);
                        FirstNames.Add(FirstName);
                    }
                    foreach (var item in nameSection.GetProperty("first_name_female").EnumerateArray())
                    {
                        FirstName FirstName = new FirstName(item.GetString(), null, Gender.Female);
                        FirstNames.Add(FirstName);
                    }
                }
                else if(countryName == "Czech Republic")
                {
                    foreach (var item in nameSection.GetProperty("male_first_name").EnumerateArray())
                    {
                        FirstName FirstName = new FirstName(item.GetString(), null, Gender.Male);
                        FirstNames.Add(FirstName);
                    }
                    foreach (var item in nameSection.GetProperty("female_first_name").EnumerateArray())
                    {
                        FirstName FirstName = new FirstName(item.GetString(), null, Gender.Female);
                        FirstNames.Add(FirstName);
                    }
                }
            }
            return FirstNames;
        }
        public IEnumerable<LastName> ReadLastNames(string path, string countryName)
        {
            var LastNames = new List<LastName>();
            string JsonString = File.ReadAllText(path);

            using (JsonDocument doc = JsonDocument.Parse(JsonString))
            {
                JsonElement root = doc.RootElement;
                JsonElement nameSection = root.GetProperty("name");

                if(countryName == "Poland")
                {
                    foreach (var item in nameSection.GetProperty("last_name").EnumerateArray())
                    {
                        LastName LastName = new LastName(item.GetString(), null, null);
                        LastNames.Add(LastName);
                    }
                }
                else if(countryName == "Czech Republic")
                {
                    foreach (var item in nameSection.GetProperty("male_last_name").EnumerateArray())
                    {
                        LastName LastName = new LastName(item.GetString(), null, Gender.Male);
                        LastNames.Add(LastName);
                    }
                    foreach (var item in nameSection.GetProperty("female_last_name").EnumerateArray())
                    {
                        LastName LastName = new LastName(item.GetString(), null, Gender.Female);
                        LastNames.Add(LastName);
                    }
                }
            }
            return LastNames;
        }
        public IEnumerable<Address> ReadAddresses(string path)
        {
            var Addresses = new List<Address>();
            string JsonString = File.ReadAllText(path);

            using (JsonDocument doc = JsonDocument.Parse(JsonString))
            {
                JsonElement root = doc.RootElement;
                JsonElement nameSection = root.GetProperty("address");
                foreach(var item in nameSection.GetProperty("street").EnumerateArray())
                {
                    Address Address = new Address(null, item.GetString());
                    Addresses.Add(Address);
                }
            }
            return Addresses;
        }

        public IEnumerable<Municipality> ReadMunicipalities(string path)
        {
            var Municipalities = new List<Municipality>();

            string JsonString = File.ReadAllText(path);

            using (JsonDocument doc = JsonDocument.Parse(JsonString))
            {
                JsonElement root = doc.RootElement;
                JsonElement nameSection = root.GetProperty("address");
                foreach (var item in nameSection.GetProperty("city_name").EnumerateArray())
                {
                    Municipality Municipality = new Municipality(item.GetString());
                    Municipalities.Add(Municipality);
                }
            }
            return Municipalities;
        }
    }
}
