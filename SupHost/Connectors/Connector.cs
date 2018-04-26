using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace SupHost.Connectors
{
    /// <summary>
    /// Реализует подключение к БД.
    /// </summary>
    /// <remarks>
    /// Класс должен реализовать паттерн синглтон для доступа из всех
    /// участков кода к БД через единое подключение.
    /// TODO: синглтон отменяется. Убрать все упоминания о нём.
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
    abstract class Connector
    {
        private SqlConnection connection;

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

        public bool ConnectionAttempt()
        {
            //ConnectionState a = this.connection.State;
            try
            {
                this.connection.Open();
                this.connection.Close();
                return true;
            }
            catch (Exception err)
            {
                LogError(err.Message);
                return false;
            }
        }

        public override string ToString()
        {
            return GetConnectionString();
        }

        protected Logger Logger { get; private set; }

        /// <summary>
        /// Получение строки подключения к БД.
        /// </summary>
        /// <returns></returns>
        protected abstract string GetConnectionString();

        protected Connector()
        {
            this.Logger = Logger.CurrentLogger;
            string connectionString = this.GetConnectionString();
            this.connection = new SqlConnection(connectionString);
            this.connection.InfoMessage += Connection_InfoMessage;
            this.connection.StateChange += Connection_StateChange;
        }

        protected virtual void LogInfo(string message)
        {
            this.Logger.Info(message);
        }

        protected virtual void LogError(string message)
        {
            this.Logger.Error(message);
        }

        private void Connection_StateChange(object sender, StateChangeEventArgs e)
        {
            LogInfo(e.CurrentState.ToString());
        }

        private void Connection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            LogError(e.Message);
        }
    }
}
