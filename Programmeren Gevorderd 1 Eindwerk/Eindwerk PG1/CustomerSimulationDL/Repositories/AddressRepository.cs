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

        public void UploadAddress(IEnumerable<Address> addresses, Country country)
        {
            string SQLMunicipality = "IF NOT EXISTS (Select 1 FROM Municipality WHERE CountryID = @CountryID AND Name = @Name) " +
                                     "INSERT INTO Municipality(CountryID, Name) " +
                                     "OUTPUT inserted.ID VALUES(@CountryID, @Name) " +
                                     "ELSE SELECT ID FROM Municipality WHERE CountryID = @CountryID AND Name = @Name";
            string SQLAddress = "INSERT INTO Address(MunicipalityID, Street) " +
                                "OUTPUT inserted.ID VALUES(@MunicipalityID, @Street)";

            using(SqlConnection conn = new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            using(SqlCommand cmd2 = conn.CreateCommand())
            using(SqlTransaction tran = conn.BeginTransaction())
            {
                conn.Open();
                cmd.CommandText = SQLMunicipality;
                cmd2.CommandText = SQLAddress;
                cmd.Transaction = tran;
                cmd2.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("@CountryID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100));
                cmd2.Parameters.Add(new SqlParameter("@MunicipalityID", SqlDbType.Int));
                cmd2.Parameters.Add(new SqlParameter("@Street", SqlDbType.NVarChar, 100));
                int municipalityId;
                int addressId;
                try
                {
                    foreach(Address a in addresses)
                    {
                        cmd.Parameters["@CountryID"].Value = country.Id;
                        cmd.Parameters["@Name"].Value = a.Municipality.Name;
                        municipalityId = (int)cmd.ExecuteScalar();

                        cmd2.Parameters["@MunicipalityID"].Value = municipalityId;
                        cmd2.Parameters["@Street"].Value = a.Street;
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
    }
}
