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
    public class NameRepository : INameRepository
    {
        private string _connectionstring;

        public NameRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public void UploadFirstName(IEnumerable<FirstName> firstNames, int countryId)
        {
            string SQL = "INSERT INTO FirstName(CountryID, Name, Gender, Frequency) " +
                         "OUTPUT inserted.ID VALUES(@CountryID, @Name, @Gender, @Frequency)";
            using(SqlConnection conn = new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction sqlTransaction = conn.BeginTransaction();
                cmd.Transaction = sqlTransaction;
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new SqlParameter("@CountryID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100));
                cmd.Parameters.Add(new SqlParameter("@Gender", SqlDbType.NVarChar, 100));
                cmd.Parameters.Add(new SqlParameter("@Frequency", SqlDbType.Int));
                int firstNameId;
                try
                {
                    foreach (FirstName firstName in firstNames)
                    {
                        cmd.Parameters["@CountryID"].Value = countryId;
                        cmd.Parameters["@Name"].Value = firstName.Name;
                        cmd.Parameters["@Gender"].Value = (object?)firstName.Gender ?? DBNull.Value;
                        cmd.Parameters["@Frequency"].Value = (object?)firstName.Frequency ?? DBNull.Value;
                        firstNameId = (int)cmd.ExecuteScalar();
                    }
                    sqlTransaction.Commit();
                }
                catch(Exception)
                {
                    sqlTransaction.Rollback();
                    throw;
                }
            }
        }
        public void UploadLastName(IEnumerable<LastName> lastNames, int countryId)
        {
            string SQL = "INSERT INTO LastName(CountryID, Name, Gender, Frequency) " +
                         "OUTPUT inserted.ID VALUES(@CountryID, @Name, @Gender, @Frequency)";
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction sqlTransaction = conn.BeginTransaction();
                cmd.Transaction = sqlTransaction;
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new SqlParameter("@CountryID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100));
                cmd.Parameters.Add(new SqlParameter("@Gender", SqlDbType.NVarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@Frequency", SqlDbType.Int));
                int lastNameID;
                try
                {
                    foreach(LastName lastName in lastNames)
                    {
                        cmd.Parameters["@CountryID"].Value = countryId;
                        cmd.Parameters["@Name"].Value = lastName.Name;
                        cmd.Parameters["@Gender"].Value = (object?)lastName.Gender ?? DBNull.Value;
                        cmd.Parameters["@Frequency"].Value = (object?)lastName.Frequency ?? DBNull.Value;
                        lastNameID = (int)cmd.ExecuteScalar();
                    }
                    sqlTransaction.Commit();
                }
                catch (Exception)
                {
                    sqlTransaction.Rollback();
                    throw;
                }
            }
        }
    }
}
