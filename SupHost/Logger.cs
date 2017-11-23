using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #region Public

        public static Logger CurrentLogger
        {
            get
            {
                if (logger == null)
                {
                    logger = new Logger();
                    return logger;
                }
                return logger;
            }
        }

        public void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("{0}  INFO: {1}", DateTime.Now, message);
            Console.ResetColor();
        }

        public void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{0}  WARN: {1}", DateTime.Now, message);
            Console.ResetColor();
        }

        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0}  ERROR: {1}", DateTime.Now, message);
            Console.ResetColor();
        }

        #endregion

        #region Private

        private Logger() { }

        #endregion
    }
}
