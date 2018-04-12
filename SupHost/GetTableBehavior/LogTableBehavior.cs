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
        protected string[] primaryKeyColumns;
        protected bool autoPrimaryKey = false;
        protected string tableName = "";

        public LogTableBehavior()
        {
            this.query = "select * from vis_log";
            this.primaryKeyColumns = new string[1];
            this.primaryKeyColumns[0] = "f_log_id";
            this.autoPrimaryKey = true;
            this.tableName = "vis_log";
        }

        public DataTable GetTable()
        {
            ConnectionToDataBaseSetup setup = this.connector.GetDataTable(this.query);
            this.table = setup.Table;
            DataColumn[] dcs = new DataColumn[primaryKeyColumns.Length];
            for (int i = 0; i < dcs.Length; i++)
            {
                dcs[i] = this.table.Columns[this.primaryKeyColumns[i]];
                if (this.autoPrimaryKey)
                {
                    this.table.Columns[this.primaryKeyColumns[i]].AutoIncrement = true;
                }
            }
            this.table.PrimaryKey = dcs;
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
