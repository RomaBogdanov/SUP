using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class VisCardsTableWrapper : AbstractTableWrapper
    {
        public VisCardsTableWrapper()
        { this.getTableBehavior = new VisCardsTableBehavior(); }
    }
}
