using System.Data;
using System.Data.Common;
using SupHost.Connectors;

namespace SupHost
{
    /// <summary>
    /// Класс с обобщённым кодом для таблиц получаемых из базы данных Visitors.
    /// </summary>
    abstract class VisitorsDBTableBehavior : ITableBehavior
    {
        protected VisConnector connector = VisConnector.CurrentConnector;
        protected DataTable table = null;
        protected DbDataAdapter adapter = null;

        protected string query = "";
        protected string[] primaryKeyColumns;
        protected bool autoPrimaryKey = false;
        protected string tableName = "";

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
            this.connector.UpdateTable(this.table, this.adapter);
        }

        public void DeleteRow()
        {
            this.connector.UpdateTable(this.table, this.adapter);
        }

        /// <summary>
        /// Процедура рассчитанная на стандартную таблицу с одной колонкой
        /// выступающей как id.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="idColumn"></param>
        protected void StandartSetup(string tableName, string idColumn)
        {
            this.query = $"select * from {tableName}";
            this.primaryKeyColumns = new string[1];
            this.primaryKeyColumns[0] = idColumn;
            this.autoPrimaryKey = true;
            this.tableName = tableName;
        }

    }
}
