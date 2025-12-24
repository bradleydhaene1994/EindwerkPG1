using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Domein
{
    public class SimulationStatistics
    {
        public SimulationStatistics(int totalCustomers, Dictionary<int, Municipality>? customersPerMunicipality, decimal averageAgeOnSimulationDate, decimal averageAgeOnCurrentDate, int ageYoungestCustomer, int ageOldestCustomer)
        {
            TotalCustomers = totalCustomers;
            CustomersPerMunicipality = customersPerMunicipality;
            AverageAgeOnSimulationDate = averageAgeOnSimulationDate;
            AverageAgeOnCurrentDate = averageAgeOnCurrentDate;
            AgeYoungestCustomer = ageYoungestCustomer;
            AgeOldestCustomer = ageOldestCustomer;
        }

        public SimulationStatistics(int id, int totalCustomers, Dictionary<int, Municipality>? customersPerMunicipality, decimal averageAgeOnSimulationDate, decimal averageAgeOnCurrentDate, int ageYoungestCustomer, int ageOldestCustomer)
        {
            Id = id;
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
        public decimal AverageAgeOnSimulationDate { get; set; }
        public decimal AverageAgeOnCurrentDate { get; set; }
        public int AgeYoungestCustomer {  get; set; }
        public int AgeOldestCustomer { get; set; }
    }
}
