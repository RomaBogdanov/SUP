using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class VisVisitorsTableWrapper : AbstractTableWrapper
    {
        public VisVisitorsTableWrapper()
        { this.getTableBehavior = new VisVisitorsTableBehavior(); }
    }
}
