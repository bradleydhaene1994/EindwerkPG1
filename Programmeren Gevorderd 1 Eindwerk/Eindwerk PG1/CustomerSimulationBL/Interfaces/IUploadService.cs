using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Domein;
using CustomerSimulationBL.Enumerations;

namespace CustomerSimulationBL.Interfaces
{
    public interface IUploadService
    {
        void Upload(string filePath, CountryVersion countryVersion, UploadDataType dataType, int countryId, IProgress<int> progress);
    }
}
