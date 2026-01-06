using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationBL.Domein
{
    public class SimulationSettings
    {
        public SimulationSettings(List<MunicipalitySelection> selectedMunicipalities, int totalCustomers, int minAge, int maxAge, int minNumber, int maxNumber, bool hasLetters, int percentageLetters)
        {
            SelectedMunicipalities = selectedMunicipalities;
            TotalCustomers = totalCustomers;
            MinAge = minAge;
            MaxAge = maxAge;
            MinNumber = minNumber;
            MaxNumber = maxNumber;
            HasLetters = hasLetters;
            PercentageLetters = percentageLetters;
        }

        public SimulationSettings(int id, List<MunicipalitySelection> selectedMunicipalities, int totalCustomers, int minAge, int maxAge, int minNumber, int maxNumber, bool hasLetters, int percentageLetters)
        {
            Id = id;
            SelectedMunicipalities = selectedMunicipalities;
            TotalCustomers = totalCustomers;
            MinAge = minAge;
            MaxAge = maxAge;
            MinNumber = minNumber;
            MaxNumber = maxNumber;
            HasLetters = hasLetters;
            PercentageLetters = percentageLetters;
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
        private List<MunicipalitySelection> _selectedMunicipalities;
        public List<MunicipalitySelection> SelectedMunicipalities
        {
            get => _selectedMunicipalities;
            private set
            {
                if (value != null && value.Any(m => m == null)) throw new SimulationException("SimulationSettings= selected municipalities are null");
                else _selectedMunicipalities = value;
            }
        }
        private int _totalCustomers;
        public int TotalCustomers
        {
            get => _totalCustomers;
            private set
            {
                _totalCustomers = value;
            }
        }
        private int _minAge;
        public int MinAge
        {
            get => _minAge;
            private set
            {
                if (value < 0) throw new SimulationException("SimulationSettings: minimum age cannot be lower than 0");
                else _minAge = value;
            }
        }
        private int _maxAge;
        public int MaxAge
        {
            get => _maxAge;
            private set
            {
                if (value < _minAge) throw new SimulationException("SimulationSettings: maximum age cannot be smaller than minimum age");
                else _maxAge = value;
            }
        }
        private int _minNumber;
        public int MinNumber
        {
            get => _minNumber;
            private set
            {
                if (value <= 0) throw new SimulationException("SimulationSettings: minimum house number cannot be lower than 0");
                else _minNumber = value;
            }
        }
        private int _maxNumber;
        public int MaxNumber
        {
            get => _maxNumber;
            private set
            {
                if (value < _minNumber) throw new SimulationException("SimulationSettings: maximum house number cannot be lower than minimum house number");
                else _maxNumber = value;
            }
        }
        private bool _hasLetters;
        public bool HasLetters
        {
            get => _hasLetters;
            private set
            {
                _hasLetters = value;
            }
        }
        private int _percentageLetters;
        public int PercentageLetters
        {
            get => _percentageLetters;
            private set
            {
                if (value < 0 || value > 100) throw new SimulationException("SimulationSettings: percentage must be between 0 and 100.");
                else _percentageLetters = value;
            }
        }

        public string HouseNumberRulesToString()
        {
            return $"Minimum number: {MinNumber}, Maximum number: {MaxNumber}, Has Letters: {HasLetters}, Percentage appearance letters in housenumbers: {PercentageLetters}%";
        }

        public void SetSelectedMunicipalities(List<MunicipalitySelection> selections)
        {
            SelectedMunicipalities = selections;
        }
    }
}
