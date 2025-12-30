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
        public int UploadSimulationSettings(SimulationSettings simulationSettings, int simulationDataId, int houseNumberRulesId)
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

                return simulationSettingsId;
            }

        }
        public int UploadHouseNumberRules(SimulationSettings simulationSettings)
        {
            string SQL = "INSERT INTO HouseNumberRules(MinNumber, MaxNumber, HasLetters, PercentageLetters) " +
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
        public int UploadSimulationStatistics(SimulationStatistics simulationStatistics, int simulationDataId)
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

                    return simulationStatisticsId;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
        public void UploadSelectedMunicipalities(
    int simulationSettingsId,
    List<MunicipalitySelection> selections)
        {
            if (selections == null || selections.Count == 0)
                return;

            string sql = @"
        INSERT INTO SimulationMunicipality
        (SimulationSettingsID, MunicipalityID, Percentage)
        VALUES (@SettingsID, @MunicipalityID, @Percentage)";

            using SqlConnection conn = new(_connectionstring);
            conn.Open();

            using SqlTransaction tx = conn.BeginTransaction();

            try
            {
                foreach (var selection in selections)
                {
                    using SqlCommand cmd = conn.CreateCommand();
                    cmd.Transaction = tx;
                    cmd.CommandText = sql;

                    cmd.Parameters.AddWithValue("@SettingsID", simulationSettingsId);
                    cmd.Parameters.AddWithValue("@MunicipalityID", selection.Municipality.Id);
                    cmd.Parameters.AddWithValue("@Percentage", selection.Percentage);

                    cmd.ExecuteNonQuery();
                }

                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }
        public List<SimulationOverviewDTO> GetAllSimulationOverviews()
        {
            var list = new List<SimulationOverviewDTO>();

            string SQL = "SELECT sd.ID AS SimulationDataId, c.Name AS CountryName, cv.Year, sd.Client AS ClientName, sd.DateCreated " +
                         "FROM SimulationData sd " +
                         "JOIN COuntryVersion cv on cv.ID = sd.CountryVersionID " +
                         "JOIN Country c on c.ID = cv.CountryID " +
                         "ORDER BY sd.DateCreated DESC";

            using(SqlConnection conn = new SqlConnection(_connectionstring))
            using(SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        int simulationDataId = reader.GetInt32(reader.GetOrdinal("SimulationDataId"));
                        string countryName = reader.GetString(reader.GetOrdinal("CountryName"));
                        int year = reader.GetInt32(reader.GetOrdinal("Year"));
                        string clientName = reader.GetString(reader.GetOrdinal("ClientName"));
                        DateTime dateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated"));

                        SimulationOverviewDTO simulationOverviewDTO = new SimulationOverviewDTO(simulationDataId, countryName, year, clientName, dateCreated);

                        list.Add(simulationOverviewDTO);
                    }
                }

                return list;
            }
        }
        public SimulationSettings GetSimulationSettingsBySimulationDataID(int simulationDataId)
        {
            SimulationSettings simSettings = null;

            string SQL = "SELECT ss.Id, ss.NumberCustomers, ss.MinAge, ss.MaxAge, hnr.MinNumber, hnr.MaxNumber, hnr.HasLetters, hnr.PercentageLetters " +
                         "FROM SimulationSettings ss " +
                         "JOIN HouseNumberRules hnr ON hnr.ID = ss.HouseNumberRulesID " +
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

            if(simSettings == null)
            {
                throw new InvalidOperationException("SimulationSettings not found");
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

                        simulationStatistics = new SimulationStatistics(id, totalCustomers, averageAgeSimulationDate, averageAgeCurrentDate, ageYoungestCustomer, ageOldestCustomer);
                    }
                }
            }
            return simulationStatistics;
        }
        public List<MunicipalitySelection> GetSelectedMunicipalities(int simulationSettingsId, List<Municipality> municipalities)
        {
            List<MunicipalitySelection> result = new();

            string sql = "SELECT MunicipalityID, Percentage " +
                         "FROM SimulationMunicipality " +
                         "WHERE SimulationSettingsID = @SettingsID";

            using SqlConnection conn = new(_connectionstring);
            using SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@SettingsID", simulationSettingsId);

            conn.Open();

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int municipalityId = reader.GetInt32(0);
                int percentage = reader.GetInt32(1);

                Municipality municipality = municipalities.FirstOrDefault(m => m.Id == municipalityId);

                if(municipality == null)
                {
                    throw new InvalidOperationException($"Municipality with ID {municipalityId} not found in provided list.");
                }

                MunicipalitySelection selection = new MunicipalitySelection(municipality, percentage, isSelected: true);

                result.Add(selection);
            }
            return result;
        }
        public void UploadMunicipalityStatistics(int simulationStatisticsId, List<MunicipalityStatistics> stats)
        {
            string SQL = "INSERT INTO SimulationMunicipalityStatistics (SimulationStatisticsID, MunicipalityID, CustomerCount) " +
                         "VALUES (@SimulationStatisticsID, @MunicipalityID, @CustomerCount";

            using SqlConnection conn = new(_connectionstring);
            conn.Open();

            using SqlTransaction tran = conn.BeginTransaction();

            try
            {
                foreach(var stat in stats)
                {
                    using SqlCommand cmd = conn.CreateCommand();
                    cmd.Transaction = tran;
                    cmd.CommandText = SQL;

                    cmd.Parameters.Add("@SimulationStatisticsID", SqlDbType.Int).Value = simulationStatisticsId;
                    cmd.Parameters.Add("@MuncipalityID", SqlDbType.Int).Value = stat.Municipality.Id;
                    cmd.Parameters.Add("@CustomerCount", SqlDbType.Int).Value = stat.Count;

                    cmd.ExecuteNonQuery();
                }

                tran.Commit();
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }
    }
}
