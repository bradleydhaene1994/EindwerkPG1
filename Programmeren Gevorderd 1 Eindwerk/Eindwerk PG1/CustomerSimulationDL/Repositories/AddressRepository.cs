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

        public void UploadAddress(IEnumerable<Address> addresses, int countryVersionID)
        {
            string SQLMunicipality = "IF NOT EXISTS (SELECT 1 FROM Municipality WHERE CountryVersionID = @CountryVersionID AND Name = @Name) " +
                                     "INSERT INTO Municipality(CountryVersionID, Name) " +
                                     "OUTPUT inserted.ID VALUES(@CountryVersionID, @Name) " +
                                     "ELSE SELECT ID FROM Municipality WHERE CountryVersionID = @CountryVersionID AND Name = @Name";
            string SQLAddress = "IF NOT EXISTS (SELECT 1 FROM ADDRESS WHERE StreetName = @StreetName AND (MunicipalityID = @MunicipalityID OR (MunicipalityID IS NULL and @MunicipalityID IS NULL)) " +
                                "INSERT INTO Address(MunicipalityID, StreetName " +
                                "OUTPUT inserted.ID VALUES(@MunicipalityID, @StreetName) " +
                                "ELSE SELECT ID FROM Adress WHERE Streetname = @StreetName AND (MunicipalityID = @MunicipalityID OR (MunicipalityID IS NULL AND @MunicipalityID IS NULL))";

            using(SqlConnection conn = new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            using(SqlCommand cmd2 = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                cmd.CommandText = SQLMunicipality;
                cmd2.CommandText = SQLAddress;
                cmd.Transaction = tran;
                cmd2.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("@CountryVersionID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100));
                cmd2.Parameters.Add(new SqlParameter("@MunicipalityID", SqlDbType.Int));
                cmd2.Parameters.Add(new SqlParameter("@StreetName", SqlDbType.NVarChar, 100));
                int? municipalityId;
                int addressId;
                try
                {
                    foreach(Address a in addresses)
                    {
                        municipalityId = null;
                        
                        if(a.Municipality != null)
                        {
                            cmd.Parameters["@CountryVersionID"].Value = countryVersionID;
                            cmd.Parameters["@Name"].Value = a.Municipality.Name;
                            municipalityId = (int)cmd.ExecuteScalar();
                        }

                        cmd2.Parameters["@MunicipalityID"].Value = (object?)municipalityId ?? DBNull.Value;
                        cmd2.Parameters["@StreetName"].Value = a.Street;
                        addressId = (int)cmd2.ExecuteScalar();
                    }
                    tran.Commit();
                }
                catch(Exception)
                {
                    tran.Rollback();
                    throw;
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
