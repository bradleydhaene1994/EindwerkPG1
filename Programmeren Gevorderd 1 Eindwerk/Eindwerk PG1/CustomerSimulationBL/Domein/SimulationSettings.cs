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
        public SimulationSettings(List<Municipality>? selectedMunicipalities, int numberCustomers, int minAge, int maxAge, string houseNumberRules)
        {
            SelectedMunicipalities = selectedMunicipalities;
            NumberCustomers = numberCustomers;
            MinAge = minAge;
            MaxAge = maxAge;
            HouseNumberRules = houseNumberRules;
        }

        public int Id { get; set; }
        public List<Municipality>? SelectedMunicipalities { get; set; }
        public int NumberCustomers { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string HouseNumberRules { get; set; }
    }
}
