using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Interfaces;

namespace CustomerSimulationBL.Managers
{
    public class NameManager
    {
        private INameRepository _nameRepo;
        private static readonly Random _random = new Random();

        public NameManager(INameRepository nameRepo)
        {
            _nameRepo = nameRepo;
        }
        public void UploadFirstName(IEnumerable<FirstName> firstNames, int countryVersionId)
        {
            _nameRepo.UploadFirstName(firstNames, countryVersionId);
        }
        public void UploadLastName(IEnumerable<LastName> lastNames, int countryVersionId)
        {
            _nameRepo.UploadLastName(lastNames, countryVersionId);
        }
        public List<FirstName> GetFirstNamesByCountryVersionID(int countryVersionID)
        {
            var firstNames = _nameRepo.GetFirstNamesByCountryVersionID(countryVersionID);
            return firstNames;
        }
        public List<LastName> GetLastNamesByCountryVersionID(int countryVersionID)
        {
            var lastNames = _nameRepo.GetLastNamesByCountryVersionID(countryVersionID);
            return lastNames;
        }
        public FirstName GetRandomFirstName(List<FirstName> firstNames)
        {
            if(firstNames == null)
            {
                throw new ArgumentNullException(nameof(firstNames));
            }
            if(firstNames.Count == 0)
            {
                throw new ArgumentException("List of first names cannot be empty.", nameof(firstNames));
            }

            bool hasFrequency = firstNames.All(x => x.Frequency.HasValue);

            if(!hasFrequency)
            {
                int index = _random.Next(firstNames.Count);
                return firstNames[index];
            }

            int totalFrequency = firstNames.Sum(x => x.Frequency!.Value);

            int randomValue = _random.Next(totalFrequency);
            int cumulative = 0;

            foreach(var firstName in firstNames)
            {
                cumulative += firstName.Frequency!.Value;
                if(randomValue < cumulative)
                {
                    return firstName;
                }
            }

            throw new InvalidOperationException("Failed to select a first name.");
        }
        public LastName GetRandomLastName(List<LastName> lastNames)
        {
            if(lastNames == null)
            {
                throw new ArgumentNullException(nameof(lastNames));
            }
            if(lastNames.Count == 0)
            {
                throw new ArgumentException("List of last names can not be empty.", nameof(lastNames));
            }

            bool hasFrequency = lastNames.All(x => x.Frequency.HasValue);

            if(!hasFrequency)
            {
                int index = _random.Next(lastNames.Count);
                return lastNames[index];
            }

            int totalFrequency = lastNames.Sum(x => x.Frequency!.Value);

            int randomValue = _random.Next(totalFrequency);
            int cumulative = 0;

            foreach(var lastName in lastNames)
            {
                cumulative += lastName.Frequency!.Value;
                if(randomValue < cumulative)
                {
                    return lastName;
                }
            }

            throw new InvalidOperationException("Failed to select a last name .");
        }
    }
}
