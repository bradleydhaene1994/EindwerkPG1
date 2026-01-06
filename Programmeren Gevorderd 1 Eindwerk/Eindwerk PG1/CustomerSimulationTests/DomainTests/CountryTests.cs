using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationTests.DomainTests
{
    public class CountryTests
    {
        [Fact]
        public void Constructor_ValidName_CreatesCountry()
        {
            // Act
            Country country = new Country("Belgium");

            // Assert
            Assert.Equal("Belgium", country.Name);
        }

        [Fact]
        public void Constructor_NameWithWhitespace_IsTrimmed()
        {
            // Act
            Country country = new Country("  Belgium  ");

            // Assert
            Assert.Equal("Belgium", country.Name);
        }

        [Fact]
        public void Constructor_EmptyName_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<CountryException>(() =>
                new Country(""));
        }

        [Fact]
        public void Constructor_WhitespaceName_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<CountryException>(() =>
                new Country("   "));
        }

        [Fact]
        public void Constructor_WithValidId_SetsId()
        {
            // Act
            Country country = new Country(1, "Belgium");

            // Assert
            Assert.Equal(1, country.Id);
            Assert.Equal("Belgium", country.Name);
        }

        [Fact]
        public void Constructor_IdLessOrEqualZero_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<CountryException>(() =>
                new Country(0, "Belgium"));
        }
    }
}
