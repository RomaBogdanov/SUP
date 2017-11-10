using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class TestTable2AdoWrapper : AbstractTableWrapper
    {
        public TestTable2AdoWrapper()
        { this.getTableBehavior = new GetTestTable2AdoBehavior(); }
    }
}
