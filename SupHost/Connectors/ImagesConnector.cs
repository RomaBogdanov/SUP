using System.Configuration;

namespace SupHost.Connectors
{
    /// <summary>
    /// Реализует подключение к БД images.
    /// </summary>
    class ImagesConnector : Connector
    {
        private static Connector connector;

        public static Connector CurrentConnector
        {
            get
            {
                if (connector == null)
                {
                    connector = new ImagesConnector();
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
                    .ConnectionStrings["ImagesConnection"].ConnectionString;
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
