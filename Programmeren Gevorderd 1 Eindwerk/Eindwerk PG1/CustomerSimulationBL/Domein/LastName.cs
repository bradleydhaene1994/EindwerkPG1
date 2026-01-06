using CustomerSimulationBL.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationBL.Domein
{
    public class LastName
    {
        public LastName(string name, int? frequency, Gender? gender)
        {
            Name = name;
            Frequency = frequency;
            Gender = gender;
        }

        public LastName(int id, string name, int? frequency, Gender? gender)
        {
            Id = id;
            Name = name;
            Frequency = frequency;
            Gender = gender;
        }
        private int _id;
        public int Id
        {
            get => _id;
            private set
            {
                _id = value;
            }
        }
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new NameException("Name of last name cannot be empty");
                else _name = value;
            }
        }
        private int? _frequency;
        public int? Frequency
        {
            get => _frequency;
            set
            {
                if (value < 0) throw new NameException("Frequency of last name cannot be lower than 0");
                else _frequency = value;
            }
        }
        public Gender? _gender;
        public Gender? Gender
        {
            get => _gender;
            set
            {
                if (value != null && !Enum.IsDefined(typeof(Gender), value)) throw new NameException("Invalid gender value in last name.");
                else _gender = value;
            }
        }
        public override string ToString()
        {
            return $"{Name}, {Frequency}, {Gender}";
        }
    }
}
