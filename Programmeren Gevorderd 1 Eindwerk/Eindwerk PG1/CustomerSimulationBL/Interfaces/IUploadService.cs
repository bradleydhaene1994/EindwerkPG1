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
        void Upload(string filePath, int year, UploadDataType dataType, int countryId, IProgress<int> progress, string countryName);
        bool DataAlreadyExists(int countryVersionId, UploadDataType dataType);
    }
}
