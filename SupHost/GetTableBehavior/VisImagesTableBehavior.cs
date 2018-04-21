using SupHost.Connectors;
using System.Data;
using System.Data.Common;

namespace SupHost
{
    class VisImagesTableBehavior : ITableBehavior
    {
        private ImagesConnector connector = ImagesConnector.CurrentConnector;
        protected DataTable table = null;
        protected DbDataAdapter adapter = null;

        protected string query = "";
        protected string[] primaryKeyColumns;
        protected bool autoPrimaryKey = false;
        protected string tableName = "";

        public VisImagesTableBehavior()
        {
            this.query = "select * from vis_image";
            this.primaryKeyColumns = new string[1];
            this.primaryKeyColumns[0] = "f_image_id";
            this.autoPrimaryKey = true;
            this.tableName = "vis_image";
        }

        public DataTable GetTable()
        {
            ConnectionToDataBaseSetup setup = this.connector.GetDataTable(this.query);
            this.table = setup.Table;
            DataColumn[] dcs = new DataColumn[primaryKeyColumns.Length];
            for (int i = 0; i < dcs.Length; i++)
            {
                dcs[i] = this.table.Columns[this.primaryKeyColumns[i]];
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
    }
}
