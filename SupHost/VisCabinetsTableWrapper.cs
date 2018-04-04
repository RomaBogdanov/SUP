using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class VisCabinetsTableWrapper : AbstractTableWrapper
    {
        public VisCabinetsTableWrapper()
        {
            this.getTableBehavior = new VisCabinetsTableBehavior();
        }
    }
}
