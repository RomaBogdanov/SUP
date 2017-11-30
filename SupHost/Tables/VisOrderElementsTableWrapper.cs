using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class VisOrderElementsTableWrapper : AbstractTableWrapper
    {
        public VisOrderElementsTableWrapper()
        { this.getTableBehavior = new VisOrderElementsTableBehavior(); }
    }
}
