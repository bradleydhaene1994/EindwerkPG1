using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Domein
{
    public class SimulationStatistics
    {
        public int Id { get; set; }
        public SimulationData SimulationData { get; set; }
        public int TotalCustomers { get; set; }
        public Dictionary<int, Municipality> CustomersPerMunicipality { get; set; }
        public int AverageAgeOnSimulationDate { get; set; }
        public int AverageAgeOCurrentDate { get; set; }
        public int AgeYoungestCustomer {  get; set; }
        public int AgeOldestCustomer { get; set; }
    }
}
