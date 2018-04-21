using System.Data;
using System.Data.Common;
using SupHost.Connectors;

namespace SupHost
{
    /// <summary>
    /// Класс с обобщённым кодом для таблиц получаемых из базы данных
    /// </summary>
    abstract class BaseTableBehavior : ITableBehavior
    {
        protected DataTable table = null;
        protected DbDataAdapter adapter = null;

        protected string query = "";
        protected string[] primaryKeyColumns;
        protected bool autoPrimaryKey = false;
        protected string tableName = "";

        public DataTable GetTable()
        {
            ConnectionToDataBaseSetup setup = this.GetConnector().GetDataTable(this.query);
            this.table = setup.Table;
            SetPrimaryKey();
            this.adapter = setup.DataAdapter;
            this.table.TableName = this.tableName;
            return this.table;
        }

        public virtual void InsertRow()
        {
            this.GetConnector().UpdateTable(this.table, this.adapter);
        }

        public virtual void UpdateRow()
        {
            this.GetConnector().UpdateTable(this.table, this.adapter);
        }

        public virtual void DeleteRow()
        {
            this.GetConnector().UpdateTable(this.table, this.adapter);
        }

        protected abstract Connector GetConnector();

        protected virtual void SetPrimaryKey()
        {
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
