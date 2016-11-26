using NLog;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.Logger.Enumerations;
using ILogger = WorkFlowBilling.Logger.Services.ILogger;

namespace WorkFlowBilling.Logger.Impl.Services
{
    /// <summary>
    /// Класс для логгирования сообщений
    /// </summary>
    [AutoInjectableInstance]
    public class LogService : ILogger
    {
        private class LogCheckAndSaveTuple
        {
            public LogMessageLevel MessageLevel { get; set; }

            public Func<bool> IsSaveEnabled { get; set; }

            public Action<string> Save { get; set; }
        }

        private NLog.Logger NLogger;

        private const string DefaultLogName = "default";

        private List<LogCheckAndSaveTuple> LogCheckAndSaveTuples { get; set; }

        private void InitLogCheckAndSaveTuples()
        {
            LogCheckAndSaveTuples = new List<LogCheckAndSaveTuple>()
            {
                new LogCheckAndSaveTuple
                {
                    MessageLevel = LogMessageLevel.Trace,
                    IsSaveEnabled = () => { return NLogger.IsTraceEnabled; },
                    Save = (mess) => { NLogger.Trace(mess); }
                },
                new LogCheckAndSaveTuple
                {
                    MessageLevel = LogMessageLevel.Debug,
                    IsSaveEnabled = () => { return NLogger.IsDebugEnabled; },
                    Save = (mess) => { NLogger.Debug(mess); }
                },
                new LogCheckAndSaveTuple
                {
                    MessageLevel = LogMessageLevel.Info,
                    IsSaveEnabled = () => { return NLogger.IsInfoEnabled; },
                    Save = (mess) => { NLogger.Info(mess); }
                },
                new LogCheckAndSaveTuple
                {
                    MessageLevel = LogMessageLevel.Warning,
                    IsSaveEnabled = () => { return NLogger.IsWarnEnabled; },
                    Save = (mess) => { NLogger.Warn(mess); }
                },
                new LogCheckAndSaveTuple
                {
                    MessageLevel = LogMessageLevel.Error,
                    IsSaveEnabled = () => { return NLogger.IsErrorEnabled; },
                    Save = (mess) => { NLogger.Error(mess); }
                },
                new LogCheckAndSaveTuple
                {
                    MessageLevel = LogMessageLevel.Fatal,
                    IsSaveEnabled = () => { return NLogger.IsFatalEnabled; },
                    Save = (mess) => { NLogger.Fatal(mess); }
                }
            };
        }

        public void SaveToLogIfLevelEnabled(LogMessageLevel messageLevel, Func<string> getMessage)
        {
            var log = LogCheckAndSaveTuples.First(_ => _.MessageLevel == messageLevel);

            if (log.IsSaveEnabled())
            {
                var message = getMessage();
                log.Save(message);
            }
        }

        public void SaveToLog(LogMessageLevel messageLevel, string message)
        {
            var log = LogCheckAndSaveTuples.First(_ => _.MessageLevel == messageLevel);
            log.Save(message);
        }

        public void Debug(string message)
        {
            NLogger.Debug(message);
        }

        public void Error(string message)
        {
            NLogger.Error(message);
        }

        public void Fatal(string message)
        {
            NLogger.Fatal(message);
        }

        public void Info(string message)
        {
            NLogger.Info(message);
        }

        public void Trace(string message)
        {
            NLogger.Trace(message);
        }

        public void Warning(string message)
        {
            NLogger.Warn(message);
        }

        public bool IsDebugEnabled
        {
            get { return NLogger.IsDebugEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return NLogger.IsInfoEnabled; }
        }

        public bool IsTraceEnabled
        {
            get { return NLogger.IsTraceEnabled; }
        }

        public bool IsWarnEnabled
        {
           get { return NLogger.IsWarnEnabled; }
        }

        public bool IsErrorEnabled
        {
            get { return NLogger.IsErrorEnabled; }
        }

        public bool IsFatalEnabled
        {
            get { return NLogger.IsFatalEnabled; }
        }

        public LogService(string logName)
        {
            NLogger = LogManager.GetLogger(logName);
            InitLogCheckAndSaveTuples();
        }

        public LogService(): this(DefaultLogName)
        {
        }
    }
}
