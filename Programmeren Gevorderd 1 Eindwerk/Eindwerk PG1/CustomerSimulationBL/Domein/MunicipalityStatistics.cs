using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Domein
{
    public class MunicipalityStatistics
    {
        public MunicipalityStatistics(Municipality municipality, int customerCount)
        {
            Municipality = municipality;
            CustomerCount = customerCount;
        }

        public Municipality Municipality { get; }
        public int CustomerCount { get; }

    }
}
