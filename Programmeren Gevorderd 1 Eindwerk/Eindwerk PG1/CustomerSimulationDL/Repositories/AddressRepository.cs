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

        public void UploadAddress(IEnumerable<Address> addresses)
        {
            string SQLMunicipality = "IF NOT EXISTS (Select 1 FROM Municipality WHERE CountryVersionID = @CountryVersionID AND Name = @Name) " +
                                     "INSERT INTO Municipality(CountryVersionID, Name) " +
                                     "OUTPUT inserted.ID VALUES(@CountryVersionID, @Name) " +
                                     "ELSE SELECT ID FROM Municipality WHERE CountryVersionID = @CountryVersionID AND Name = @Name";
            string SQLAddress = "INSERT INTO Address(MunicipalityID, StreetName) " +
                                "OUTPUT inserted.ID VALUES(@MunicipalityID, @StreetName)";

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
                int municipalityId;
                int addressId;
                try
                {
                    foreach(Address a in addresses)
                    {
                        cmd.Parameters["@CountryVersionID"].Value = a.Municipality.Country.Id;
                        cmd.Parameters["@Name"].Value = a.Municipality.Name;
                        municipalityId = (int)cmd.ExecuteScalar();

                        cmd2.Parameters["@MunicipalityID"].Value = municipalityId;
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

        public List<Address> GetAddressesByCountryID(int countryId)
        {
            List<Address> addresses = new List<Address>();

            string SQL = "SELECT a.ID , a.Street, a.MunicipalityID, m.Name " +
                         "FROM Address a " +
                         "INNER JOIN Municipality m ON a.MunicipalityID = m.ID " +
                         "WHERE m.CountryID = @CountryID";

            using(SqlConnection conn =  new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@CountryID", countryId);
                conn.Open();
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string Street = reader.GetString(reader.GetOrdinal("Street"));
                        string MunicipalityName = reader.GetString(reader.GetOrdinal("Name"));

                        Municipality municipality = new Municipality(MunicipalityName);

                        Address address = new Address(municipality, Street);

                        addresses.Add(address);
                    }
                }
            }
            return addresses;
        }
    }
}
