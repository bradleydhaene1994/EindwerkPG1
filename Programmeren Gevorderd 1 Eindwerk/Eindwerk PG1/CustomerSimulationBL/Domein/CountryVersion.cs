using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationBL.Domein
{
    public class CountryVersion
    {
        public CountryVersion(int year)
        {
            Year = year;
        }

        public CountryVersion(int id, int year) : this(year)
        {
            Id = id;
        }

        public CountryVersion(int id, int year, Country country) : this(id, year)
        {
            Country = country;
        }
        private int _id;
        public int Id
        {
            get => _id;
            private set
            {
                _id = value;
            }
        }
        private int _year;
        public int Year
        {
            get => _year;
            set
            {
                if (value < 1900) throw new CountryException("Country version year has to be higher than 1900");
                else _year = value;
            }
        }
        private Country _country;
        public Country Country
        {
            get => _country;
            set
            {
                if (value == null) throw new CountryException("Country not found in country version.");
                else _country = value;
            }
        }
    }
}
