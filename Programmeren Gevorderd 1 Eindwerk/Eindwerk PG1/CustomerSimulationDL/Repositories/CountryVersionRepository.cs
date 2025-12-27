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

        public void UploadCountryVersion(CountryVersion countryVersion, int countryId)
        {
            string SQL = "INSERT INTO CountryVersion(CountryID, Year) " +
                         "OUTPUT inserted.ID VALUES(@CountryID, @Year)";

            using(SqlConnection conn = new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                cmd.CommandText = SQL;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("@CountryID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Year", SqlDbType.Int));

                try
                {
                    cmd.Parameters["@CountryID"].Value = countryId;
                    cmd.Parameters["@Year"].Value = countryVersion.Year;
                    countryVersion.Id = (int)cmd.ExecuteScalar();

                    tran.Commit();
                }
                catch(Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
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
    }
}
