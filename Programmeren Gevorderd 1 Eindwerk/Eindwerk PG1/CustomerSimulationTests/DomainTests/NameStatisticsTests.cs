using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationTests.DomainTests
{
    public class NameStatisticsTests
    {

        [Theory]
        [InlineData("John", 0)]
        [InlineData("Anna", 1)]
        [InlineData("Michael", 25)]
        public void Constructor_ValidValues_CreatesNameStatistics(string name, int count)
        {
            // Act
            NameStatistics stats = new NameStatistics(name, count);

            // Assert
            Assert.Equal(name, stats.Name);
            Assert.Equal(count, stats.Count);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_InvalidName_ThrowsException(string name)
        {
            // Act & Assert
            Assert.Throws<SimulationException>(() =>
                new NameStatistics(name, 1));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(-100)]
        public void Constructor_NegativeCount_ThrowsException(int count)
        {
            // Act & Assert
            Assert.Throws<SimulationException>(() =>
                new NameStatistics("John", count));
        }
    }
}
