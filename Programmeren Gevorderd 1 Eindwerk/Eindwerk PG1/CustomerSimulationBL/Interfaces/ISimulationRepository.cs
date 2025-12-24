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
        void UploadSimulationSettings(SimulationSettings simulationSettings, int simulationDataId);
        void UploadSimulationStatistics(SimulationStatistics simulationStatistics, int simulationDataId);
    }
}
