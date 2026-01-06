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
        // ---------- VALID MUNICIPALITY ----------

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

        // ---------- NAME VALIDATION ----------

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_InvalidName_ThrowsException(string name)
        {
            Assert.Throws<MunicipalityException>(() =>
                new Municipality(name));
        }

        // ---------- ID VALIDATION ----------

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(100)]
        public void Constructor_ValidId_SetsId(int id)
        {
            // Act
            Municipality municipality = new Municipality(id, "Antwerp");

            // Assert
            Assert.Equal(id, municipality.Id);
            Assert.Equal("Antwerp", municipality.Name);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-50)]
        public void Constructor_InvalidId_ThrowsException(int id)
        {
            Assert.Throws<MunicipalityException>(() =>
                new Municipality(id, "Antwerp"));
        }
    }
}
