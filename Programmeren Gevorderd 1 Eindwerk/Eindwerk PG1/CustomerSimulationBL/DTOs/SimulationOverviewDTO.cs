using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;

namespace CustomerSimulationBL.DTOs
{
    public class SimulationOverviewDTO
    {
        public SimulationOverviewDTO(int simulationDataId, string countryName, int year, string clientName, DateTime dateCreated)
        {
            SimulationDataId = simulationDataId;
            CountryName = countryName;
            Year = year;
            ClientName = clientName;
            DateCreated = dateCreated;
        }

        public int SimulationDataId { get; set; }
        public string CountryName { get; set; }
        public int Year { get; set; }
        public string ClientName { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
