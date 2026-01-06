using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationBL.Domein
{
    public class NameStatistics
    {
        public NameStatistics(string name, int count)
        {
            Name = name;
            this.Count = count;
        }
        private string _name;
        public string Name
        {
            get => _name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new SimulationException("NameStatistics: Name cannot be empty");
                else _name = value;
            }
        }
        private int _count;
        public int Count
        {
            get => _count;
            private set
            {
                if (value < 0) throw new SimulationException("NameStatistics: Count cannot be lower than 0");
                else _count = value;
            }
        }
    }
}
