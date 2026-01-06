using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationTests.DomainTests
{
    public class SimulationStatisticsResultTests
    {

        private SimulationStatistics CreateValidGeneralStatistics()
        {
            return new SimulationStatistics(
                totalCustomers: 10,
                averageAgeOnSimulationDate: 30,
                averageAgeOnCurrentDate: 32,
                ageYoungestCustomer: 18,
                ageOldestCustomer: 65);
        }

        private List<MunicipalityStatistics> CreateValidMunicipalityStatistics()
        {
            return new List<MunicipalityStatistics>
            {
                new MunicipalityStatistics(new Municipality("Antwerp"), 10)
            };
        }

        private List<NameStatistics> CreateValidNameStatistics()
        {
            return new List<NameStatistics>
            {
                new NameStatistics("John", 5)
            };
        }

        [Fact]
        public void Constructor_ValidValues_CreatesSimulationStatisticsResult()
        {
            // Arrange
            SimulationStatistics general = CreateValidGeneralStatistics();
            var customersPerMunicipality = CreateValidMunicipalityStatistics();
            var streetsPerMunicipality = CreateValidMunicipalityStatistics();
            var maleNames = CreateValidNameStatistics();
            var femaleNames = CreateValidNameStatistics();
            var lastNames = CreateValidNameStatistics();

            // Act
            SimulationStatisticsResult result = new SimulationStatisticsResult(
                general,
                customersPerMunicipality,
                streetsPerMunicipality,
                maleNames,
                femaleNames,
                lastNames);

            // Assert
            Assert.Equal(general, result.General);
            Assert.Equal(customersPerMunicipality, result.CustomersPerMunicipality);
            Assert.Equal(streetsPerMunicipality, result.StreetsPerMunicipality);
            Assert.Equal(maleNames, result.MaleNames);
            Assert.Equal(femaleNames, result.FemaleNames);
            Assert.Equal(lastNames, result.LastNames);
        }

        [Fact]
        public void Constructor_NullGeneral_ThrowsException()
        {
            Assert.Throws<SimulationException>(() =>
                new SimulationStatisticsResult(
                    null,
                    CreateValidMunicipalityStatistics(),
                    CreateValidMunicipalityStatistics(),
                    CreateValidNameStatistics(),
                    CreateValidNameStatistics(),
                    CreateValidNameStatistics()));
        }

        [Fact]
        public void Constructor_NullCustomersPerMunicipality_ThrowsException()
        {
            Assert.Throws<SimulationException>(() =>
                new SimulationStatisticsResult(
                    CreateValidGeneralStatistics(),
                    null,
                    CreateValidMunicipalityStatistics(),
                    CreateValidNameStatistics(),
                    CreateValidNameStatistics(),
                    CreateValidNameStatistics()));
        }

        [Fact]
        public void Constructor_NullStreetsPerMunicipality_ThrowsException()
        {
            Assert.Throws<SimulationException>(() =>
                new SimulationStatisticsResult(
                    CreateValidGeneralStatistics(),
                    CreateValidMunicipalityStatistics(),
                    null,
                    CreateValidNameStatistics(),
                    CreateValidNameStatistics(),
                    CreateValidNameStatistics()));
        }

        [Fact]
        public void Constructor_NullMaleNames_ThrowsException()
        {
            Assert.Throws<SimulationException>(() =>
                new SimulationStatisticsResult(
                    CreateValidGeneralStatistics(),
                    CreateValidMunicipalityStatistics(),
                    CreateValidMunicipalityStatistics(),
                    null,
                    CreateValidNameStatistics(),
                    CreateValidNameStatistics()));
        }

        [Fact]
        public void Constructor_NullFemaleNames_ThrowsException()
        {
            Assert.Throws<SimulationException>(() =>
                new SimulationStatisticsResult(
                    CreateValidGeneralStatistics(),
                    CreateValidMunicipalityStatistics(),
                    CreateValidMunicipalityStatistics(),
                    CreateValidNameStatistics(),
                    null,
                    CreateValidNameStatistics()));
        }

        [Fact]
        public void Constructor_NullLastNames_ThrowsException()
        {
            Assert.Throws<SimulationException>(() =>
                new SimulationStatisticsResult(
                    CreateValidGeneralStatistics(),
                    CreateValidMunicipalityStatistics(),
                    CreateValidMunicipalityStatistics(),
                    CreateValidNameStatistics(),
                    CreateValidNameStatistics(),
                    null));
        }
    }
}
