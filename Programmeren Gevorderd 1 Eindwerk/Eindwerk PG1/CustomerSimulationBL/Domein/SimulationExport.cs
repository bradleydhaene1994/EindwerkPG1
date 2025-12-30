using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Domein
{
    public class SimulationExport
    {
        public SimulationExport(SimulationData simulationData, SimulationSettings simulationSettings, SimulationStatisticsResult simulationStatisticsResult)
        {
            SimulationData = simulationData;
            SimulationSettings = simulationSettings;
            SimulationStatisticsResult = simulationStatisticsResult;
        }

        public SimulationData SimulationData { get; }
        public SimulationSettings SimulationSettings { get; }
        public SimulationStatisticsResult SimulationStatisticsResult { get; }
    }
}
