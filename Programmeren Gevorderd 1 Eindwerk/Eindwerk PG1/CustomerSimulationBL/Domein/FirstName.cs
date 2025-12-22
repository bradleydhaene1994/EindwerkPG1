using CustomerSimulationBL.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Domein
{
    public class FirstName
    {
        public FirstName(string name, int? frequency, Gender gender)
        {
            Name = name;
            Frequency = frequency;
            Gender = gender;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Frequency { get; set; }
        public Gender Gender { get; set; }
        public override string ToString()
        {
            return $"{Name}, {Frequency}, {Gender}";
        }
    }
}
