using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.DTOs;
using CustomerSimulationBL.Interfaces;

namespace CustomerSimulationBL.Managers
{
    public class SimulationDataManager
    {
        private ISimulationRepository _simulationRepo;

        public SimulationDataManager(ISimulationRepository simulationRepo)
        {
            _simulationRepo = simulationRepo;
        }
        public int UploadSimulationData(SimulationData simulationData, int countryVersionId)
        {
            int simulationDataId = _simulationRepo.UploadSimulationData(simulationData, countryVersionId);
            return simulationDataId;
        }
        public int UploadSimulationSettings(SimulationSettings simulationSettings, int simulationDataId, int houseNumberRulesId)
        {
            int simulationSettingsId = _simulationRepo.UploadSimulationSettings(simulationSettings, simulationDataId, houseNumberRulesId);
            return simulationSettingsId;
        }
        public int UploadSimulationStatistics(SimulationStatistics simulationStatistics, int simulationDataId)
        {
            int simulationStatisticsId = _simulationRepo.UploadSimulationStatistics(simulationStatistics, simulationDataId);
            return simulationStatisticsId;
        }
        public int UploadHouseNumberRules(SimulationSettings simulationSettings)
        {
            int HouseNumberRulesId = _simulationRepo.UploadHouseNumberRules(simulationSettings);
            return HouseNumberRulesId;
        }
        /*public List<SimulationData> GetAllSimulationData()
        {
            var simData = _simulationRepo.GetAllSimulationData();
            return simData;
        }*/
        public SimulationSettings GetSimulationSettingsBySimulationDataID(int simulationDataId)
        {
            SimulationSettings simSettings = _simulationRepo.GetSimulationSettingsBySimulationDataID(simulationDataId);
            return simSettings;
        }
        public SimulationStatistics GetSimulationStatisticsBySimulationDataID(int simulationDataId)
        {
            SimulationStatistics simStatistics = _simulationRepo.GetSimulationStatisticsBySimulationDataID(simulationDataId);
            return simStatistics;
        }
        public List<SimulationOverviewDTO> GetSimulationOverview()
        {
            var simulationOverviews = _simulationRepo.GetAllSimulationOverviews();
            return simulationOverviews;
        }
        public void UploadSelectedMunicipalities(int simulationSettingsId, List<MunicipalitySelection> selections)
        {
            _simulationRepo.UploadSelectedMunicipalities(simulationSettingsId, selections);
        }
        public List<MunicipalitySelection> GetSelectedMunicipalities(int simulationSettingsId, List<Municipality> municipalities)
        {
            var selectedMunicipalities = _simulationRepo.GetSelectedMunicipalities(simulationSettingsId, municipalities);
            return selectedMunicipalities;
        }
        public void UploadMunicipalityStatistics(int simulationStatisticsId, List<MunicipalityStatistics> stats)
        {
            _simulationRepo.UploadMunicipalityStatistics(simulationStatisticsId, stats);
        }
        public SimulationData GetSimulationDataById(int simulationDataId)
        {
            return _simulationRepo.GetSimulationDataById(simulationDataId);
        }
    }
}
