using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Interfaces;
using CustomerSimulationDL.FileReaders;

namespace CustomerSimulationUtils
{
    public class FileReaderFactory
    {
        public ICsvReader GetCsvReader()
        {
            return new CsvFileReader();
        }
        public IJsonReader GetJsonReader()
        {
            return new JsonFileReader();
        }
        public ITxtReader GetTxtReader()
        {
            return new TxtFileReader();
        }
    }
}
