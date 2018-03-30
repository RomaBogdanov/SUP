using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class VisVisitsTableWrapper : AbstractTableWrapper
    {
        public VisVisitsTableWrapper()
        {
            this.getTableBehavior = new VisVisitsTableBehavior();
        }
    }
}
