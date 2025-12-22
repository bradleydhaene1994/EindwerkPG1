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
            string SQL = "INSERT INTO Customer(Country, FirstName, LastName, Address, SimulationID, BirthDate, HouseNumber) " +
                         "OUTPUT inserted.ID(@Country, @FirstName, @LastName, @Address, @SimulationID, @BirthDate, @HouseNumber)";

            using(SqlConnection conn  = new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            using(SqlTransaction tran = conn.BeginTransaction())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("@Country", SqlDbType.NVarChar, 100));
                cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 100));
                cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 100));
                cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar, 100));
                cmd.Parameters.Add(new SqlParameter("@SimulationID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BirthDate", SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@HouseNumber", SqlDbType.NVarChar, 100));
                int customerId;
                try
                {
                    foreach(Customer c in customers)
                    {
                        cmd.Parameters["@Country"].Value = c.Country;
                        cmd.Parameters["@FirstName"].Value = c.FirstName;
                        cmd.Parameters["@LastName"].Value = c.LastName;
                        cmd.Parameters["@Address"].Value = c.Address;
                        cmd.Parameters["@SimulationID"].Value = c.simulationData.Id;
                        cmd.Parameters["@BirthDate"].Value = c.BirthDate;
                        cmd.Parameters["@HouseNumber"].Value = c.HouseNumber;
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
