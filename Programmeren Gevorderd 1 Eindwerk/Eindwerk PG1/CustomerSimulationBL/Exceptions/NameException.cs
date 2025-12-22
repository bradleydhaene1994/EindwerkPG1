using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSimulationBL.Exceptions
{
    public class NameException : Exception
    {
        public NameException()
        {
        }
        public NameException(string? message) : base(message)
        {
        }
        public NameException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
