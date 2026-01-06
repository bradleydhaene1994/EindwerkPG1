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
    public class FirstNameTests
    {
        // ---------- VALID FIRST NAME ----------

        [Fact]
        public void Constructor_ValidValues_CreatesFirstName()
        {
            // Act
            FirstName firstName = new FirstName("John", 10, Gender.Male);

            // Assert
            Assert.Equal("John", firstName.Name);
            Assert.Equal(10, firstName.Frequency);
            Assert.Equal(Gender.Male, firstName.Gender);
        }

        // ---------- NAME VALIDATION ----------

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_InvalidName_ThrowsException(string name)
        {
            Assert.Throws<NameException>(() =>
                new FirstName(name, 10, Gender.Male));
        }

        // ---------- FREQUENCY VALIDATION ----------

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Constructor_InvalidFrequency_ThrowsException(int frequency)
        {
            Assert.Throws<NameException>(() =>
                new FirstName("John", frequency, Gender.Male));
        }

        [Fact]
        public void Constructor_NullFrequency_IsAllowed()
        {
            // Act
            FirstName firstName = new FirstName("John", null, Gender.Male);

            // Assert
            Assert.Null(firstName.Frequency);
        }

        // ---------- ID VALIDATION ----------

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(100)]
        public void Constructor_ValidId_SetsId(int id)
        {
            // Act
            FirstName firstName = new FirstName(id, "John", 10, Gender.Male);

            // Assert
            Assert.Equal(id, firstName.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-50)]
        public void Constructor_InvalidId_ThrowsException(int id)
        {
            Assert.Throws<NameException>(() =>
                new FirstName(id, "John", 10, Gender.Male));
        }

        // ---------- GENDER VALIDATION ----------

        [Fact]
        public void Constructor_InvalidGender_ThrowsException()
        {
            // Arrange
            Gender invalidGender = (Gender)999;

            // Act & Assert
            Assert.Throws<NameException>(() =>
                new FirstName("John", 10, invalidGender));
        }
    }
}
