using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerSimulationBL.Interfaces;
using CustomerSimulationDL.FileReaders;

namespace CustomerSimulationUtils
{
    public static class FileReaderFactory
    {
        public static ICsvReader GeefCsvReader()
        {
            return new CsvFileReader();
        }
        public static IJsonReader GeefJsonReader()
        {
            return new JsonFileReader();
        }
        public static ITxtReader GeefTxtReader()
        {
            return new TxtFileReader();
        }
    }
}
