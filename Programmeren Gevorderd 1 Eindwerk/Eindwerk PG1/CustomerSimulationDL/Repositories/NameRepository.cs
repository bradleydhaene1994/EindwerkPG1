using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using CustomerSimulationBL.Enumerations;
using System.Diagnostics;

namespace CustomerSimulationDL.Repositories
{
    public class NameRepository : INameRepository
    {
        private string _connectionstring;

        public NameRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public void UploadFirstName(IEnumerable<FirstName> firstNames, int countryVersionId, IProgress<int> progress)
        {
            string SQL = "INSERT INTO FirstName(CountryVersionID, Name, Gender, Frequency) " +
                         "OUTPUT inserted.ID VALUES(@CountryVersionID, @Name, @Gender, @Frequency)";
            using(SqlConnection conn = new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction sqlTransaction = conn.BeginTransaction();

                var firstNameList = firstNames.ToList();
                int total = firstNames.Count();
                int processed = 0;

                const int REPORT_EVERY = 100;

                cmd.Transaction = sqlTransaction;
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new SqlParameter("@CountryVersionID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100));
                cmd.Parameters.Add(new SqlParameter("@Gender", SqlDbType.NVarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@Frequency", SqlDbType.Int));
                int firstNameId;
                try
                {
                    foreach (FirstName firstName in firstNames)
                    {
                        cmd.Parameters["@CountryVersionID"].Value = countryVersionId;
                        cmd.Parameters["@Name"].Value = firstName.Name;
                        cmd.Parameters["@Gender"].Value = (object?)firstName.Gender ?? DBNull.Value;
                        cmd.Parameters["@Frequency"].Value = (object?)firstName.Frequency ?? DBNull.Value;
                        firstNameId = (int)cmd.ExecuteScalar();

                        processed++;

                        if (processed % REPORT_EVERY == 0 || processed == total)
                        {
                            int percent = (int)((processed / (double)total) * 100);
                            progress?.Report(percent);
                        }
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
        public void UploadLastName(IEnumerable<LastName> lastNames, int countryVersionId, IProgress<int> progress)
        {
            string SQL = "INSERT INTO LastName(CountryVersionID, Name, Gender, Frequency) " +
                         "OUTPUT inserted.ID VALUES(@CountryVersionID, @Name, @Gender, @Frequency)";

            using (SqlConnection conn = new SqlConnection(_connectionstring))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction sqlTransaction = conn.BeginTransaction();

                var lastNameList = lastNames.ToList();
                int total = lastNames.Count();
                int processed = 0;

                const int REPORT_EVERY = 100;

                cmd.Transaction = sqlTransaction;
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new SqlParameter("@CountryVersionID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100));
                cmd.Parameters.Add(new SqlParameter("@Gender", SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@Frequency", SqlDbType.Int));
                int lastNameID;
                try
                {
                    foreach(LastName lastName in lastNames)
                    {
                        cmd.Parameters["@CountryVersionID"].Value = countryVersionId;
                        cmd.Parameters["@Name"].Value = lastName.Name;
                        cmd.Parameters["@Gender"].Value = (object?)lastName.Gender ?? DBNull.Value;
                        cmd.Parameters["@Frequency"].Value = (object?)lastName.Frequency ?? DBNull.Value;
                        lastNameID = (int)cmd.ExecuteScalar();

                        processed++;

                        if (processed % REPORT_EVERY == 0 || processed == total)
                        {
                            int percent = (int)((processed / (double)total) * 100);
                            progress?.Report(percent);
                        }
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

        public List<FirstName> GetFirstNamesByCountryVersionID(int countryVersionID)
        {
            List<FirstName> firstNames = new List<FirstName>();

            string SQL = "SELECT fn.ID, fn.Name, fn.Gender, fn.Frequency " +
                         "FROM FirstName fn " +
                         "WHERE CountryVersionID = @CountryVersionID";

            using(SqlConnection conn = new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@CountryVersionID", countryVersionID);
                cmd.CommandText = SQL;

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("ID"));
                        string name = reader.GetString(reader.GetOrdinal("Name"));
                        string genderAsString = reader.GetString(reader.GetOrdinal("Gender"));
                        Gender gender = (Gender)Enum.Parse(typeof(Gender), genderAsString);
                        int? frequency = reader.IsDBNull(reader.GetOrdinal("Frequency")) ? null : reader.GetInt32(reader.GetOrdinal("Frequency"));

                        FirstName firstName = new FirstName(id, name, frequency, gender);

                        firstNames.Add(firstName);
                    }
                }
            }
            return firstNames;
        }

        public List<LastName> GetLastNamesByCountryVersionID(int countryVersionID)
        {
            List<LastName> lastNames = new List<LastName>();

            string SQL = "SELECT ln.ID, ln.Name, ln.Gender, ln.Frequency " +
                         "FROM LastName ln " +
                         "WHERE CountryVersionID = @CountryVersionID";

            using(SqlConnection conn = new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@CountryVersionID", countryVersionID);
                cmd.CommandText = SQL;

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("ID"));
                        string name = reader.GetString(reader.GetOrdinal("Name"));
                        int genderIndex = reader.GetOrdinal("Gender");
                        Gender gender;
                        if(!reader.IsDBNull(genderIndex))
                        {
                            string genderAsString = reader.GetString(genderIndex);
                            gender = (Gender)Enum.Parse(typeof(Gender), genderAsString, ignoreCase: true);
                        }
                        else
                        {
                            gender = Gender.Unknown;
                        }
                        int? frequency = reader.IsDBNull(reader.GetOrdinal("Frequency")) ? null : reader.GetInt32(reader.GetOrdinal("Frequency"));

                        LastName lastName = new LastName(id, name, frequency, gender);

                        lastNames.Add(lastName);
                    }
                }
            }
            return lastNames;
        }
        public Gender GetGenderByFirstName(string firstName)
        {
            string sql = "SELECT Gender FROM FirstName WHERE Name = @Name";

            using SqlConnection conn = new(_connectionstring);
            using SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = sql;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = firstName;

            conn.Open();

            object? result = cmd.ExecuteScalar();

            if (result == null || result == DBNull.Value)
            {
                return Gender.Unknown;
            }

            return Enum.Parse<Gender>(result.ToString()!, true);
        }
        /*public bool HasFirstNames(int countryVersionId)
        {
            const string SQL = "SELECT 1 FROM FirstName WHERE CountryVersionID = @CountryVersionID";

            using SqlConnection conn = new SqlConnection(_connectionstring);
            using SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = SQL;
            cmd.Parameters.Add("@CountryVersionID", SqlDbType.Int).Value = countryVersionId;

            conn.Open();

            using SqlDataReader reader = cmd.ExecuteReader();

            return reader.HasRows;
        }*/
        public bool HasLastNames(int countryVersionId)
        {
            const string SQL = "SELECT 1 FROM LastName WHERE CountryVersionID = @CountryVersionID";

            using SqlConnection conn = new SqlConnection(_connectionstring);
            using SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = SQL;
            cmd.Parameters.Add("@CountryVersionID", SqlDbType.Int).Value = countryVersionId;

            conn.Open();

            using SqlDataReader reader = cmd.ExecuteReader();

            return reader.HasRows;
        }
    }
}
