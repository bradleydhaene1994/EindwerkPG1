using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Data;
using System.Data;
using CustomerSimulationBL.Domein;

namespace CustomerSimulationDL.Repositories
{
    public class CountryVersionRepository : ICountryVersionRepository
    {
        private string _connectionstring;

        public CountryVersionRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public int GetOrUploadCountryVersion(int countryId, int year)
        {
            string SQLInsert = "INSERT INTO CountryVersion(CountryID, Year) " +
                               "OUTPUT inserted.ID VALUES(@CountryID, @Year)";

            string SQLSelect = "SELECT ID FROM CountryVersion " +
                               "WHERE CountryID = @CountryID AND Year = @Year";

            using SqlConnection conn = new SqlConnection(_connectionstring);
            using SqlCommand cmd = new SqlCommand(SQLInsert, conn);

            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = countryId;
            cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;

            conn.Open();

            try
            {
                return (int)cmd.ExecuteScalar();
            }
            catch(SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                cmd.CommandText = SQLSelect;
                return (int)cmd.ExecuteScalar();
            }
        }
        public List<Country> GetAllCountries()
        {
            List<Country> countries = new List<Country>();

            string SQL = "SELECT * " +
                         "FROM Country";

            using(SqlConnection con = new SqlConnection(_connectionstring))
            using (SqlCommand cmd = con.CreateCommand())
            {
                con.Open();
                cmd.CommandText = SQL;

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int Id = (int)reader["Id"];
                        string Name = (string)reader["Name"];

                        Country country = new Country(Id, Name);

                        countries.Add(country);
                    }
                }
            }
            return countries;
        }
        public List<CountryVersion> GetAllCountryVersions()
        {
            List<CountryVersion> countryVersions = new List<CountryVersion>();

            string SQL = "SELECT cv.ID as Id, cv.CountryID, cv.Year, c.Name AS CountryName " +
                         "FROM CountryVersion cv " +
                         "JOIN Country c ON c.ID = cv.CountryID " +
                         "ORDER BY c.Name, cv.Year";

            using (SqlConnection conn = new SqlConnection(_connectionstring))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("Id"));
                        int countryId = reader.GetInt32(reader.GetOrdinal("CountryID"));
                        int year = reader.GetInt32(reader.GetOrdinal("Year"));
                        string countryName = reader.GetString(reader.GetOrdinal("CountryName"));

                        Country country = new Country(countryId, countryName);

                        CountryVersion countryVersion = new CountryVersion(id, year, country);

                        countryVersions.Add(countryVersion);
                    }
                }
                return countryVersions;
            }
        }
    }
}
