using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationTests.DomainTests
{
    public class SimulationSettingsTests
    {
        // ---------- HELPERS ----------

        private List<MunicipalitySelection> CreateValidMunicipalities()
        {
            return new List<MunicipalitySelection>
            {
                new MunicipalitySelection(new Municipality("Antwerp"), 100, true)
            };
        }

        private SimulationSettings CreateValidSettings()
        {
            return new SimulationSettings(
                selectedMunicipalities: CreateValidMunicipalities(),
                totalCustomers: 10,
                minAge: 18,
                maxAge: 65,
                minNumber: 1,
                maxNumber: 100,
                hasLetters: true,
                percentageLetters: 50);
        }

        // ---------- VALID SETTINGS ----------

        [Fact]
        public void Constructor_ValidValues_CreatesSimulationSettings()
        {
            // Act
            SimulationSettings settings = CreateValidSettings();

            // Assert
            Assert.Equal(10, settings.TotalCustomers);
            Assert.Equal(18, settings.MinAge);
            Assert.Equal(65, settings.MaxAge);
            Assert.Equal(1, settings.MinNumber);
            Assert.Equal(100, settings.MaxNumber);
            Assert.True(settings.HasLetters);
            Assert.Equal(50, settings.PercentageLetters);
            Assert.NotNull(settings.SelectedMunicipalities);
        }

        // ---------- ID VALIDATION ----------

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(100)]
        public void Constructor_ValidId_SetsId(int id)
        {
            SimulationSettings settings = new SimulationSettings(
                id,
                CreateValidMunicipalities(),
                10,
                18,
                65,
                1,
                100,
                true,
                50);

            Assert.Equal(id, settings.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-50)]
        public void Constructor_InvalidId_ThrowsException(int id)
        {
            Assert.Throws<SimulationException>(() =>
                new SimulationSettings(
                    id,
                    CreateValidMunicipalities(),
                    10,
                    18,
                    65,
                    1,
                    100,
                    true,
                    50));
        }

        // ---------- SELECTED MUNICIPALITIES ----------

        [Fact]
        public void Constructor_SelectedMunicipalitiesContainingNull_ThrowsException()
        {
            var listWithNull = new List<MunicipalitySelection> { null };

            Assert.Throws<SimulationException>(() =>
                new SimulationSettings(
                    listWithNull,
                    10,
                    18,
                    65,
                    1,
                    100,
                    true,
                    50));
        }

        // ---------- TOTAL CUSTOMERS ----------

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Constructor_InvalidTotalCustomers_ThrowsException(int totalCustomers)
        {
            Assert.Throws<SimulationException>(() =>
                new SimulationSettings(
                    CreateValidMunicipalities(),
                    totalCustomers,
                    18,
                    65,
                    1,
                    100,
                    true,
                    50));
        }

        // ---------- AGE VALIDATION ----------

        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        public void Constructor_InvalidMinAge_ThrowsException(int minAge)
        {
            Assert.Throws<SimulationException>(() =>
                new SimulationSettings(
                    CreateValidMunicipalities(),
                    10,
                    minAge,
                    65,
                    1,
                    100,
                    true,
                    50));
        }

        [Theory]
        [InlineData(17)]
        [InlineData(10)]
        public void Constructor_MaxAgeLowerThanMinAge_ThrowsException(int maxAge)
        {
            Assert.Throws<SimulationException>(() =>
                new SimulationSettings(
                    CreateValidMunicipalities(),
                    10,
                    18,
                    maxAge,
                    1,
                    100,
                    true,
                    50));
        }

        // ---------- HOUSE NUMBER VALIDATION ----------

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Constructor_InvalidMinHouseNumber_ThrowsException(int minNumber)
        {
            Assert.Throws<SimulationException>(() =>
                new SimulationSettings(
                    CreateValidMunicipalities(),
                    10,
                    18,
                    65,
                    minNumber,
                    100,
                    true,
                    50));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0)]
        [InlineData(50)]
        public void Constructor_MaxHouseNumberLowerThanMin_ThrowsException(int maxNumber)
        {
            Assert.Throws<SimulationException>(() =>
                new SimulationSettings(
                    CreateValidMunicipalities(),
                    10,
                    18,
                    65,
                    100,
                    maxNumber,
                    true,
                    50));
        }

        // ---------- PERCENTAGE LETTERS ----------

        [Theory]
        [InlineData(0)]
        [InlineData(50)]
        [InlineData(100)]
        public void Constructor_ValidPercentageLetters_SetsValue(int percentage)
        {
            SimulationSettings settings = new SimulationSettings(
                CreateValidMunicipalities(),
                10,
                18,
                65,
                1,
                100,
                true,
                percentage);

            Assert.Equal(percentage, settings.PercentageLetters);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(101)]
        [InlineData(1000)]
        public void Constructor_InvalidPercentageLetters_ThrowsException(int percentage)
        {
            Assert.Throws<SimulationException>(() =>
                new SimulationSettings(
                    CreateValidMunicipalities(),
                    10,
                    18,
                    65,
                    1,
                    100,
                    true,
                    percentage));
        }

        // ---------- SETSELECTEDMUNICIPALITIES ----------

        [Fact]
        public void SetSelectedMunicipalities_WithValidList_UpdatesValue()
        {
            SimulationSettings settings = CreateValidSettings();
            var newList = CreateValidMunicipalities();

            settings.SetSelectedMunicipalities(newList);

            Assert.Equal(newList, settings.SelectedMunicipalities);
        }

        [Fact]
        public void SetSelectedMunicipalities_WithNullItem_ThrowsException()
        {
            SimulationSettings settings = CreateValidSettings();

            Assert.Throws<SimulationException>(() =>
                settings.SetSelectedMunicipalities(new List<MunicipalitySelection> { null }));
        }
    }
}
