using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class TestTable1Wrapper : AbstractTableWrapper
    {
        public TestTable1Wrapper()
        { this.getTableBehavior = new TestTable1Behavior(); }
    }
}
