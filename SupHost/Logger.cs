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
                    bool.TryParse(ConfigurationManager.AppSettings["dBlog"], out logger.dbLog);
                    if (logger.dbLog)
                    {
                        logger.logTableWrapper = LogTableWrapper.GetLogTableWrapper();
                    }
                    return logger;
                }
                return logger;
            }
        }

        public void Debug(string message)
        {
            Write(message, "DEBUG", ConsoleColor.DarkMagenta);
        }

        public void Info(string message)
        {
            Write(message, "INFO", ConsoleColor.Green);
        }

        public void Warn(string message)
        {
            Write(message, "WARN", ConsoleColor.Yellow);
        }

        public void Error(string message)
        {
            Write(message, "ERROR", ConsoleColor.Red);
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

        private void Write(string message, string severity, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("{0}  {1}: {2}", DateTime.Now, severity, message);
            Console.ResetColor();
            if (dbLog)
            {
                // Пишем в базу в отдельном потоке, чтобы не блокировать консольный ввод
                var thread = new Thread(LogToDb);
                thread.Start(new LogData
                {
                    Date = DateTime.Now,
                    Severity = severity,
                    Message = message
                });
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
