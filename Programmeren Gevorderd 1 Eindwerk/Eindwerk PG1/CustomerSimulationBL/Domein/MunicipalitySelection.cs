using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

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

        public Municipality Municipality { get; set; }
        public int Percentage { get; set; }
        public bool IsSelected { get; set; }
    }
}
