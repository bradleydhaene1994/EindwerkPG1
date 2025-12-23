using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Interfaces;

namespace CustomerSimulationBL.Managers
{
    public class SimulationManager
    {
        private ISimulationRepository _simulationRepo;

        public SimulationManager(ISimulationRepository simulationRepo)
        {
            _simulationRepo = simulationRepo;
        }
        public void UploadSimulationData(SimulationData simulationData, CountryVersion countryVersion)
        {
            _simulationRepo.UploadSimulationData(simulationData, countryVersion);
        }
        public void UploadSimulationSettings(SimulationSettings simulationSettings)
        {
            _simulationRepo.UploadSimulationSettings(simulationSettings);
        }
        public void UploadSimulationStatistics(SimulationStatistics simulationStatistics)
        {
            _simulationRepo.UploadSimulationStatistics(simulationStatistics);
        }
    }
}
