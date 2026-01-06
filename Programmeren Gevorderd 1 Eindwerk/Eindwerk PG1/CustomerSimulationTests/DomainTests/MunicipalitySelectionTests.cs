using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationTests.DomainTests
{
    public class MunicipalitySelectionTests
    {
        [Fact]
        public void Constructor_ValidValues_CreatesMunicipalitySelection()
        {
            // Arrange
            Municipality municipality = new Municipality("Antwerp");

            // Act
            MunicipalitySelection selection =
                new MunicipalitySelection(municipality, 50, true);

            // Assert
            Assert.Equal(municipality, selection.Municipality);
            Assert.Equal(50, selection.Percentage);
            Assert.True(selection.IsSelected);
        }

        [Fact]
        public void Constructor_NullMunicipality_ThrowsException()
        {
            Assert.Throws<MunicipalityException>(() =>
                new MunicipalitySelection(null, 50, true));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(25)]
        [InlineData(50)]
        [InlineData(100)]
        public void Constructor_ValidPercentage_SetsPercentage(int percentage)
        {
            // Arrange
            Municipality municipality = new Municipality("Antwerp");

            // Act
            MunicipalitySelection selection =
                new MunicipalitySelection(municipality, percentage, true);

            // Assert
            Assert.Equal(percentage, selection.Percentage);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-50)]
        [InlineData(101)]
        [InlineData(1000)]
        public void Constructor_InvalidPercentage_ThrowsException(int percentage)
        {
            // Arrange
            Municipality municipality = new Municipality("Antwerp");

            // Act & Assert
            Assert.Throws<MunicipalityException>(() =>
                new MunicipalitySelection(municipality, percentage, true));
        }

        [Fact]
        public void IsSelected_CanBeTrueOrFalse()
        {
            // Arrange
            Municipality municipality = new Municipality("Antwerp");

            // Act
            MunicipalitySelection selection =
                new MunicipalitySelection(municipality, 50, false);

            // Assert
            Assert.False(selection.IsSelected);

            selection.IsSelected = true;
            Assert.True(selection.IsSelected);
        }
    }
}
