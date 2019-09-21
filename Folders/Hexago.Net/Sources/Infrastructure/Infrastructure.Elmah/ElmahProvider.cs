using Core.Infrastructure.Interfaces.Logging;
using Elmah;
using $safeprojectname$.Exceptions;
using System;
using System.Diagnostics;
using System.Web;

namespace $safeprojectname$
{
    public class ElmahProvider : ILoggingProvider
    {
        public void LogError(string errorMessage, Exception ex)
        {
            try
            {
                var frame = new StackFrame(1, true);
                var method = frame.GetMethod();
                var fileName = method.DeclaringType.Name;

                Error(fileName, method.Name, errorMessage, ex);
            }
            catch (Exception e)
            {
                Log(e);
            }
        }

        private void Error(string filename, string methodname, string errorMessage, Exception exception)
        {
            var ex = new ErrorException(
                $"[{filename}:{methodname}] => {errorMessage} : {exception.Message} {exception.StackTrace}"
            )
            {
                Source = string.Format($"{filename}:{methodname}")
            };

            Log(ex);
        }

        public void LogInfo(string message)
        {
            try
            {
                var frame = new StackFrame(1, true);
                var method = frame.GetMethod();
                var fileName = method.DeclaringType.Name;

                Info(fileName, method.Name, message);
            }
            catch (Exception e)
            {
                Log(e);
            }
        }

        private void Info(string filename, string methodname, string message)
        {
            var ex = new InfoException(
                $"[{filename}:{methodname}] => {message}"
            )
            {
                Source = string.Format($"{filename}:{methodname}")
            };

            Log(ex);
        }

        public void LogTrace(string errorMessage, string dataString)
        {
            try
            {
                var frame = new StackFrame(1, true);
                var method = frame.GetMethod();
                var fileName = method.DeclaringType.Name;

                Trace(fileName, method.Name, errorMessage, dataString);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void Trace(string filename, string methodname, string errorMessage, string dataString)
        {
            var ex = new TraceException(
                $"[{filename}:{methodname}] => {errorMessage} : {dataString}"
            )
            {
                Source = string.Format($"{filename}:{methodname}")
            };

            Log(ex);
        }

        public void LogWarn(string errorMessage, string dataString)
        {
            try
            {
                var frame = new StackFrame(1, true);
                var method = frame.GetMethod();
                var fileName = method.DeclaringType.Name;

                Warn(fileName, method.Name, errorMessage, dataString);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void Warn(string filename, string methodname, string errorMessage, string dataString)
        {
            var ex = new WarnException(
                $"[{filename}:{methodname}] => {errorMessage} : {dataString}"
            )
            {
                Source = string.Format($"{filename}:{methodname}")
            };

            Log(ex);
        }

        private void Log(Exception ex)
        {
            try
            {
                ErrorSignal.FromCurrentContext().Raise(ex);

                Debug.WriteLine(ex.Message);
                if (!string.IsNullOrEmpty(ex.StackTrace))
                    Debug.WriteLine(ex.StackTrace);
            }
            catch (Exception)
            {
                Error err = new Error(ex, HttpContext.Current);
                ErrorLog.GetDefault(HttpContext.Current).Log(err);
            }
        }
    }
}
