using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class GetVisOrdersTableBehavior : IGetTableBehavior
    {
        Connector connector;

        public GetVisOrdersTableBehavior()
        {
            this.connector = Connector.CurrentConnector;
        }

        public DataTable GetTable()
        {
            string query = "select * from vis_orders";
            DataTable dt = new DataTable("vis_orders");
            dt = this.connector.GetDataTable(query);
            if (dt.TableName == null | dt.TableName == "")
            {
                dt.TableName = "vis_orders";
            }
            return dt;
        }
    }
}
