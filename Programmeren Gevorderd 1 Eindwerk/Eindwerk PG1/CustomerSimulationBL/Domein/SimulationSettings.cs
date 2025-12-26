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
        public SimulationSettings(int id, IReadOnlyList<int> selectedMunicipalityIds, int totalCustomers, int minAge, int maxAge, string houseNumberRules)
        {
            Id = id;
            SelectedMunicipalityIds = selectedMunicipalityIds;
            TotalCustomers = totalCustomers;
            this.minAge = minAge;
            this.maxAge = maxAge;
            HouseNumberRules = houseNumberRules;
        }

        public int Id { get; private set; }
        public IReadOnlyList<int> SelectedMunicipalityIds { get; }
        public int TotalCustomers { get; }
        public int minAge { get; }
        public int maxAge { get; }
        public string HouseNumberRules { get; }
    }
}
