using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CustomerSimulationDL.Repositories
{
    public class MunicipalityRepository : IMunicipalityRepository
    {
        private string _connectionstring;

        public MunicipalityRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public void UploadMunicipality(IEnumerable<Municipality> municipalities, CountryVersion countryVersion)
        {
            string SQL = "INSERT INTO Municipality(CountryVersionID, Name) " +
                         "OUTPUT inserted.ID VALUES(@CountryVersionID, @Name)";

            using(SqlConnection conn = new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                cmd.CommandText = SQL;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("@CountryVersionID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100));
                int municipalityId;
                try
                {
                    foreach(Municipality m in municipalities)
                    {
                        cmd.Parameters["@CountryVersionID"].Value = countryVersion.Id;
                        cmd.Parameters["@Name"].Value = m.Name;
                        municipalityId = (int)cmd.ExecuteScalar();
                    }
                    tran.Commit();
                }
                catch(Exception)
                {
                    tran.Rollback();
                }
            }
        }
    }
}
