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
        public int UploadSimulationData(SimulationData simulationData, int countryVersionId)
        {
            string SQL = "INSERT INTO SimulationData(Client, DateCreated, CountryVersionID) " +
                         "OUTPUT inserted.ID VALUES(@Client, @DateCreated, @CountryVersionID)";

            using (SqlConnection conn = new SqlConnection(_connectionstring))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
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
                    cmd.Parameters["@CountryVersionID"].Value = countryVersionId;
                    simulationDataId = (int)cmd.ExecuteScalar();

                    tran.Commit();
                    return simulationDataId;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
        public void UploadSimulationSettings(SimulationSettings simulationSettings, int simulationDataId, int houseNumberRulesId)
        {
            string SQLSimulationSettings = "INSERT INTO SimulationSettings(SimulationDataID, NumberCustomers, MinAge, MaxAge, HouseNumberRulesID) " +
                                           "OUTPUT inserted.ID VALUES(@SimulationDataID, @NumberCustomers, @MinAge, @MaxAge, @HouseNumberRulesID)";

            using (SqlConnection conn = new SqlConnection(_connectionstring))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                cmd.CommandText = SQLSimulationSettings;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("@SimulationDataID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@NumberCustomers", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@MinAge", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@MaxAge", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@HouseNumberRulesID", SqlDbType.Int));
                int simulationSettingsId;
                try
                {
                    cmd.Parameters["@SimulationDataID"].Value = simulationDataId;
                    cmd.Parameters["@NumberCustomers"].Value = simulationSettings.TotalCustomers;
                    cmd.Parameters["@MinAge"].Value = simulationSettings.MinAge;
                    cmd.Parameters["@MaxAge"].Value = simulationSettings.MaxAge;
                    cmd.Parameters["@HouseNumberRulesID"].Value = houseNumberRulesId;
                    simulationSettingsId = (int)cmd.ExecuteScalar();

                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }

        }
        public int UploadHouseNumberRules(SimulationSettings simulationSettings)
        {
            string SQL = "INSERT INTO HouseNumberRules(MinNumber, MaxNumber, HasLetters, PercentageLetters " +
                         "OUTPUT inserted.ID VALUES(@MinNumber, @MaxNumber, @HasLetters, @PercentageLetters)";

            using(SqlConnection conn = new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                cmd.CommandText = SQL;
                cmd.Transaction = tran;
                cmd.Parameters.Add(new SqlParameter("@MinNumber", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@MaxNumber", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@HasLetters", SqlDbType.Bit));
                cmd.Parameters.Add(new SqlParameter("@PercentageLetters", SqlDbType.Int));
                int houseNumberRulesId;
                try
                {
                    cmd.Parameters["@MinNumber"].Value = simulationSettings.MinNumber;
                    cmd.Parameters["@MaxNumber"].Value = simulationSettings.MaxNumber;
                    cmd.Parameters["@HasLetters"].Value = simulationSettings.HasLetters;
                    cmd.Parameters["@PercentageLetters"].Value = simulationSettings.PercentageLetters;
                    houseNumberRulesId = (int)cmd.ExecuteScalar();

                    tran.Commit();

                    return houseNumberRulesId;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
        public void UploadSimulationStatistics(SimulationStatistics simulationStatistics, int simulationDataId)
        {
            string SQL = "INSERT INTO SimulationStatistics(SimulationDataID, TotalCustomers, AverageAgeSimulationDate, AverageAgeCurrentDate, AgeYoungestCustomer, AgeOldestCustomer) " +
                         "OUTPUT inserted.ID VALUES(@SimulationDataID, @TotalCustomers, @AverageAgeSimulationDate, @AverageAgeCurrentDate, @AgeYoungestCustomer, @AgeOldestCustomer)";

            using (SqlConnection conn = new SqlConnection(_connectionstring))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
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
                    cmd.Parameters["@SimulationDataID"].Value = simulationDataId;
                    cmd.Parameters["@TotalCustomers"].Value = simulationStatistics.TotalCustomers;
                    cmd.Parameters["@AverageAgeSimulationDate"].Value = simulationStatistics.AverageAgeOnSimulationDate;
                    cmd.Parameters["@AverageAgeCurrentDate"].Value = simulationStatistics.AverageAgeOnCurrentDate;
                    cmd.Parameters["@AgeYoungestCustomer"].Value = simulationStatistics.AgeYoungestCustomer;
                    cmd.Parameters["@AgeOldestCustomer"].Value = simulationStatistics.AgeOldestCustomer;
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
        public List<SimulationData> GetAllSimulationData()
        {
            List<SimulationData> simulationDatas = new List<SimulationData>();

            string SQL = "SELECT sd.ID, sd.Client, sd.DateCreated " +
                         "FROM SimulationData sd";

            using (SqlConnection conn = new SqlConnection(_connectionstring))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("ID"));
                        string client = reader.GetString(reader.GetOrdinal("Client"));
                        DateTime dateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated"));

                        SimulationData simulationData = new SimulationData(id, client, dateCreated);

                        simulationDatas.Add(simulationData);
                    }
                }
            }
            return simulationDatas;
        }
        public SimulationSettings GetSimulationSettingsBySimulationDataID(int simulationDataId)
        {
            SimulationSettings simSettings = null;

            string SQL = "SELECT ss.Id, ss.NumberCustomers, ss.MinAge, ss.MaxAge, hnr.MinNumber, hnr.MaxNumer, hnr.HasLetters, hnr.PercentageLetters " +
                         "FROM SimulationSettings ss " +
                         "JOIN HouseNumberRules hnr " +
                         "ONT ss.HouseNumberRulesID = hnr.ID " +
                         "Where ss.SimulationDataID = @SimulationDataID";

            using(SqlConnection conn = new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.Parameters.AddWithValue("SimulationDataID", simulationDataId);
                cmd.CommandText = SQL;

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("ID"));
                        int numberCustomers = reader.GetInt32(reader.GetOrdinal("NumberCustomers"));
                        int minAge = reader.GetInt32(reader.GetOrdinal("MinAge"));
                        int maxAge = reader.GetInt32(reader.GetOrdinal("MaxAge"));

                        int minNumber = reader.GetInt32(reader.GetOrdinal("MinNumber"));
                        int maxNumber = reader.GetInt32(reader.GetOrdinal("MaxNumber"));
                        bool hasLetters = reader.GetBoolean(reader.GetOrdinal("HasLetters"));
                        int percentageLetters = reader.GetInt32(reader.GetOrdinal("PercentageLetters"));

                        simSettings = new SimulationSettings(id, null, numberCustomers, minAge, maxAge, minNumber, maxNumber, hasLetters, percentageLetters);
                    }
                }
            }
            return simSettings;
        }

        public SimulationStatistics GetSimulationStatisticsBySimulationDataID(int simulationDataId)
        {
            SimulationStatistics simulationStatistics = null;

            string SQL = "SELECT ss.ID, ss.TotalCustomers, ss.AverageAgeSimulationDate, ss.AverageAgeCurrentDate, ss.AgeYoungestCustomer, ss.AgeOldestCustomer " +
                         "FROM SimulationStatistics ss " +
                         "WHERE SimulationDataID = @SimulationDataID";

            using(SqlConnection conn = new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.Parameters.AddWithValue("SimulationDataID", simulationDataId);
                cmd.CommandText = SQL;

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("ID"));
                        int totalCustomers = reader.GetInt32(reader.GetOrdinal("TotalCustomers"));
                        double averageAgeSimulationDate = reader.GetFloat(reader.GetOrdinal("AverageAgeSimulationDate"));
                        double averageAgeCurrentDate = reader.GetFloat(reader.GetOrdinal("AverageAgeCurrentDate"));
                        int ageYoungestCustomer = reader.GetInt32(reader.GetOrdinal("AgeYoungestCustomer"));
                        int ageOldestCustomer = reader.GetInt32(reader.GetOrdinal("AgeOldestCustomer"));

                        simulationStatistics = new SimulationStatistics(id, totalCustomers, null, averageAgeSimulationDate, averageAgeCurrentDate, ageYoungestCustomer, ageOldestCustomer);
                    }
                }
            }
            return simulationStatistics;
        }
    }
}
