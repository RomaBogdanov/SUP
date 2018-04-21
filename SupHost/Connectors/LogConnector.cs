using System.Configuration;

namespace SupHost.Connectors
{
    /// <summary>
    /// Реализует подключение к БД лога.
    /// </summary>
    class LogConnector : Connector
    {
        private static Connector connector;

        public static Connector CurrentConnector
        {
            get
            {
                if (connector == null)
                {
                    connector = new LogConnector();
                    return connector;
                }
                return connector;
            }
        }
 
        /// <summary>
        /// Получение строки подключения к БД.
        /// </summary>
        /// <returns></returns>
        protected override string GetConnectionString()
        {
            string connectionString;
            if (ConfigurationManager.ConnectionStrings.Count != 0)
            {
                connectionString = ConfigurationManager
                    .ConnectionStrings["LogsConnection"].ConnectionString;
            }
            else
            {
                connectionString = "";
            }
            return connectionString;
        }

        protected override void LogInfo(string message)
        {
        }

        protected override void LogError(string message)
        {
        }
    }
}
