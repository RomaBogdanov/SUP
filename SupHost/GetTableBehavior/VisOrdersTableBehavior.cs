using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class VisOrdersTableBehavior : VisitorsDBTableBehavior
    {
        public VisOrdersTableBehavior()
        {
            this.query = "select * from vis_orders";
            this.primaryKeyColumns = new string[1];
            this.primaryKeyColumns[0] = "f_order_id";
            this.tableName = "vis_orders";
        }
    }
}
