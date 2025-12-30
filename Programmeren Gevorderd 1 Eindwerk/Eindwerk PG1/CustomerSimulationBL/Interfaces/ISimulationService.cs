using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.DTOs;

namespace CustomerSimulationBL.Interfaces
{
    public interface ISimulationService
    {
        void RunSimulation(SimulationData simData, SimulationSettings simSettings, int countryVersionId, List<Municipality> municipalities);
    }
}
