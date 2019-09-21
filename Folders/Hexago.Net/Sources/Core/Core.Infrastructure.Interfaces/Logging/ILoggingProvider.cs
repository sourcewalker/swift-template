using System;

namespace $safeprojectname$.Logging
{
    public interface ILoggingProvider
    {
        void LogInfo(string message);

        void LogTrace(string errorMessage, string dataString);

        void LogWarn(string errorMessage, string dataString);

        void LogError(string errorMessage, Exception ex);
    }
}
