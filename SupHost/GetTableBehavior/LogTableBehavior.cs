using System;
using System.Data;
using System.Data.Common;

namespace SupHost
{
    class LogTableBehavior : ITableBehavior
    {
        private LogConnector connector = LogConnector.CurrentConnector;
        protected DataTable table = null;
        protected DbDataAdapter adapter = null;

        protected string query = "";
        protected string tableName = "";

        public LogTableBehavior()
        {
            this.query = "select * from vis_log";
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
            this.connector.UpdateTable(this.table, this.adapter);
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
