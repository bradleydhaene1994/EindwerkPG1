using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;

namespace CustomerSimulationBL.DTOs
{
    public class SimulationSettingsDTO
    {
        public SimulationSettingsDTO(List<MunicipalitySelection> selectedMunicipalities, int totalCustomers, int minAge, int maxAge, int minNumber, int maxNumber, bool hasLetters, int percentageLetters)
        {
            SelectedMunicipalities = selectedMunicipalities;
            TotalCustomers = totalCustomers;
            MinAge = minAge;
            MaxAge = maxAge;
            this.minNumber = minNumber;
            this.maxNumber = maxNumber;
            HasLetters = hasLetters;
            PercentageLetters = percentageLetters;
        }

        public int Id {  get; set; }
        public List<MunicipalitySelection> SelectedMunicipalities { get; set; } = new();
        public int TotalCustomers { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int minNumber { get; set; }
        public int maxNumber { get; set; }
        public bool HasLetters { get; set; }
        public int PercentageLetters { get; set; }
    }
}
