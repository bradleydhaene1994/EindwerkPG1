using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationTests.DomainTests
{
    public class MunicipalityTests
    {

        [Fact]
        public void Constructor_ValidName_CreatesMunicipality()
        {
            // Act
            Municipality municipality = new Municipality("Antwerp");

            // Assert
            Assert.Equal("Antwerp", municipality.Name);
        }

        [Fact]
        public void Constructor_NameWithWhitespace_IsTrimmed()
        {
            // Act
            Municipality municipality = new Municipality("  Antwerp  ");

            // Assert
            Assert.Equal("Antwerp", municipality.Name);
        }


        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_InvalidName_ThrowsException(string name)
        {
            Assert.Throws<MunicipalityException>(() =>
                new Municipality(name));
        }
    }
}
