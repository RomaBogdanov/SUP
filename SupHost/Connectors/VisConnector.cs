using System.Configuration;

namespace SupHost.Connectors
{
    /// <summary>
    /// Реализует подключение к основной БД.
    /// </summary>
    class VisConnector : Connector
    {
        private static Connector connector;

        public static Connector CurrentConnector
        {
            get
            {
                if (connector == null)
                {
                    connector = new VisConnector();
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
            //ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            if (ConfigurationManager.ConnectionStrings.Count != 0)
            {
                connectionString = ConfigurationManager
                    .ConnectionStrings["BaseConnection"].ConnectionString;
            }
            else
            {
                this.Logger.Warn("Строка поключения отсутствует в файле конфигурации системы");
                connectionString = "";
            }
            return connectionString;
        }
    }
}
