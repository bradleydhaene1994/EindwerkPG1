using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationTests.DomainTests
{
    public class AddressTests
    {
        [Fact]
        public void Constructor_ValidValues_CreatesAddress()
        {
            // Arrange
            Municipality municipality = new Municipality("Antwerp");
            string street = "Main Street";

            // Act
            Address address = new Address(municipality, street);

            // Assert
            Assert.Equal("Main Street", address.Street);
            Assert.Equal(municipality, address.Municipality);
        }
        [Fact]
        public void Constructor_EmptyStreet_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<AddressException>(() =>
                new Address(null, ""));
        }
        [Fact]
        public void Constructor_WhitespaceStreet_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<AddressException>(() =>
                new Address(null, "   "));
        }
        [Fact]
        public void Street_WithWhitespace_IsTrimmed()
        {
            // Act
            Address address = new Address(null, "  Baker Street  ");

            // Assert
            Assert.Equal("Baker Street", address.Street);
        }
        [Fact]
        public void Municipality_Null_IsAllowed()
        {
            // Act
            Address address = new Address(null, "Main Street");

            // Assert
            Assert.Null(address.Municipality);
        }
    }
}
