using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Interfaces;

namespace CustomerSimulationDL.Repositories
{
    public class NameRepository : INameRepository
    {
        private string _connectionstring;

        public NameRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public void UploadFirstName(List<FirstName> firstNames, int countryId)
        {

        }
        public void UploadLastName(List<LastName> lastNames, int countryId)
        {

        }
    }
}
