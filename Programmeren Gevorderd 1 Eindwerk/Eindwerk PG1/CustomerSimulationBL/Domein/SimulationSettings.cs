using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace CustomerSimulationBL.Domein
{
    public class SimulationSettings
    {
        public SimulationSettings(List<int> selectedMunicipalityIds, int totalCustomers, int minAge, int maxAge, int minNumber, int maxNumber, bool hasLetters, int percentageLetters)
        {
            SelectedMunicipalityIds = selectedMunicipalityIds;
            TotalCustomers = totalCustomers;
            MinAge = minAge;
            MaxAge = maxAge;
            MinNumber = minNumber;
            MaxNumber = maxNumber;
            HasLetters = hasLetters;
            PercentageLetters = percentageLetters;
        }

        public SimulationSettings(int id, List<int> selectedMunicipalityIds, int totalCustomers, int minAge, int maxAge, int minNumber, int maxNumber, bool hasLetters, int percentageLetters)
        {
            Id = id;
            SelectedMunicipalityIds = selectedMunicipalityIds;
            TotalCustomers = totalCustomers;
            this.MinAge = minAge;
            this.MaxAge = maxAge;
            this.MinNumber = minNumber;
            this.MaxNumber = maxNumber;
            HasLetters = hasLetters;
            PercentageLetters = percentageLetters;
        }

        public int Id { get; set; }
        public List<int> SelectedMunicipalityIds { get; }
        public int TotalCustomers { get; }
        public int MinAge { get; }
        public int MaxAge { get; }
        public int MinNumber { get; }
        public int MaxNumber { get; }
        public bool HasLetters { get; }
        public int PercentageLetters { get; }

        public string HouseNumberRulesToString()
        {
            return $"Minimum number: {MinNumber}, Maximum number: {MaxNumber}, Has Letters: {HasLetters}, Percentage appearance letters in housenumbers: {PercentageLetters}%";
        }
    }
}
