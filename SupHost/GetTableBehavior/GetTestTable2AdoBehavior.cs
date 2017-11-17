using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace SupHost
{
    class GetTestTable2AdoBehavior : IGetTableBehavior
    {
        Connector connector;
        DataTable table = null;
        DbDataAdapter adapter = null;

        public GetTestTable2AdoBehavior()
        {
            this.connector = Connector.CurrentConnector;
        }

        public DataTable GetTable()
        {
            string query = "select * from TestTab";
            ConnectionToDataBaseSetup setup = this.connector.GetDataTable(query);
            this.table = setup.Table;
            this.adapter = setup.DataAdapter;
            this.table.TableName = "TestTab";
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
