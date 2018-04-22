using SupContract;
using SupHost.Data;
using System;
using System.Configuration;
using System.Threading;

namespace SupHost
{
    /// <summary>
    /// Инкапсюлирует вопросы логгирования сообщений системы, в частности:
    /// ошибки, предупреждения, сообщения.
    /// </summary>
    /// <remarks>Реализуется через паттерн Одиночка.</remarks>
    class Logger
    {
        private static Logger logger;

        private bool dbLog;

        LogTableWrapper logTableWrapper;

        #region Public

        public static Logger CurrentLogger
        {
            get
            {
                if (logger == null)
                {
                    logger = new Logger();
                    bool.TryParse(ConfigurationManager.AppSettings["dBlog"],
                        out logger.dbLog);
                    if (logger.dbLog)
                    {
                        logger.logTableWrapper =
                            (LogTableWrapper)LogTableWrapper.GetTableWrapper(
                                TableName.VisClientLogs);
                    }
                    return logger;
                }
                return logger;
            }
        }

        public void Debug(string message, OperationInfo info = null)
        {
            Write(new LogData
            {
                Date = DateTime.Now,
                Severity = "DEBUG",
                Message = message,
                Class = new System.Diagnostics.StackTrace().ToString(),
                User = info != null ? info.Id : -1,
                Machine = info != null ? info.Machine : "",
            }, ConsoleColor.DarkMagenta);
        }

        public void Info(string message, OperationInfo info = null)
        {
            Write(new LogData
            {
                Date = DateTime.Now,
                Severity = "INFO",
                Message = message,
                Class = new System.Diagnostics.StackTrace().ToString(),
                User = info != null ? info.Id : -1,
                Machine = info != null ? info.Machine : "",
            }, ConsoleColor.Green);
        }

        public void Warn(string message, OperationInfo info = null)
        {
            Write(new LogData
            {
                Date = DateTime.Now,
                Severity = "WARN",
                Message = message,
                Class = new System.Diagnostics.StackTrace().ToString(),
                User = info != null ? info.Id : -1,
                Machine = info != null ? info.Machine : "",
            }, ConsoleColor.Yellow);
        }

        public void Error(string message, OperationInfo info = null)
        {
            Write(new LogData
            {
                Date = DateTime.Now,
                Severity = "ERROR",
                Message = message,
                Class = new System.Diagnostics.StackTrace().ToString(),
                User = info != null ? info.Id : -1,
                Machine = info != null ? info.Machine : "",
            }, ConsoleColor.Red);
        }

        public void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0}  ERROR: {1}", DateTime.Now, message);
            Console.ResetColor();
        }

        #endregion

        #region Private

        private Logger() { }

        private void Write(LogData logData, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("{0}  {1}: {2}", DateTime.Now, logData.Severity,
                logData.Message);
            Console.ResetColor();
            if (dbLog)
            {
                // Пишем в базу в отдельном потоке, чтобы не блокировать консольный ввод
                var thread = new Thread(LogToDb);
                thread.Start(logData);
            }
        }

        private void LogToDb(object logData)
        {
            if (logData is LogData)
            {
                logTableWrapper.Write(logData as LogData);
            }
        }

        #endregion
    }
}
