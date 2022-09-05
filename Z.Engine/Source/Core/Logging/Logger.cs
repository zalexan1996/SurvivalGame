using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Engine.Source.Interfaces;

namespace Z.Engine.Source.Core.Logging
{
    /// <summary>
    /// Basic logger implementation
    /// TODO: Look into Serilog
    /// </summary>
    public class Logger : ILogger
    {
        private readonly LogLevels _minDebugLevel;
        private readonly LogLevels _minReleaseLevel;

        public Logger(LogLevels minDebugLevel = LogLevels.Info, LogLevels minReleaseLevel = LogLevels.Error)
        {
            _minDebugLevel = minDebugLevel;
            _minReleaseLevel = minReleaseLevel;
        }

        protected void Log(string message, LogLevels logLevel)
        {
#if DEBUG
            if (logLevel >= _minDebugLevel)
            {
                Console.WriteLine($"{logLevel.ToString()} : {message}");
            }
#else
            if (logLevel >= _minReleaseLevel)
            {
                Console.WriteLine($"{logLevel.ToString()} : {message}");
            }
#endif
        }
        public void Error(string message)
        {
            Log(message, LogLevels.Error);
        }

        public void Info(string message)
        {
            Log(message, LogLevels.Info);
        }

        public void Warning(string message)
        {
            Log(message, LogLevels.Warning);
        }
    }
}
