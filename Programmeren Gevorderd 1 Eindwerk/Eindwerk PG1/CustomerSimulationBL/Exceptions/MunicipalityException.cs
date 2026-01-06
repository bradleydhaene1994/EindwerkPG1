using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Exceptions
{
    public class MunicipalityException : Exception
    {
        public MunicipalityException()
        {
        }
        public MunicipalityException(string? message) : base(message)
        {
        }
        public MunicipalityException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
