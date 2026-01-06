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
        // ---------- VALID LAST NAME ----------

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

        // ---------- NAME VALIDATION ----------

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_InvalidName_ThrowsException(string name)
        {
            Assert.Throws<NameException>(() =>
                new LastName(name, 10, Gender.Male));
        }

        // ---------- FREQUENCY VALIDATION ----------

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

        // ---------- ID VALIDATION ----------

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(100)]
        public void Constructor_ValidId_SetsId(int id)
        {
            // Act
            LastName lastName = new LastName(id, "Smith", 10, Gender.Male);

            // Assert
            Assert.Equal(id, lastName.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-50)]
        public void Constructor_InvalidId_ThrowsException(int id)
        {
            Assert.Throws<NameException>(() =>
                new LastName(id, "Smith", 10, Gender.Male));
        }

        // ---------- GENDER VALIDATION ----------

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
