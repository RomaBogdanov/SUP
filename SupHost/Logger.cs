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

        public void Warn(string message)
        { }

        public void Error(string message)
        { }

        #endregion

        #region Private

        private Logger() { }

        #endregion
    }
}
