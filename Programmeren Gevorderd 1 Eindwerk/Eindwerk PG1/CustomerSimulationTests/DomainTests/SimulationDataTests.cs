using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationTests.DomainTests
{
    public class SimulationDataTests
    {

        [Fact]
        public void Constructor_ValidValues_CreatesSimulationData()
        {
            // Arrange
            DateTime date = DateTime.Now.AddMinutes(-1);

            // Act
            SimulationData data = new SimulationData("Client A", date);

            // Assert
            Assert.Equal("Client A", data.Client);
            Assert.Equal(date, data.DateCreated);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_InvalidClient_ThrowsException(string client)
        {
            Assert.Throws<SimulationException>(() =>
                new SimulationData(client, DateTime.Now));
        }

        [Fact]
        public void Constructor_FutureDate_ThrowsException()
        {
            DateTime futureDate = DateTime.Now.AddMinutes(1);

            Assert.Throws<SimulationException>(() =>
                new SimulationData("Client A", futureDate));
        }
    }
}
