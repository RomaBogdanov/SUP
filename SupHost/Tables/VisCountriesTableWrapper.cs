using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class VisCountriesTableWrapper : AbstractTableWrapper
    {
        public VisCountriesTableWrapper()
        {
            this.getTableBehavior = new VisCountriesTableBehavior();
        }
    }
}
