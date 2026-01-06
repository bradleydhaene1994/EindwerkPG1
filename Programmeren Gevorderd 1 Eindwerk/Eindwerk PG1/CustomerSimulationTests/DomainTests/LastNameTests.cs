using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Enumerations;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationTests.DomainTests
{
    public class LastNameTests
    {

        [Fact]
        public void Constructor_ValidValues_CreatesLastName()
        {
            // Act
            LastName lastName = new LastName("Smith", 15, Gender.Male);

            // Assert
            Assert.Equal("Smith", lastName.Name);
            Assert.Equal(15, lastName.Frequency);
            Assert.Equal(Gender.Male, lastName.Gender);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_InvalidName_ThrowsException(string name)
        {
            Assert.Throws<NameException>(() =>
                new LastName(name, 10, Gender.Male));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(-100)]
        public void Constructor_NegativeFrequency_ThrowsException(int frequency)
        {
            Assert.Throws<NameException>(() =>
                new LastName("Smith", frequency, Gender.Male));
        }

        [Fact]
        public void Constructor_NullFrequency_IsAllowed()
        {
            // Act
            LastName lastName = new LastName("Smith", null, Gender.Male);

            // Assert
            Assert.Null(lastName.Frequency);
        }

        [Fact]
        public void Constructor_ZeroFrequency_IsAllowed()
        {
            // Act
            LastName lastName = new LastName("Smith", 0, Gender.Male);

            // Assert
            Assert.Equal(0, lastName.Frequency);
        }

        [Fact]
        public void Gender_Null_IsAllowed()
        {
            // Act
            LastName lastName = new LastName("Smith", 10, null);

            // Assert
            Assert.Null(lastName.Gender);
        }

        [Fact]
        public void Constructor_InvalidGender_ThrowsException()
        {
            // Arrange
            Gender invalidGender = (Gender)999;

            // Act & Assert
            Assert.Throws<NameException>(() =>
                new LastName("Smith", 10, invalidGender));
        }
    }
}
