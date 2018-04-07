using System;
using System.Data;
using System.Data.Common;

namespace SupHost
{
    /// <summary>
    /// Логи для клиента
    /// </summary>
    class VisClientLogsTableBehavior : ITableBehavior
    {
        private LogConnector connector = LogConnector.CurrentConnector;
        protected DataTable table = null;
        protected DbDataAdapter adapter = null;

        protected string query = "";
        protected string tableName = "";

        public VisClientLogsTableBehavior()
        {
            this.query = "select f_log_id, f_rec_operator, f_log_severety, f_log_class, f_log_message, f_rec_date from vis_log";
            this.tableName = "vis_log";
        }

        public DataTable GetTable()
        {
            ConnectionToDataBaseSetup setup = this.connector.GetDataTable(this.query);
            this.table = setup.Table;
            this.adapter = setup.DataAdapter;
            this.table.TableName = this.tableName;
            return this.table;
        }

        public void InsertRow()
        {
            throw new NotImplementedException("Добавление данных лога не поддерживается");
        }

        public void UpdateRow()
        {
            throw new NotImplementedException("Обновление данных лога не поддерживается");
        }

        public void DeleteRow()
        {
            throw new NotImplementedException("Удаление данных лога не поддерживается");
        }
    }
}
