using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Enumerations;
using CustomerSimulationBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CustomerSimulationDL.FileReaders
{
    public class TxtFileReader : ITxtReader
    {
        public IEnumerable<FirstName> ReadFirstNames(string path, string countryName)
        {
            var FirstNames = new List<FirstName>();

            bool isDanish = countryName.Equals("Denmark", StringComparison.OrdinalIgnoreCase);
            bool isFinish = countryName.Equals("Finland", StringComparison.OrdinalIgnoreCase);
            bool isSpanish = countryName.Equals("Spain", StringComparison.OrdinalIgnoreCase);
            bool isSwedish = countryName.Equals("Sweden", StringComparison.OrdinalIgnoreCase);
            bool isSwiss = countryName.Equals("Switserland", StringComparison.OrdinalIgnoreCase);

            int? skiplines;

            if (isDanish)
            {
                skiplines = 5;
            }
            else if (isFinish)
            {
                skiplines = 1;
            }
            else if (isSpanish)
            {
                skiplines = 7;
            }
            else if (isSwedish)
            {
                skiplines = 5;
            }
            else if (isSwiss)
            {
                skiplines = 6;
            }
            else
            {
                skiplines = null;
            }

            using(var sr = new StreamReader(path))
            {
                for(int i = 0; i < skiplines; i++)
                {
                    sr.ReadLine();
                }

                string line;

                while((line = sr.ReadLine()) != null && !string.IsNullOrWhiteSpace(line))
                {
                    if(string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    var parts = line.Split(new char[] { '\t', ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    int startIndex = 0;

                    string firstindexcheck = parts[0].Replace(".", "").Trim();

                    if(int.TryParse(firstindexcheck, out _))
                    {
                        startIndex = 1;
                    }

                    string Name = parts[startIndex];
                    int Frequency;

                    if (isSwiss && int.TryParse(parts[startIndex + 1], out int fFreq))
                    {
                        Frequency = fFreq;
                    }
                    else if (isSwiss && int.TryParse(parts[startIndex + 2], out int mFreq))
                    {
                        Frequency = mFreq;
                    }
                    else
                    {
                        Frequency = int.Parse(parts[startIndex + 1].Replace(".", ""));
                    }

                    Gender Gender;

                    if (path.Contains("hombre") || path.Contains("miehet") || path.Contains("mænd") || path.Contains("män") || parts[startIndex + 1] == "*")
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
                return FirstNames;
            }
        }
        public IEnumerable<LastName> ReadLastNames(string path, string countryName)
        {
            var LastNames = new List<LastName>();

            bool isDanish = countryName.Equals("Denmark", StringComparison.OrdinalIgnoreCase);
            bool isFinish = countryName.Equals("Finland", StringComparison.OrdinalIgnoreCase);
            bool isSpanish = countryName.Equals("Spain", StringComparison.OrdinalIgnoreCase);
            bool isSwedish = countryName.Equals("Sweden", StringComparison.OrdinalIgnoreCase);
            bool isSwiss = countryName.Equals("Switserland", StringComparison.OrdinalIgnoreCase);

            int? skiplines;

            if (isDanish)
            {
                skiplines = 5;
            }
            else if (isFinish)
            {
                skiplines = 1;
            }
            else if (isSpanish)
            {
                skiplines = 7;
            }
            else if (isSwedish)
            {
                skiplines = 5;
            }
            else if (isSwiss)
            {
                skiplines = 6;
            }
            else
            {
                skiplines = null;
            }

            using (var sr = new StreamReader(path))
            {
                for (int i = 0; i < skiplines; i++)
                {
                    sr.ReadLine();
                }
                string line;

                while((line = sr.ReadLine()) != null && !string.IsNullOrWhiteSpace(line))
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    var parts = line.Split(new char[] { '\t', ';', ','}, StringSplitOptions.RemoveEmptyEntries);
                    int startIndex = 0;

                    string firstindexcheck = parts[0].Replace(".", "").Trim();

                    if (int.TryParse(firstindexcheck, out _))
                    {
                        startIndex = 1;
                    }

                    string Name = parts[startIndex];
                    int Frequency;

                    if(isSwiss)
                    {
                        Frequency = int.Parse(parts[startIndex + 2]);
                    }
                    else
                    {
                        Frequency = int.Parse(parts[startIndex + 1].Replace(".", ""));
                    }

                    LastName LastName = new LastName(Name, Frequency, null);

                    LastNames.Add(LastName);
                }
            }
            return LastNames;
        }
    }
}
