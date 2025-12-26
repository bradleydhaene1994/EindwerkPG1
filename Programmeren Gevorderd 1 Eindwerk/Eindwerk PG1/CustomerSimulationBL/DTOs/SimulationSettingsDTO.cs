using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.DTOs
{
    public class SimulationSettingsDTO
    {
        public SimulationSettingsDTO(int id, List<int> selectedMunicipalityIds, int totalCustomers, int minAge, int maxAge, string houseNumberRules)
        {
            Id = id;
            SelectedMunicipalityIds = selectedMunicipalityIds;
            TotalCustomers = totalCustomers;
            MinAge = minAge;
            MaxAge = maxAge;
            HouseNumberRules = houseNumberRules;
        }

        public int Id {  get; set; }
        public List<int> SelectedMunicipalityIds { get; set; } = new();
        public int TotalCustomers { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string HouseNumberRules { get; set; }
    }
}
