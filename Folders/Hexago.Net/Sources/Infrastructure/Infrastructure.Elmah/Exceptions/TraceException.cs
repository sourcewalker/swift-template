using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.Exceptions
{
    public class TraceException : System.ApplicationException
    {
        public TraceException(string message) : base($"Trace: {message}") { }
    }
}
