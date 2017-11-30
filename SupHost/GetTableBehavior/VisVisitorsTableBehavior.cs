using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class VisVisitorsTableBehavior : VisitorsDBTableBehavior
    {
        public VisVisitorsTableBehavior()
        {
            this.query = "select * from vis_visitors";
            this.primaryKeyColumns = new string[1];
            this.primaryKeyColumns[0] = "f_visitor_id";
            this.tableName = "vis_visitors";
        }
    }
}
