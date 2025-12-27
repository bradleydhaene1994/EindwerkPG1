using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;

namespace CustomerSimulationBL.Interfaces
{
    public interface ISimulationRepository
    {
        void UploadSimulationData(SimulationData simulationData, int countryVersionId);
        void UploadSimulationSettings(SimulationSettings simulationSettings, int simulationDataId, int houseNumberRulesId);
        void UploadSimulationStatistics(SimulationStatistics simulationStatistics, int simulationDataId);
        List<SimulationData> GetAllSimulationData();
        SimulationSettings GetSimulationSettingsBySimulationDataID(int simulationDataId);
        SimulationStatistics GetSimulationStatisticsBySimulationDataID(int simulationDataId);
    }
}
