using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;

namespace CustomerSimulationBL.Managers
{
    public class CustomersetSimulationService
    {
        private readonly AddressManager _addressmanager;
        private readonly MunicipalityManager _municipalitymanager;
        private readonly NameManager _namemanager;

        private readonly Random _random = new Random();

        public List<Customer> CustomerSetSimulation(SimulationSettings simulationSettings, int countryVersionId)
        {
            throw new NotImplementedException();
        }
    }
}
