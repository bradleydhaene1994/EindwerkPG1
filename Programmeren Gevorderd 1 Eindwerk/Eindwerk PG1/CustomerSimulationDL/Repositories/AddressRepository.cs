using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Interfaces;
using CustomerSimulationBL.Domein;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

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
            const string SQLMunicipality = "INSERT INTO Municipality (CountryVersionID, Name) " +
                                           "OUTPUT INSERTED.ID VALUES (@CountryVersionID, @Name)";

            const string SQLMunicipalitySelect = "SELECT ID FROM Municipality " +
                                                 "WHERE CountryVersionID = @CountryVersionID AND Name = @Name";

            const string SQLAddress = "INSERT INTO [Address] (MunicipalityID, StreetName, CountryVersionID) " +
                                      "OUTPUT INSERTED.ID VALUES (@MunicipalityID, @StreetName, @CountryVersionID)";

            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                connection.Open();
                
                using var transaction = connection.BeginTransaction();

                var municipalityCache = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                var unknownMunicipalityCache = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                var knownMunicipalityCache = new HashSet<(string Municipality, string Street)>();

                var addressList = addresses.ToList();
                int total = addressList.Count;
                int processed = 0;

                const int REPORT_EVERY = 100;
                
                try
                {
                    using var municipalityCmd = new SqlCommand(SQLMunicipality, connection, transaction);
                    using var municipalitySelectCmd = new SqlCommand(SQLMunicipalitySelect, connection, transaction);
                    using var addressCmd = new SqlCommand(SQLAddress, connection, transaction);

                    municipalityCmd.Parameters.Add("@CountryVersionID", SqlDbType.Int);
                    municipalityCmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100);


                    municipalitySelectCmd.Parameters.Add("@CountryVersionID", SqlDbType.Int);
                    municipalitySelectCmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100);

                    addressCmd.Parameters.Add("@MunicipalityID", SqlDbType.Int);
                    addressCmd.Parameters.Add("@StreetName", SqlDbType.NVarChar, 100);
                    addressCmd.Parameters.Add("@CountryVersionID", SqlDbType.Int);

                    municipalityCmd.Prepare();
                    municipalitySelectCmd.Prepare();
                    addressCmd.Prepare();

                    foreach (var address in addressList)
                    {
                        InsertAddress(address, countryVersionID, municipalityCache, unknownMunicipalityCache, knownMunicipalityCache, municipalityCmd, municipalitySelectCmd, addressCmd);

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
                    throw;
                }
            }
        }
        private void InsertAddress(Address address, int countryVersionId, Dictionary<string, int> municipalityCache, HashSet<string> unknownMunicipalityCache, HashSet<(string Municipality, string Street)> knownAddressCache, SqlCommand municipalityCmd, SqlCommand municipalitySelectCmd, SqlCommand addressCmd)
        {
            string streetName = address.Street;
            bool isUnknownMunicipality = address.Municipality == null || address.Municipality.Name.Equals("unknown", StringComparison.OrdinalIgnoreCase);
            int? municipalityId = null;

            if (isUnknownMunicipality)
            {
                if (!unknownMunicipalityCache.Add(streetName))
                {
                    return;
                }
            }
            else
            {
                string municipalityName = address.Municipality.Name;

                if (!knownAddressCache.Add((municipalityName, streetName)))
                {
                    return;
                }

                if (!municipalityCache.TryGetValue(municipalityName, out int cachedId))
                {
                    municipalityCmd.Parameters["@CountryVersionID"].Value = countryVersionId;
                    municipalityCmd.Parameters["@Name"].Value = municipalityName;

                    try
                    {
                        cachedId = (int)municipalityCmd.ExecuteScalar();
                    }
                    catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
                    {
                        municipalitySelectCmd.Parameters["@CountryVersionID"].Value = countryVersionId;
                        municipalitySelectCmd.Parameters["@Name"].Value = municipalityName;
                        cachedId = (int)municipalitySelectCmd.ExecuteScalar();
                    }

                    municipalityCache[municipalityName] = cachedId;
                }

                municipalityId = cachedId;
            }

            addressCmd.Parameters["@MunicipalityID"].Value = (object?)municipalityId ?? DBNull.Value;
            addressCmd.Parameters["@StreetName"].Value = streetName;
            addressCmd.Parameters["@CountryVersionID"].Value = countryVersionId;

            int addressId = (int)addressCmd.ExecuteScalar();
        }
        public List<Address> GetAddressesByCountryVersionID(int countryVersionId, List<Municipality> municipalities)
        {
            List<Address> addresses = new List<Address>();

            string SQL = "SELECT a.StreetName, a.MunicipalityID, m.Name " +
                         "FROM Address a " +
                         "LEFT JOIN Municipality m ON a.MunicipalityID = m.ID " +
                         "WHERE m.CountryVersionID = @CountryVersionID";

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
                        string street = reader.GetString(reader.GetOrdinal("StreetName"));

                        Municipality? municipality = null;

                        if(!reader.IsDBNull(reader.GetOrdinal("MunicipalityID")))
                        {
                            int municipalityId = reader.GetInt32(reader.GetOrdinal("MunicipalityID"));

                            municipality = municipalities.FirstOrDefault(m => m.Id == municipalityId);

                            if(municipality == null)
                            {
                                throw new InvalidOperationException($"Municipality {municipalityId} not found for address {street}");
                            }
                        }

                        Address address = new Address(municipality, street);

                        addresses.Add(address);
                    }
                }
            }
            return addresses;
        }
    }
}
