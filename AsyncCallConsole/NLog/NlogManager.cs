using AsyncCallConsole.NLog;
using NLog;

using System;

namespace AsyncCallConsole
{
    public static class NlogManager
    {
        private static readonly Logger logManager = LogManager.GetCurrentClassLogger();

        public static void Log(LogType level, string message, Exception exception)
        {
            switch (level)
            {
                case LogType.Error:
                    logManager.Log(LogLevel.Error, message, exception);
                    break;
                case LogType.Info:
                    logManager.Log(LogLevel.Info, message, exception);
                    break;
                case LogType.Warning:
                    logManager.Log(LogLevel.Warn, message, exception);
                    break;
                case LogType.Trace:
                    logManager.Log(LogLevel.Trace, message, exception);
                    break;
            }
            if (exception.InnerException != null)
            {
                Log(level, message, exception.InnerException);
            }
        }

        public static void Log(LogType level, string message)
        {
            switch (level)
            {
                case LogType.Error:
                    logManager.Log(LogLevel.Error, message);
                    break;
                case LogType.Info:
                    logManager.Log(LogLevel.Info, message);
                    break;
                case LogType.Warning:
                    logManager.Log(LogLevel.Warn, message);
                    break;
                case LogType.Trace:
                    logManager.Log(LogLevel.Trace, message);
                    break;
            }
        }

        public static void Log(LogType level, string message, LogMessageGenerator logMessage)
        {
            switch (level)
            {
                case LogType.Error:
                    logManager.Log(LogLevel.Error, message, logMessage);
                    break;
                case LogType.Info:
                    logManager.Log(LogLevel.Info, message);
                    break;
                case LogType.Warning:
                    logManager.Log(LogLevel.Warn, message);
                    break;
                case LogType.Trace:
                    logManager.Log(LogLevel.Trace, message);
                    break;

            }
        }

    }
}