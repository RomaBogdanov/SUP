using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class VisOrdersTableWrapper : AbstractTableWrapper
    {
        public VisOrdersTableWrapper()
        { this.getTableBehavior = new VisOrdersTableBehavior(); }
    }
}
