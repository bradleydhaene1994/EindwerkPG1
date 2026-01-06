using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationTests.DomainTests
{
    public class MunicipalityStatisticsTests
    {

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void Constructor_ValidCount_CreatesMunicipalityStatistics(int count)
        {
            // Arrange
            Municipality municipality = new Municipality("Antwerp");

            // Act
            MunicipalityStatistics stats =
                new MunicipalityStatistics(municipality, count);

            // Assert
            Assert.Equal(municipality, stats.Municipality);
            Assert.Equal(count, stats.Count);
        }

        [Fact]
        public void Constructor_NullMunicipality_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<MunicipalityException>(() =>
                new MunicipalityStatistics(null, 10));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(-100)]
        public void Constructor_NegativeCount_ThrowsException(int count)
        {
            // Arrange
            Municipality municipality = new Municipality("Antwerp");

            // Act & Assert
            Assert.Throws<MunicipalityException>(() =>
                new MunicipalityStatistics(municipality, count));
        }
    }
}
