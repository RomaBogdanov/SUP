using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class VisOrderElementsTableBehavior : VisitorsDBTableBehavior
    {
        public VisOrderElementsTableBehavior()
        {
            this.query = "select * from vis_order_elements";
            this.primaryKeyColumns = new string[1];
            this.primaryKeyColumns[0] = "f_oe_id";
            this.tableName = "vis_order_elements";
        }
    }
}
