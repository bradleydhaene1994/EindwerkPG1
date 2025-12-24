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
        }

        public int Id { get; set; }
        public int Year { get; set; }
    }
}
