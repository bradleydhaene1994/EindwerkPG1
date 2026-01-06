using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using CustomerSimulationBL.Exceptions;

namespace CustomerSimulationBL.Domein
{
    public class MunicipalitySelection
    {
        public MunicipalitySelection(Municipality municipality, int percentage, bool isSelected)
        {
            Municipality = municipality;
            Percentage = percentage;
            IsSelected = isSelected;
        }
        private Municipality _municipality;
        public Municipality Municipality
        {
            get => _municipality;
            set
            {
                if (value == null) throw new MunicipalityException("No municipality selected");
                else _municipality = value;
            }
        }
        private int _percentage;
        public int Percentage
        {
            get => _percentage;
            set
            {
                if (value < 0 || value > 100) throw new MunicipalityException("percentage must be between 0 and 100");
                else _percentage = value;
            }
        }
        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
            }
        }
    }
}
