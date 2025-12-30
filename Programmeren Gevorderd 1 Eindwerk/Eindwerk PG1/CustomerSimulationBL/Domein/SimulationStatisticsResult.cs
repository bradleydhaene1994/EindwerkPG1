using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Domein
{
    public class SimulationStatisticsResult
    {
        public SimulationStatisticsResult(SimulationStatistics general, List<MunicipalityStatistics> customersPerMunicipality, List<MunicipalityStatistics> streetsPerMunicipality, List<NameStatistics> maleNames, List<NameStatistics> femaleName, List<NameStatistics> lastNames)
        {
            General = general;
            CustomersPerMunicipality = customersPerMunicipality;
            StreetsPerMunicipality = streetsPerMunicipality;
            MaleNames = maleNames;
            FemaleNames = femaleName;
            LastNames = lastNames;
        }

        public SimulationStatistics General { get; }
        public List<MunicipalityStatistics> CustomersPerMunicipality { get; }
        public List<MunicipalityStatistics> StreetsPerMunicipality { get; }
        public List<NameStatistics> MaleNames { get; }
        public List<NameStatistics> FemaleNames { get; }
        public List<NameStatistics> LastNames { get; }
    }
}
