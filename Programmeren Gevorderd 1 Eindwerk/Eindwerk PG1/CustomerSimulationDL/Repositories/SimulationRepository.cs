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
        public void UploadSimulationData(SimulationData simulationData)
        {
            string SQL = "INSERT INTO SimulationData(CountryID, ClientName, DateCreated) " +
                         "OUTPUT inserted.ID VALUES(@CountryID, @ClientName, @DateCreated)";

            using(SqlConnection conn = new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            using(SqlTransaction tran = conn.BeginTransaction())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("@CountryID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@ClientName", SqlDbType.NVarChar, 100));
                cmd.Parameters.Add(new SqlParameter("@DateCreated", SqlDbType.DateTime));
                int simulationDataId;
                try
                {
                    cmd.Parameters["@CountryID"].Value = simulationData.Country.Id;
                    cmd.Parameters["@ClientName"].Value = simulationData.ClientName;
                    cmd.Parameters["@DateCreated"].Value = simulationData.DateCreated;
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
            string SQLSimulationSettings = "INSERT INTO SimulationSettings(SimulationID, CountryID, NumberCustomers, MinAge, MaxAge, HouseNumberRules) " +
                                           "OUTPUT inserted.ID VALUES(@SimulationID, @CountryID, @NumberCustomers, @MinAge, @MaxAge, @HouseNumberRules)";
            string SQLSimulationSettingsMunicipality = "INSERT INTO SimulationSettingsMunicipality(SimulationSettingsID, MunicipalityID) " +
                                                       "OUTPUT inserted.ID VALUES(@SimulationSettingsID, @MunicipalityID)";

            using(SqlConnection conn = new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            using(SqlCommand cmd2 = conn.CreateCommand())
            using(SqlTransaction tran = conn.BeginTransaction())
            {
                conn.Open();
                cmd.CommandText = SQLSimulationSettings;
                cmd2.CommandText = SQLSimulationSettingsMunicipality;
                cmd.Transaction = tran;
                cmd2.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("@SimulationID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CountryID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@NumberCustomers", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@MinAge", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@MaxAge", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@HouseNumberRules", SqlDbType.NVarChar, 100));
                cmd2.Parameters.Add(new SqlParameter("@SimulationSettingsID", SqlDbType.Int));
                cmd2.Parameters.Add(new SqlParameter("@MunicipalityID", SqlDbType.Int));
                int simulationSettingsId;
                int simulationSettingsMunicipalityId;
                try
                {
                    cmd.Parameters["@SimulationID"].Value = simulationSettings.SimulationData.Id;
                    cmd.Parameters["@CountryID"].Value = simulationSettings.Country.Id;
                    cmd.Parameters["@NumberCustomers"].Value = simulationSettings.NumberCustomers;
                    cmd.Parameters["@MinAge"].Value = simulationSettings.MinAge;
                    cmd.Parameters["@MaxAge"].Value = simulationSettings.MaxAge;
                    cmd.Parameters["@HouseNumberRules"].Value = simulationSettings.HouseNumberRules;
                    simulationSettingsId = (int)cmd.ExecuteScalar();

                    foreach(Municipality m in simulationSettings.SelectedMunicipalities)
                    {
                        cmd2.Parameters["@SimulationSettingsID"].Value = simulationSettingsId;
                        cmd2.Parameters["@MunicipalityID"].Value = m.Id;
                    }

                    simulationSettingsMunicipalityId = (int)cmd2.ExecuteScalar();

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
                cmd.Parameters.Add(new SqlParameter("@AverageAgeSimulationDate", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@AverageAgeCurrentDate", SqlDbType.Int));
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
