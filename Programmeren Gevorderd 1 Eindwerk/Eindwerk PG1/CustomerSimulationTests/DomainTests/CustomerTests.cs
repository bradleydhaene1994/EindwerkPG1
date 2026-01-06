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
    public  class CustomerTests
    {
        [Fact]
        public void Constructor_ValidValues_CreatesCustomer()
        {
            // Act
            Customer customer = new Customer(
                "John",
                "Doe",
                "Antwerp",
                "Main Street",
                new DateTime(1990, 1, 1),
                "12A",
                Gender.Male);

            // Assert
            Assert.Equal("John", customer.FirstName);
            Assert.Equal("Doe", customer.LastName);
            Assert.Equal("Antwerp", customer.Municipality);
            Assert.Equal("Main Street", customer.Street);
            Assert.Equal("12A", customer.HouseNumber);
            Assert.Equal(Gender.Male, customer.Gender);
        }
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_InvalidFirstName_ThrowsException(string firstName)
        {
            Assert.Throws<CustomerException>(() =>
                new Customer(
                    firstName,
                    "Doe",
                    "Antwerp",
                    "Main Street",
                    new DateTime(1990, 1, 1),
                    "12",
                    Gender.Male));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_InvalidLastName_ThrowsException(string lastName)
        {
            Assert.Throws<CustomerException>(() =>
                new Customer(
                    "John",
                    lastName,
                    "Antwerp",
                    "Main Street",
                    new DateTime(1990, 1, 1),
                    "12",
                    Gender.Male));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_InvalidMunicipality_ThrowsException(string municipality)
        {
            Assert.Throws<CustomerException>(() =>
                new Customer(
                    "John",
                    "Doe",
                    municipality,
                    "Main Street",
                    new DateTime(1990, 1, 1),
                    "12",
                    Gender.Male));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_InvalidStreet_ThrowsException(string street)
        {
            Assert.Throws<CustomerException>(() =>
                new Customer(
                    "John",
                    "Doe",
                    "Antwerp",
                    street,
                    new DateTime(1990, 1, 1),
                    "12",
                    Gender.Male));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_InvalidHouseNumber_ThrowsException(string houseNumber)
        {
            Assert.Throws<CustomerException>(() =>
                new Customer(
                    "John",
                    "Doe",
                    "Antwerp",
                    "Main Street",
                    new DateTime(1990, 1, 1),
                    houseNumber,
                    Gender.Male));
        }

        [Fact]
        public void Constructor_BirthDateInFuture_ThrowsException()
        {
            Assert.Throws<CustomerException>(() =>
                new Customer(
                    "John",
                    "Doe",
                    "Antwerp",
                    "Main Street",
                    DateTime.Today.AddDays(1),
                    "12",
                    Gender.Male));
        }

        [Fact]
        public void Gender_Null_IsAllowed()
        {
            Customer customer = new Customer(
                1,
                "John",
                "Doe",
                "Antwerp",
                "Main Street",
                new DateTime(1990, 1, 1),
                "12",
                null);

            Assert.Null(customer.Gender);
        }
    }
}
