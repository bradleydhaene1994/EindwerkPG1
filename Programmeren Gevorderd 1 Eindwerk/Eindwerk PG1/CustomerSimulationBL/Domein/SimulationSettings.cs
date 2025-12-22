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
        public int Id { get; set; }
        public SimulationData SimulationData { get; set; }
        public List<Municipality> SelectedMunicipalities { get; set; }
        public Country Country { get; set; }
        public int NumberCustomers { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string HouseNumberRules { get; set; }
    }
}
