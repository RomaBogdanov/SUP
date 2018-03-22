using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class VisDocumentsTableWrapper: AbstractTableWrapper
    {
        public VisDocumentsTableWrapper()
        {
            this.getTableBehavior = new VisDocumentsTableBehavior();
        }
    }
}
