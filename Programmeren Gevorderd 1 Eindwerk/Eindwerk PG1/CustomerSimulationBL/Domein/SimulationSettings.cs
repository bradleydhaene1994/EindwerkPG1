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
        public SimulationSettings(int id, IReadOnlyList<int> selectedMunicipalityIds, int totalCustomers, int minAge, int maxAge, int minNumber, int maxNumber, bool hasLetters, int percentageLetters)
        {
            Id = id;
            SelectedMunicipalityIds = selectedMunicipalityIds;
            TotalCustomers = totalCustomers;
            this.minAge = minAge;
            this.maxAge = maxAge;
            this.minNumber = minNumber;
            this.maxNumber = maxNumber;
            HasLetters = hasLetters;
            PercentageLetters = percentageLetters;
        }

        public int Id { get; set; }
        public IReadOnlyList<int> SelectedMunicipalityIds { get; }
        public int TotalCustomers { get; }
        public int minAge { get; }
        public int maxAge { get; }
        public int minNumber { get; }
        public int maxNumber { get; }
        public bool HasLetters { get; }
        public int PercentageLetters { get; }

        public string HouseNumberRulesToString()
        {
            return $"Minimum number: {minNumber}, Maximum number: {maxNumber}, Has Letters: {HasLetters}, Percentage appearance letters in housenumbers: {PercentageLetters}%";
        }
    }
}
