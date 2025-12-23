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
        void UploadSimulationData(SimulationData simulationData, CountryVersion countryVersion);
        void UploadSimulationSettings(SimulationSettings simulationSettings);
        void UploadSimulationStatistics(SimulationStatistics simulationStatistics);
    }
}
