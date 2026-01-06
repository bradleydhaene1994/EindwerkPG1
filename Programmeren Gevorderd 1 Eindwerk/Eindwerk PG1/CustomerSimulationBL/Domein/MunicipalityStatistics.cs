using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationBL.Domein
{
    public class MunicipalityStatistics
    {
        public MunicipalityStatistics(Municipality municipality, int count)
        {
            Municipality = municipality;
            Count = count;
        }
        private Municipality _municipality;
        public Municipality Municipality
        {
            get => _municipality;
            private set
            {
                if (value == null) throw new MunicipalityException("No Municipality found");
                else _municipality = value;
            }
        }
        private int _count;
        public int Count
        {
            get => _count;
            private set
            {
                if (value < 0) throw new MunicipalityException("Count cannot be lower than 0");
                else _count = value;
            }
        }
    }
}
