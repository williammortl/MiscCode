namespace WCFExample.Logging
{
    using Microsoft.Practices.EnterpriseLibrary.Logging;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Logger abstraction, wraps the EnterpriseLibrary
    /// </summary>
    public static class Logger
    {

        /// <summary>
        /// The title for log entries
        /// </summary>
        private const String LOG_TITLE = "WCFExample Service Log Entry";

        /// <summary>
        /// The priority for all logging messages
        /// </summary>
        private const int LOG_PRIORITY = int.MaxValue;

        /// <summary>
        /// The log writer
        /// </summary>
        private static LogWriter logWriter = null;

        /// <summary>
        /// Log a trace (verbose) message
        /// </summary>
        /// <param name="message">the message to log</param>
        public static void LogTrace(String message)
        {
            Logger.LogMessage(message, TraceEventType.Verbose);
        }

        /// <summary>
        /// Log an informational message
        /// </summary>
        /// <param name="message">the message to log</param>
        public static void LogInfo(String message)
        {
            Logger.LogMessage(message, TraceEventType.Information);
        }

        /// <summary>
        /// Log a warning
        /// </summary>
        /// <param name="message">the message to log</param>
        public static void LogWarning(String message)
        {
            Logger.LogMessage(message, TraceEventType.Warning);
        }

        /// <summary>
        /// Log an error
        /// </summary>
        /// <param name="message">the message to log</param>
        public static void LogError(String message)
        {
            Logger.LogMessage(message, TraceEventType.Error);
        }

        /// <summary>
        /// Log critical message
        /// </summary>
        /// <param name="message">the message to log</param>
        public static void LogCritical(String message)
        {
            Logger.LogMessage(message, TraceEventType.Critical);
        }

        /// <summary>
        /// Actually log the messgae
        /// </summary>
        /// <param name="message">the message to log</param>
        /// <param name="severity">log entry severity</param>
        public static void LogMessage(String message, TraceEventType severity)
        {

            // build the log entry
            LogEntry le = new LogEntry();
            le.Message = message;
            le.ProcessName = Process.GetCurrentProcess().ProcessName;
            le.ProcessId = Process.GetCurrentProcess().Id.ToString();
            le.TimeStamp = DateTime.Now;
            le.MachineName = Environment.MachineName;
            le.Priority = Logger.LOG_PRIORITY;
            le.Title = Logger.LOG_TITLE;
            le.Severity = severity;

            // actually log the message
            if (Logger.logWriter == null)
            {
                LogWriterFactory logWriterFactory = new LogWriterFactory();
                Logger.logWriter = logWriterFactory.Create();
            }
            Logger.logWriter.Write(le);

            // write the message to the console
            Console.WriteLine(String.Format("[{0}] {1} - {2}", DateTime.Now.ToString(), severity.ToString().ToUpper(), message));
        }
    }
}
