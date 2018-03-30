using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class VisSprCardstatesTableWrapper : AbstractTableWrapper
    {
        public VisSprCardstatesTableWrapper()
        {
            this.getTableBehavior = new VisSprCardstatesTableBehavior();
        }
    }
}
