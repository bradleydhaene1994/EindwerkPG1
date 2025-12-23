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
    public class CustomerRepository : ICustomerRepository
    {
        private string _connectionstring;

        public CustomerRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public void UploadCustomer(IEnumerable<Customer> customers)
        {
            string SQL = "INSERT INTO Customer(CountryVersionID, SimulationDataID, FirstName, LastName, Municipality, Street, HouseNumber ,BirthDate) " +
                         "OUTPUT inserted.ID(@CountryVersionID, @SimulationDataID, @FirstName, @LastName, @Municipality, @Street, @HouseNumber, @BirthDate)";

            using(SqlConnection conn  = new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            using(SqlTransaction tran = conn.BeginTransaction())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("@CountryVersionID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@SimulationDataID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 100));
                cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 100));
                cmd.Parameters.Add(new SqlParameter("@Municipality", SqlDbType.NVarChar, 100));
                cmd.Parameters.Add(new SqlParameter("@Street", SqlDbType.NVarChar, 100));
                cmd.Parameters.Add(new SqlParameter("@HouseNumber", SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@BirthDate", SqlDbType.Date));
                int customerId;
                try
                {
                    foreach(Customer c in customers)
                    {
                        cmd.Parameters["@CountryVersionID"].Value = c.CountryVersion.Id;
                        cmd.Parameters["@SimulationDataID"].Value = c.simulationData.Id;
                        cmd.Parameters["@FirstName"].Value = c.FirstName;
                        cmd.Parameters["@LastName"].Value = c.LastName;
                        cmd.Parameters["@Municipality"].Value = c.Municipality;
                        cmd.Parameters["@Street"].Value = c.Street;
                        cmd.Parameters["@HouseNumber"].Value = c.HouseNumber;
                        cmd.Parameters["@BirthDate"].Value = c.BirthDate;
                        customerId = (int)cmd.ExecuteScalar();
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
