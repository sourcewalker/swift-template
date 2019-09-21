using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.Exceptions
{
    public class WarnException : System.ApplicationException
    {
        public WarnException(string message) : base($"Warn: {message}") { }
    }
}
