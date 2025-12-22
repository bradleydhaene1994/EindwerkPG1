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

        public void UploadCountryVersion(Country country, int version)
        {
            string SQL = "INSERT INTO Country_Version(CountryID, Version) " +
                         "OUTPUT inserted.ID VALUES(@CountryID, @Version)";

            using(SqlConnection conn = new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                cmd.CommandText = SQL;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("@CountryID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Version", SqlDbType.Int));
                int countryVersionId;

                try
                {
                    cmd.Parameters["@CountryID"].Value = country.Id;
                    cmd.Parameters["@Version"].Value = version;
                    countryVersionId = (int)cmd.ExecuteScalar();

                    tran.Commit();
                }
                catch(Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
    }
}
