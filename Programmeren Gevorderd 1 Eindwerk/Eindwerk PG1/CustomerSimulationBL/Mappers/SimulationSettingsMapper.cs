using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.DTOs;

namespace CustomerSimulationBL.Mappers
{
    public class SimulationSettingsMapper
    {
        public static SimulationSettings ToDomain(SimulationSettingsDTO dto)
        {
            if(dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            SimulationSettings domain = new SimulationSettings(dto.Id, dto.SelectedMunicipalities, dto.TotalCustomers, dto.MinAge, dto.MaxAge, dto.minNumber, dto.maxNumber, dto.HasLetters, dto.PercentageLetters);

            return domain;
        }
        public static SimulationSettingsDTO ToDTO(SimulationSettings domain)
        {
            if(domain == null)
            {
                throw new ArgumentNullException(nameof(domain));
            }

            SimulationSettingsDTO dto = new SimulationSettingsDTO(domain.SelectedMunicipalities, domain.TotalCustomers, domain.MinAge, domain.MaxAge, domain.MinNumber, domain.MaxNumber, domain.HasLetters, domain.PercentageLetters);

            return dto;

        }
    }
}
