using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Domein
{
    public class NameStatistics
    {
        public NameStatistics(string name, int count)
        {
            Name = name;
            this.Count = count;
        }

        public string Name { get; }
        public int Count { get; }
    }
}
