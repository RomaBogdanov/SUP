using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SupHost
{
    class GetTestTable2AdoBehavior : IGetTableBehavior
    {
        Connector connector;

        public GetTestTable2AdoBehavior()
        {
            this.connector = Connector.CurrentConnector;
        }

        public DataTable GetTable()
        {
            string query = "select * from TestTab";
            DataTable dt = null;
            dt = this.connector.GetDataTable(query);
            DataTable dt1 = new DataTable("TestTab");
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DataColumn d = dt.Columns[i];
                dt1.Columns.Add(d.ColumnName);
            }
            foreach (DataRow item in dt.Select())
            {
                DataRow r = dt1.NewRow();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    r[i] = item[i];
                }
                dt1.Rows.Add(r);
            }
            return dt1;
        }
    }
}
