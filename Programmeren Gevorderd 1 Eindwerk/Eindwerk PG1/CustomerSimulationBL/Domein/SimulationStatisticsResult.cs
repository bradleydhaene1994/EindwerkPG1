using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Exceptions;

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
        private SimulationStatistics _general;
        public SimulationStatistics General
        {
            get => _general;
            private set
            {
                if (value == null) throw new SimulationException("SimulationStatisticsResult: general statistics can't be null");
                else _general = value;
            }
        }
        private List<MunicipalityStatistics> _customersPerMunicipality;
        public List<MunicipalityStatistics> CustomersPerMunicipality
        {
            get => _customersPerMunicipality;
            private set
            {
                if (value == null) throw new SimulationException("SimulationStatisticsResult: customers per municipality can't be null");
                else _customersPerMunicipality= value;
            }
        }
        private List<MunicipalityStatistics> _streetsPerMunicipality;
        public List<MunicipalityStatistics> StreetsPerMunicipality
        {
            get => _streetsPerMunicipality;
            private set
            {
                if (value == null) throw new SimulationException("SimulationStatisticsResult: streets per municipality can't be null");
                else _streetsPerMunicipality = value;
            }
        }
        private List<NameStatistics> _maleNames;
        public List<NameStatistics> MaleNames
        {
            get => _maleNames;
            private set
            {
                if (value == null) throw new SimulationException("SimulationStatisticsResult: male names can't be null");
                else _maleNames = value;
            }
        }
        private List<NameStatistics> _femaleNames;
        public List<NameStatistics> FemaleNames
        {
            get => _femaleNames;
            private set
            {
                if (value == null) throw new SimulationException("SimulationStatisticsResult: female names can't be null");
                else _femaleNames = value;
            }
        }
        private List<NameStatistics> _lastNames;
        public List<NameStatistics> LastNames
        {
            get => _lastNames;
            private set
            {
                if (value == null) throw new SimulationException("SimulationStatisticsResult: last names can't be null");
                else _lastNames = value;
            }
        }
    }
}
