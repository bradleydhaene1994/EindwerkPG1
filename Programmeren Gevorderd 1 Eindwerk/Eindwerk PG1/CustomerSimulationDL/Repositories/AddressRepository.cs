using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Interfaces;
using CustomerSimulationBL.Domein;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CustomerSimulationDL.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private string _connectionstring;

        public AddressRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public void UploadAddress(IEnumerable<Address> addresses, int countryVersionID, IProgress<int> progress)
        {
            using(SqlConnection connection = new SqlConnection(_connectionstring))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                Dictionary<string, int> municipalityLookup;

                var addressList = addresses.ToList();
                int total = addressList.Count;
                int processed = 0;

                const int REPORT_EVERY = 1;
                
                try
                {
                    foreach (var address in addresses)
                    {
                        InsertAddress(address, countryVersionID, connection, transaction);

                        processed++;

                        if(processed % REPORT_EVERY == 0 || processed == total)
                        {
                            int percent = (int)((processed / (double)total) * 100);
                            progress?.Report(percent);
                        }
                    }

                    transaction.Commit();
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        private void InsertAddress(Address address, int countryVersionId, SqlConnection conn, SqlTransaction tran)
        {
            string SQLMunicipality = "INSERT INTO Municipality (CountryVersionID, Name) " +
                                     "OUTPUT INSERTED.ID VALUES (@CountryVersionID, @Name)";

            string SQLAddress = "INSERT INTO [Address] (MunicipalityID, StreetName, CountryVersionID) " +
                                "OUTPUT INSERTED.ID VALUES (@MunicipalityID, @StreetName, @CountryVersionID)";

            using(SqlCommand municipalityCmd = new SqlCommand())
            using(SqlCommand addressCmd = new SqlCommand())
            {
                municipalityCmd.Connection = conn;
                municipalityCmd.Transaction = tran;
                municipalityCmd.CommandText = SQLMunicipality;

                addressCmd.Connection = conn;
                addressCmd.Transaction = tran;
                addressCmd.CommandText = SQLAddress;

                municipalityCmd.Parameters.Add("@CountryVersionID", SqlDbType.Int);
                municipalityCmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100);

                addressCmd.Parameters.Add("@MunicipalityID", SqlDbType.Int);
                addressCmd.Parameters.Add("@StreetName", SqlDbType.NVarChar, 100);
                addressCmd.Parameters.Add("@CountryVersionID", SqlDbType.Int);

                int? municipalityId = null;

                if(address.Municipality != null)
                {
                    try
                    {
                        municipalityCmd.Parameters["@CountryVersionID"].Value = countryVersionId;
                        municipalityCmd.Parameters["@Name"].Value = address.Municipality.Name;
                        municipalityId = (int)municipalityCmd.ExecuteScalar();
                    }
                    catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
                    {
                        //If duplicate => fetch existing ID
                        municipalityCmd.CommandText = "SELECT ID FROM Municipality WHERE CountryVersionID = @CountryVersionID AND Name = @Name";

                        municipalityId = (int)municipalityCmd.ExecuteScalar();
                    }
                }

                int addressId;

                try
                {
                    addressCmd.Parameters["@MunicipalityID"].Value = (object?)municipalityId ?? DBNull.Value;
                    addressCmd.Parameters["@StreetName"].Value = address.Street;
                    addressCmd.Parameters["@CountryVersionID"].Value = countryVersionId;
                    addressId = (int)addressCmd.ExecuteScalar();
                }
                catch(SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
                {
                    //If duplicate => fetch ID
                    addressCmd.CommandText = "SELECT ID FROM [Address] WHERE StreetName = @StreetName AND CountryVersionID = @CountryVersionID " +
                                             "AND (MunicipalityID = @MunicipalityID OR (MunicipalityID IS NULL AND @MunicipalityID IS NULL))";
                    addressId = (int)addressCmd.ExecuteScalar();
                }
            }
        }

        public List<Address> GetAddressesByCountryVersionID(int countryVersionId)
        {
            List<Address> addresses = new List<Address>();

            string SQL = "SELECT a.StreetName, m.Name " +
                         "FROM Address a " +
                         "LEFT JOIN Municipality m ON a.MunicipalityID = m.ID " +
                         "WHERE m.CountryVersionID = @CountryVersionID " +
                         "OR a.MunicipalityID IS NULL";

            using(SqlConnection conn =  new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@CountryVersionID", countryVersionId);
                conn.Open();
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string Street = reader.GetString(reader.GetOrdinal("StreetName"));
                        string MunicipalityName = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name"));

                        Municipality municipality = MunicipalityName == null ? null : new Municipality(MunicipalityName);

                        Address address = new Address(municipality, Street);

                        addresses.Add(address);
                    }
                }
            }
            return addresses;
        }
    }
}
