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
    public class SimulationRepository : ISimulationRepository
    {
        private string _connectionstring;
        public SimulationRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public void UploadSimulationData(SimulationData simulationData, CountryVersion countryVersion)
        {
            string SQL = "INSERT INTO SimulationData(Client, DateCreated, CountryVersionID) " +
                         "OUTPUT inserted.ID VALUES(@Client, @DateCreated, @CountryVersionID)";

            using(SqlConnection conn = new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            using(SqlTransaction tran = conn.BeginTransaction())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("@Client", SqlDbType.NVarChar, 100));
                cmd.Parameters.Add(new SqlParameter("@DateCreated", SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@CountryVersionID", SqlDbType.Int));
                int simulationDataId;
                try
                {
                    cmd.Parameters["@Client"].Value = simulationData.Client;
                    cmd.Parameters["@DateCreated"].Value = simulationData.DateCreated;
                    cmd.Parameters["@CountryVersionID"].Value = countryVersion.Id;
                    simulationDataId = (int)cmd.ExecuteScalar();

                    tran.Commit();
                }
                catch(Exception)
                {
                    tran.Rollback();
                }
            }
        }
        public void UploadSimulationSettings(SimulationSettings simulationSettings)
        {
            string SQLSimulationSettings = "INSERT INTO SimulationSettings(SimulationDataID, NumberCustomers, MinAge, MaxAge, HouseNumberRules) " +
                                           "OUTPUT inserted.ID VALUES(@SimulationID, @NumberCustomers, @MinAge, @MaxAge, @HouseNumberRules)";

            using(SqlConnection conn = new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            using(SqlTransaction tran = conn.BeginTransaction())
            {
                conn.Open();
                cmd.CommandText = SQLSimulationSettings;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("@SimulationDataID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@NumberCustomers", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@MinAge", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@MaxAge", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@HouseNumberRules", SqlDbType.NVarChar, 100));
                int simulationSettingsId;
                try
                {
                    cmd.Parameters["@SimulationDataID"].Value = simulationSettings.SimulationData.Id;
                    cmd.Parameters["@NumberCustomers"].Value = simulationSettings.NumberCustomers;
                    cmd.Parameters["@MinAge"].Value = simulationSettings.MinAge;
                    cmd.Parameters["@MaxAge"].Value = simulationSettings.MaxAge;
                    cmd.Parameters["@HouseNumberRules"].Value = simulationSettings.HouseNumberRules;
                    simulationSettingsId = (int)cmd.ExecuteScalar();

                    tran.Commit();
                }
                catch(Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }

        }
        public void UploadSimulationStatistics(SimulationStatistics simulationStatistics)
        {
            string SQL = "INSERT INTO SimulationStatistics(SimulationDataID, TotalCustomers, AverageAgeSimulationDate, AverageAgeCurrentDate, AgeYoungestCustomer, AgeOldestCustomer) " +
                         "OUTPUT inserted.ID VALUES(@SimulationDataID, @TotalCustomers, @AverageAgeSimulationDate, @AverageAgeCurrentDate, @AgeYoungestCustomer, @AgeOldestCustomer)";

            using (SqlConnection conn = new SqlConnection(_connectionstring))
            using (SqlCommand cmd = conn.CreateCommand())
            using (SqlTransaction tran = conn.BeginTransaction())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("@SimulationDataID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@TotalCustomers", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@AverageAgeSimulationDate", SqlDbType.Decimal));
                cmd.Parameters.Add(new SqlParameter("@AverageAgeCurrentDate", SqlDbType.Decimal));
                cmd.Parameters.Add(new SqlParameter("@AgeYoungestCustomer", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@AgeOldestCustomer", SqlDbType.Int));
                int simulationStatisticsId;
                try
                {
                    cmd.Parameters["@SimulationDataID"].Value = simulationStatistics.SimulationData.Id;
                    cmd.Parameters["@TotalCustomers"].Value = simulationStatistics.TotalCustomers;
                    cmd.Parameters["@AverageAgeSimulationDate"].Value = simulationStatistics.AverageAgeOnSimulationDate;
                    cmd.Parameters["@AverageAgeCurrentDate"].Value = simulationStatistics.AverageAgeOnCurrentDate;
                    cmd.Parameters["AgeYoungestCustomer"].Value = simulationStatistics.AgeYoungestCustomer;
                    cmd.Parameters["AgeOldestCustomer"].Value = simulationStatistics.AgeOldestCustomer;
                    simulationStatisticsId = (int)cmd.ExecuteScalar();

                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
    }
}
