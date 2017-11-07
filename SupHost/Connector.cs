using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Common;

namespace SupHost
{
    /// <summary>
    /// Реализует подключение к БД.
    /// </summary>
    /// <remarks>
    /// Класс должен реализовать паттерн синглтон для доступа из всех
    /// участков кода к БД через единое подключение.
    /// <div>Функционал класса:</div>
    /// <ul>
    ///     <li>Организация подключения к БД;</li>
    ///     <li>Получение данных по запросу из БД;</li>
    ///     <li>Добавление данных в таблицу БД;</li>
    ///     <li>Редактирование данных в таблице БД;</li>
    ///     <li>Удаление данных из таблицы БД.</li>
    ///     <li></li>
    /// </ul>
    /// </remarks>
    public class Connector
    {
        private static Connector connector;

        private Logger logger;

        private SqlConnection connection;

        #region Public

        public static Connector CurrentConnector
        {
            get
            {
                if (connector == null)
                {
                    connector = new Connector();
                    return connector;
                }
                return connector;
            }
        }

        public DataTable GetDataTable(string query)
        {
            DataTable dt = new DataTable();
            this.connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, connection);
            da.Fill(dt);
            this.connection.Close();
            return dt;
        }

        /*public void SelectData(string query)
        {
            this.connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader a = command.ExecuteReader();
            while (a.Read())
            {
                for (int i = 0; i < a.FieldCount; i++)
                {
                    Console.Write(a.GetString(i) + "\t");
                    if (i == a.FieldCount - 1)
                    {
                        Console.WriteLine();
                    }
                }
            }
            this.connection.Close();
        }

        /// <summary>
        /// Отправка запросов для редактирования данных в БД.
        /// </summary>
        /// <param name="query"></param>
        public void ChangeData(string query)
        {
            this.connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
        }*/

        #endregion

        #region Private

        private Connector()
        {
            this.logger = Logger.CurrentLogger;
            string connectionString;
            if (ConfigurationManager.ConnectionStrings.Count != 0)
            {
                connectionString = ConfigurationManager.ConnectionStrings["BaseConnection"].ConnectionString;
            }
            else
            {
                this.logger.Warn("Строка поключения отсутствует в файле конфигурации системы");
                return;
            }
            this.connection = new SqlConnection(connectionString);
        }

        #endregion
    }
}
