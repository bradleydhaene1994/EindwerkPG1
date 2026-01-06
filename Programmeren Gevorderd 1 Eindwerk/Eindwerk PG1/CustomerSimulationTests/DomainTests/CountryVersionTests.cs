using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationTests.DomainTests
{
    public class CountryVersionTests
    {
        [Theory]
        [InlineData(1900)]
        [InlineData(1950)]
        [InlineData(2024)]
        public void Constructor_ValidYear_CreatesCountryVersion(int year)
        {
            // Act
            CountryVersion version = new CountryVersion(year);

            // Assert
            Assert.Equal(year, version.Year);
        }

        [Theory]
        [InlineData(1899)]
        [InlineData(1800)]
        [InlineData(0)]
        public void Constructor_InvalidYear_ThrowsException(int year)
        {
            // Act & Assert
            Assert.Throws<CountryException>(() =>
                new CountryVersion(year));
        }

        [Fact]
        public void Constructor_WithCountry_SetsCountry()
        {
            // Arrange
            Country country = new Country("Belgium");

            // Act
            CountryVersion version = new CountryVersion(1, 2020, country);

            // Assert
            Assert.Equal(country, version.Country);
        }

        [Fact]
        public void SettingCountry_Null_ThrowsException()
        {
            // Arrange
            CountryVersion version = new CountryVersion(2020);

            // Act & Assert
            Assert.Throws<CountryException>(() =>
                version.Country = null);
        }
    }
}
