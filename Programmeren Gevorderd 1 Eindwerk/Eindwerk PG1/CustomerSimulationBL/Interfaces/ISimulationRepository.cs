using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.DTOs;

namespace CustomerSimulationBL.Interfaces
{
    public interface ISimulationRepository
    {
        int UploadSimulationData(SimulationData simulationData, int countryVersionId);
        int UploadSimulationSettings(SimulationSettings simulationSettings, int simulationDataId, int houseNumberRulesId);
        int UploadSimulationStatistics(SimulationStatistics simulationStatistics, int simulationDataId);
        int UploadHouseNumberRules(SimulationSettings simulationSettings);
        void UploadSelectedMunicipalities(int simulationSettingsId, List<MunicipalitySelection> municipalitySelections);
        List<SimulationOverviewDTO> GetAllSimulationOverviews();
        SimulationSettings GetSimulationSettingsBySimulationDataID(int simulationDataId);
        SimulationStatistics GetSimulationStatisticsBySimulationDataID(int simulationDataId);
        public List<MunicipalitySelection> GetSelectedMunicipalities(int simulationSettingsId, List<Municipality> municipalities);
        public void UploadMunicipalityStatistics(int simulationStatisticsId, List<MunicipalityStatistics> stats);
        public SimulationData GetSimulationDataById(int simulationDataId);
    }
}
