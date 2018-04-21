using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace SupHost.Connectors
{
    /// <summary>
    /// Реализует подключение к БД images.
    /// </summary>
    class ImagesConnector
    {
        private static ImagesConnector connector;

        private SqlConnection connection;

        public static ImagesConnector CurrentConnector
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

        public ConnectionToDataBaseSetup GetDataTable(string query)
        {
            DataTable dt = new DataTable();
            this.connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, connection);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Fill(dt);
            this.connection.Close();
            return new ConnectionToDataBaseSetup()
            { Table = dt, DataAdapter = da };
        }

        public void UpdateTable(DataTable dataTable, DbDataAdapter adapter)
        {
            this.connection.Open();
            adapter.Update(dataTable);
            this.connection.Close();
        }

        private ImagesConnector()
        {
            string connectionString = this.GetConnectionString();
            this.connection = new SqlConnection(connectionString);
            this.connection.InfoMessage += Connection_InfoMessage;
            this.connection.StateChange += Connection_StateChange;
        }

        private void Connection_StateChange(object sender, StateChangeEventArgs e)
        {
            // TODO
        }

        private void Connection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            // TODO
        }

        /// <summary>
        /// Получение строки подключения к БД.
        /// </summary>
        /// <returns></returns>
        private string GetConnectionString()
        {
            string connectionString;
            if (ConfigurationManager.ConnectionStrings.Count != 0)
            {
                connectionString = ConfigurationManager
                    .ConnectionStrings["ImagesConnection"].ConnectionString;
            }
            else
            {
                connectionString = "";
            }
            return connectionString;
        }
    }
}
