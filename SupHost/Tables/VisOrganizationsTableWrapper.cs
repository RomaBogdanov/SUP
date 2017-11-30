using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class VisOrganizationsTableWrapper : AbstractTableWrapper
    {
        public VisOrganizationsTableWrapper()
        { this.getTableBehavior = new VisOrganizationsTableBehavior(); }
    }
}
