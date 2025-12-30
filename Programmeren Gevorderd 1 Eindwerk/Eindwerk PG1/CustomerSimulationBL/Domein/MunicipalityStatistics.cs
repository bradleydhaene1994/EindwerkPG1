using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Domein
{
    public class MunicipalityStatistics
    {
        public MunicipalityStatistics(Municipality municipality, int count)
        {
            Municipality = municipality;
            Count = count;
        }

        public Municipality Municipality { get; }
        public int Count { get; }

    }
}
