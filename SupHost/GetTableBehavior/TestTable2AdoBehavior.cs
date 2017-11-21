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
    class TestTable2AdoBehavior : ITableBehavior
    {
        Connector connector;
        DataTable table = null;
        DbDataAdapter adapter = null;

        public TestTable2AdoBehavior()
        {
            this.connector = Connector.CurrentConnector;
        }

        public DataTable GetTable()
        {
            string query = "select * from TestTab";
            ConnectionToDataBaseSetup setup = this.connector.GetDataTable(query);
            this.table = setup.Table;
            this.table.TableName = "TestTab";
            //this.table.PrimaryKey = new DataColumn[] { this.table.Columns["C1"] };
            string stUpdate = 
                "update TestTab set C1=@C1, C2=@C2, C3=@C3 where" +
                " C1=@C1_old";
            this.adapter = setup.DataAdapter;
            this.adapter.UpdateCommand = new SqlCommand(stUpdate);
            SqlParameterCollection pc = ((SqlDataAdapter)this.adapter).UpdateCommand.Parameters;
            SqlParameter p;
            pc.Add("@C1", SqlDbType.NVarChar, 20, "C1");
            pc.Add("@C2", SqlDbType.NVarChar, 20, "C2");
            pc.Add("@C3", SqlDbType.NVarChar, 20, "C3");
            p = pc.Add("@C1_old", SqlDbType.NVarChar, 20, "C1");
            p.SourceVersion = DataRowVersion.Original;
            string stDelete = "delete from TestTab where C1=@C1";
            this.adapter.DeleteCommand = new SqlCommand(stDelete);
            pc = ((SqlDataAdapter)this.adapter).DeleteCommand.Parameters;
            pc.Add("@C1", SqlDbType.NVarChar, 20, "C1");
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
