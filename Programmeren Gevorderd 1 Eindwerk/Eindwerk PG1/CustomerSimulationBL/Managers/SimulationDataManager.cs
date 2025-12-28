using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
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
        public void UploadSimulationSettings(SimulationSettings simulationSettings, int simulationDataId, int houseNumberRulesId)
        {
            _simulationRepo.UploadSimulationSettings(simulationSettings, simulationDataId, houseNumberRulesId);
        }
        public void UploadSimulationStatistics(SimulationStatistics simulationStatistics, int simulationDataId)
        {
            _simulationRepo.UploadSimulationStatistics(simulationStatistics, simulationDataId);
        }
        public int UploadHouseNumberRules(SimulationSettings simulationSettings)
        {
            int HouseNumberRulesId = _simulationRepo.UploadHouseNumberRules(simulationSettings);
            return HouseNumberRulesId;
        }
        public List<SimulationData> GetAllSimulationData()
        {
            var simData = _simulationRepo.GetAllSimulationData();
            return simData;
        }
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
    }
}
