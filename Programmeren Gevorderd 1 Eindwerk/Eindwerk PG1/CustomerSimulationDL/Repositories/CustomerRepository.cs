using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using CustomerSimulationBL.DTOs;

namespace CustomerSimulationDL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private string _connectionstring;

        public CustomerRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public void UploadCustomer(IEnumerable<CustomerDTO> customers, int simulationDataId, int countryVersionId)
        {
            string SQL = "INSERT INTO Customer(CountryVersionID, SimulationDataID, FirstName, LastName, Municipality, Street, HouseNumber ,BirthDate) " +
                         "OUTPUT inserted.ID VALUES(@CountryVersionID, @SimulationDataID, @FirstName, @LastName, @Municipality, @Street, @HouseNumber, @BirthDate)";

            using(SqlConnection conn  = new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
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
                    foreach(var c in customers)
                    {
                        cmd.Parameters["@CountryVersionID"].Value = countryVersionId;
                        cmd.Parameters["@SimulationDataID"].Value = simulationDataId;
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

        public List<Customer> GetCustomerBySimulationDataID(int simulationDataID)
        {
            List<Customer> customers = new List<Customer>();

            string SQL = "SELECT c.ID, c.FirstName, c.LastName, c.Municipality, c.Street, c.HouseNumber, c.BirthDate " +
                         "FROM Customer c " +
                         "WHERE c.SimulationDataID = @SimulationDataID";

            using(SqlConnection conn = new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@SimulationDataID", simulationDataID);
                cmd.CommandText = SQL;
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("ID"));
                        string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        string lastName = reader.GetString(reader.GetOrdinal("LastName"));
                        string municipality = reader.GetString(reader.GetOrdinal("Municipality"));
                        string street = reader.GetString(reader.GetOrdinal("Street"));
                        string houseNumber = reader.GetString(reader.GetOrdinal("HouseNumber"));
                        DateTime birthDate = reader.GetDateTime(reader.GetOrdinal("BirthDate"));

                        Customer customer = new Customer(id, firstName, lastName, municipality, street, birthDate, houseNumber);

                        customers.Add(customer);
                    }
                }
            }
            return customers;
        }
    }
}
