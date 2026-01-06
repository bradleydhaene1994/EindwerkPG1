using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Exceptions;

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
        private SimulationData _simulationData;
        public SimulationData SimulationData
        {
            get => _simulationData;
            private set
            {
                if (value == null) throw new SimulationException("SimulationExport: simulationdata is null.");
                else _simulationData = value;
            }
        }
        private SimulationSettings _simulationSettings;
        public SimulationSettings SimulationSettings
        {
            get => _simulationSettings;
            private set
            {
                if (value == null) throw new SimulationException("SimulationExport: simulationsettings are null");
                else _simulationSettings = value;
            }
        }
        public SimulationStatisticsResult _simulationStatisticsResult;
        public SimulationStatisticsResult SimulationStatisticsResult
        {
            get => _simulationStatisticsResult;
            set
            {
                if (value == null) throw new SimulationException("SimulationExport: simulationstatisticsresult is null");
                else _simulationStatisticsResult = value;
            }
        }
    }
}
