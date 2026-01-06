using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationTests.DomainTests
{
    public class SimulationStatisticsTests
    {
        [Fact]
        public void Constructor_ValidValues_CreatesSimulationStatistics()
        {
            // Act
            SimulationStatistics stats = new SimulationStatistics(
                totalCustomers: 10,
                averageAgeOnSimulationDate: 30,
                averageAgeOnCurrentDate: 32,
                ageYoungestCustomer: 18,
                ageOldestCustomer: 65);

            // Assert
            Assert.Equal(10, stats.TotalCustomers);
            Assert.Equal(30, stats.AverageAgeOnSimulationDate);
            Assert.Equal(32, stats.AverageAgeOnCurrentDate);
            Assert.Equal(18, stats.AgeYoungestCustomer);
            Assert.Equal(65, stats.AgeOldestCustomer);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(100)]
        public void Constructor_ValidTotalCustomers_SetsValue(int totalCustomers)
        {
            SimulationStatistics stats = new SimulationStatistics(
                totalCustomers,
                30,
                32,
                18,
                65);

            Assert.Equal(totalCustomers, stats.TotalCustomers);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        public void Constructor_NegativeTotalCustomers_ThrowsException(int totalCustomers)
        {
            Assert.Throws<SimulationException>(() =>
                new SimulationStatistics(
                    totalCustomers,
                    30,
                    32,
                    18,
                    65));
        }
        [Theory]
        [InlineData(0)]
        [InlineData(25.5)]
        [InlineData(100)]
        public void Constructor_ValidAverageAges_SetsValues(double avgAge)
        {
            SimulationStatistics stats = new SimulationStatistics(
                10,
                avgAge,
                avgAge,
                18,
                65);

            Assert.Equal(avgAge, stats.AverageAgeOnSimulationDate);
            Assert.Equal(avgAge, stats.AverageAgeOnCurrentDate);
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(-5)]
        public void Constructor_NegativeAverageAgeOnSimulationDate_ThrowsException(double avgAge)
        {
            Assert.Throws<SimulationException>(() =>
                new SimulationStatistics(
                    10,
                    avgAge,
                    30,
                    18,
                    65));
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(-5)]
        public void Constructor_NegativeAverageAgeOnCurrentDate_ThrowsException(double avgAge)
        {
            Assert.Throws<SimulationException>(() =>
                new SimulationStatistics(
                    10,
                    30,
                    avgAge,
                    18,
                    65));
        }

        [Fact]
        public void Constructor_YoungestAgeGreaterThanOldest_ThrowsException()
        {
            Assert.Throws<SimulationException>(() =>
                new SimulationStatistics(
                    10,
                    30,
                    32,
                    ageYoungestCustomer: 70,
                    ageOldestCustomer: 65));
        }

        [Fact]
        public void Constructor_NegativeYoungestAge_ThrowsException()
        {
            Assert.Throws<SimulationException>(() =>
                new SimulationStatistics(
                    10,
                    30,
                    32,
                    ageYoungestCustomer: -1,
                    ageOldestCustomer: 65));
        }

        [Fact]
        public void Constructor_OldestAgeLowerThanYoungest_ThrowsException()
        {
            Assert.Throws<SimulationException>(() =>
                new SimulationStatistics(
                    10,
                    30,
                    32,
                    ageYoungestCustomer: 30,
                    ageOldestCustomer: 20));
        }
    }
}
