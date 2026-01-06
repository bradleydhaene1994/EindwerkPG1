using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationTests.DomainTests
{
    public class SimulationExportTests
    {
        private SimulationData CreateValidSimulationData()
        {
            return new SimulationData("Client A", DateTime.Now.AddMinutes(-1));
        }

        private SimulationSettings CreateValidSimulationSettings()
        {
            return new SimulationSettings(
                null,
                totalCustomers: 10,
                minAge: 18,
                maxAge: 65,
                minNumber: 1,
                maxNumber: 100,
                hasLetters: true,
                percentageLetters: 50);
        }

        private SimulationStatisticsResult CreateValidStatisticsResult()
        {
            var general = new SimulationStatistics(
                totalCustomers: 10,
                averageAgeOnSimulationDate: 30,
                averageAgeOnCurrentDate: 32,
                ageYoungestCustomer: 18,
                ageOldestCustomer: 65);

            return new SimulationStatisticsResult(
                general,
                customersPerMunicipality: new(),
                streetsPerMunicipality: new(),
                maleNames: new(),
                femaleNames: new(),
                lastNames: new());
        }


        [Fact]
        public void Constructor_ValidValues_CreatesSimulationExport()
        {
            // Arrange
            SimulationData data = CreateValidSimulationData();
            SimulationSettings settings = CreateValidSimulationSettings();
            SimulationStatisticsResult stats = CreateValidStatisticsResult();

            // Act
            SimulationExport export = new SimulationExport(data, settings, stats);

            // Assert
            Assert.Equal(data, export.SimulationData);
            Assert.Equal(settings, export.SimulationSettings);
            Assert.Equal(stats, export.SimulationStatisticsResult);
        }

        [Fact]
        public void Constructor_NullSimulationData_ThrowsException()
        {
            SimulationSettings settings = CreateValidSimulationSettings();
            SimulationStatisticsResult stats = CreateValidStatisticsResult();

            Assert.Throws<SimulationException>(() =>
                new SimulationExport(null, settings, stats));
        }

        [Fact]
        public void Constructor_NullSimulationSettings_ThrowsException()
        {
            SimulationData data = CreateValidSimulationData();
            SimulationStatisticsResult stats = CreateValidStatisticsResult();

            Assert.Throws<SimulationException>(() =>
                new SimulationExport(data, null, stats));
        }

        [Fact]
        public void Constructor_NullSimulationStatisticsResult_ThrowsException()
        {
            SimulationData data = CreateValidSimulationData();
            SimulationSettings settings = CreateValidSimulationSettings();

            Assert.Throws<SimulationException>(() =>
                new SimulationExport(data, settings, null));
        }
    }
}
