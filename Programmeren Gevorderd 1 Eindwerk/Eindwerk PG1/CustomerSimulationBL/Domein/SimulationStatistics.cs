using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Domein
{
    public class SimulationStatistics
    {
        public SimulationStatistics(int totalCustomers, Dictionary<int, Municipality>? customersPerMunicipality, double averageAgeOnSimulationDate, double averageAgeOnCurrentDate, int ageYoungestCustomer, int ageOldestCustomer)
        {
            TotalCustomers = totalCustomers;
            CustomersPerMunicipality = customersPerMunicipality;
            AverageAgeOnSimulationDate = averageAgeOnSimulationDate;
            AverageAgeOnCurrentDate = averageAgeOnCurrentDate;
            AgeYoungestCustomer = ageYoungestCustomer;
            AgeOldestCustomer = ageOldestCustomer;
        }

        public SimulationStatistics(int id, int totalCustomers, Dictionary<int, Municipality>? customersPerMunicipality, double averageAgeOnSimulationDate, double averageAgeOnCurrentDate, int ageYoungestCustomer, int ageOldestCustomer)
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
        public double AverageAgeOnSimulationDate { get; set; }
        public double AverageAgeOnCurrentDate { get; set; }
        public int AgeYoungestCustomer {  get; set; }
        public int AgeOldestCustomer { get; set; }
    }
}
