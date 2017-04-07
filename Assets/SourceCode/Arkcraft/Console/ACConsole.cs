using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arkcraft.Console
{
    public class ACConsole : IACConsoleListener
    {
        private StringBuilder log = new StringBuilder("");
        private string logCopy = null;

        public string TextLog
        {
            get 
            {
                if (logCopy == null)
                    logCopy = log.ToString();

                return logCopy;
            }
        }

        public IACConsoleListener listener;
        public enum LogLevel
        {
            Info,
            Warning,
            Error
        }

        private ACConsole()
        {
        }

        static private ACConsole singleton;

        static public ACConsole Singleton
        {
            get { if (singleton == null) singleton = new ACConsole(); return singleton; }
        }

        public void Log(ACConsole.LogLevel level, string message)
        {
            log.AppendLine(level.ToString() + " : " + message);
            logCopy = null;

            if (listener != null)
                listener.Log(level, message);
        }

        static public void LogError(string message)
        {
            Singleton.Log(LogLevel.Error, message);
        }

        static public void LogWarning(string message)
        {
            Singleton.Log(LogLevel.Warning, message);
        }

        static public void LogInfo(string message)
        {
            Singleton.Log(LogLevel.Info, message);
        }
    }
}
