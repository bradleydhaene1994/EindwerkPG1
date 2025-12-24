using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Domein
{
    public class SimulationStatistics
    {
        public SimulationStatistics(int totalCustomers, Dictionary<int, Municipality>? customersPerMunicipality, int averageAgeOnSimulationDate, int averageAgeOnCurrentDate, int ageYoungestCustomer, int ageOldestCustomer)
        {
            TotalCustomers = totalCustomers;
            CustomersPerMunicipality = customersPerMunicipality;
            AverageAgeOnSimulationDate = averageAgeOnSimulationDate;
            AverageAgeOnCurrentDate = averageAgeOnCurrentDate;
            AgeYoungestCustomer = ageYoungestCustomer;
            AgeOldestCustomer = ageOldestCustomer;
        }

        public int Id { get; set; }
        public int TotalCustomers { get; set; }
        public Dictionary<int, Municipality>? CustomersPerMunicipality { get; set; }
        public int AverageAgeOnSimulationDate { get; set; }
        public int AverageAgeOnCurrentDate { get; set; }
        public int AgeYoungestCustomer {  get; set; }
        public int AgeOldestCustomer { get; set; }
    }
}
