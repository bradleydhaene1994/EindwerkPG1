using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationBL.Domein
{
    public class SimulationStatistics
    {
        public SimulationStatistics(int totalCustomers, double averageAgeOnSimulationDate, double averageAgeOnCurrentDate, int ageYoungestCustomer, int ageOldestCustomer)
        {
            TotalCustomers = totalCustomers;
            AverageAgeOnSimulationDate = averageAgeOnSimulationDate;
            AverageAgeOnCurrentDate = averageAgeOnCurrentDate;
            AgeYoungestCustomer = ageYoungestCustomer;
            AgeOldestCustomer = ageOldestCustomer;
        }

        public SimulationStatistics(int id, int totalCustomers, double averageAgeOnSimulationDate, double averageAgeOnCurrentDate, int ageYoungestCustomer, int ageOldestCustomer)
        {
            Id = id;
            TotalCustomers = totalCustomers;
            AverageAgeOnSimulationDate = averageAgeOnSimulationDate;
            AverageAgeOnCurrentDate = averageAgeOnCurrentDate;
            AgeYoungestCustomer = ageYoungestCustomer;
            AgeOldestCustomer = ageOldestCustomer;
        }
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
            }
        }
        private int _totalCustomers;
        public int TotalCustomers
        {
            get => _totalCustomers;
            set
            {
                if (value < 0) throw new SimulationException("Totalcustomers < 0");
                else _totalCustomers = value;
            }
        }
        private double _averageAgeOnSimulationDate;
        public double AverageAgeOnSimulationDate
        {
            get => _averageAgeOnSimulationDate;
            set
            {
                if (value < 0) throw new SimulationException("Average age on simulation date is < 0");
                else _averageAgeOnSimulationDate = value;
            }
        }
        private double _averageAgeOnCurrentDate;
        public double AverageAgeOnCurrentDate
        {
            get => _averageAgeOnCurrentDate;
            set
            {
                if (value < 0) throw new SimulationException("Average age on current date is < 0");
                else _averageAgeOnCurrentDate = value;
            }
        }
        private int _ageYoungestCustomer;
        public int AgeYoungestCustomer
        {
            get => _ageYoungestCustomer;
            set
            {
                if (value < 0) throw new SimulationException("Age of youngest customer cannot be lower than 0");
                if (_ageOldestCustomer > 0 && value > _ageOldestCustomer) throw new SimulationException("Age of youngest customer cannot be greater than age of oldest customer.");
                else _ageYoungestCustomer = value;
            }
        }
        private int _ageOldestCustomer;
        public int AgeOldestCustomer
        {
            get => _ageOldestCustomer;
            set
            {
                if (value < _ageYoungestCustomer) throw new SimulationException("Age oldest customer cannot be smaller than age youngest customer.");
                else _ageOldestCustomer = value;
            }
        }
    }
}
