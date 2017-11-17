using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace SupHost
{
    class GetVisOrdersTableBehavior : IGetTableBehavior
    {
        Connector connector;
        DataTable table = null;
        DbDataAdapter adapter = null;

        public GetVisOrdersTableBehavior()
        {
            this.connector = Connector.CurrentConnector;
        }

        public DataTable GetTable()
        {
            string query = "select * from vis_orders";
            ConnectionToDataBaseSetup setup = this.connector.GetDataTable(query);
            this.table = setup.Table;
            this.table.PrimaryKey = new DataColumn[] { this.table.Columns["f_order_id"] };
            this.adapter = setup.DataAdapter;
            this.table.TableName = "vis_orders";
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
