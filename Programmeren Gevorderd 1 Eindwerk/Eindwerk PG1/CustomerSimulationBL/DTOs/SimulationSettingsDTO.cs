using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.DTOs
{
    public class SimulationSettingsDTO
    {
        public List<int> SelectedMunicipalityIds { get; set; } = new();
        public int TotalCustomers { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int MinHouseNumber { get; set; }
        public int MaxHouseNumber { get; set; }
        public bool HasLetters { get; set; }
        public int LetterPercentage { get; set; }
    }
}
