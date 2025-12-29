using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Domein
{
    public class CountryVersion
    {
        public CountryVersion(int year)
        {
            Year = year;
        }

        public CountryVersion(int id, int year) : this(id)
        {
            Year = year;
            Id = id;
        }

        public CountryVersion(int id, int year, Country country) : this(id, year)
        {
            Country = country;
            Id = id;
        }

        public int Id { get; set; }
        public int Year { get; set; }
        public Country Country { get; set; }
    }
}
