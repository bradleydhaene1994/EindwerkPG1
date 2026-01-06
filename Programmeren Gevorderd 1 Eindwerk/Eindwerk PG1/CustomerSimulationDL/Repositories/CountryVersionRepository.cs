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
using Microsoft.Identity.Client;
using System.Transactions;

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
            const string SQLSelect = "SELECT ID FROM CountryVersion WHERE CountryID = @CountryID AND Year = @Year";

            const string SQLInsert = "INSERT INTO CountryVersion (CountryID, Year) VALUES (@CountryID, @Year); " +
                                     "SELECT SCOPE_IDENTITY();";

            using SqlConnection conn = new SqlConnection(_connectionstring);
            conn.Open();

            //Try to get existing
            using (SqlCommand selectCmd = conn.CreateCommand())
            {
                selectCmd.CommandText = SQLSelect;
                selectCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = countryId;
                selectCmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;

                object existing = selectCmd.ExecuteScalar();
                if (existing != null && existing != DBNull.Value)
                {
                    return Convert.ToInt32(existing);
                }
            }
            //Insert only if not found
            using (SqlCommand insertCmd = conn.CreateCommand())
            {
                insertCmd.CommandText = SQLInsert;
                insertCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = countryId;
                insertCmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;

                return Convert.ToInt32(insertCmd.ExecuteScalar());
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
            }
            return countryVersions;
        }
        public CountryVersion GetCountryVersionById(int countryVersionId)
        {
            string SQL = "SELECT cv.ID, cv.Year, c.ID AS CountryId, c.Name AS CountryName " +
                         "FROM countryVersion cv " +
                         "JOIN Country c on c.ID = cv.CountryID " +
                         "WHERE cv.ID = @CountryVersionId";

            using SqlConnection conn = new SqlConnection(_connectionstring);
            using SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = SQL;
            cmd.Parameters.Add("@CountryVersionId", SqlDbType.Int).Value = countryVersionId;

            conn.Open();

            using SqlDataReader reader = cmd.ExecuteReader();

            if(!reader.Read())
            {
                throw new InvalidOperationException($"CountryVersion with ID {countryVersionId} could not be found");
            }

            int id = reader.GetInt32(reader.GetOrdinal("ID"));
            int year = reader.GetInt32(reader.GetOrdinal("Year"));
            int countryId = reader.GetInt32(reader.GetOrdinal("CountryId"));
            string countryName = reader.GetString(reader.GetOrdinal("CountryName"));

            Country country = new Country(countryId, countryName);
            return new CountryVersion(id, year, country);
        }
    }
}
