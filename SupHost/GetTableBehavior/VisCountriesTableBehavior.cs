using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class VisCountriesTableBehavior : VisitorsDBTableBehavior
    {
        public VisCountriesTableBehavior()
        {
            this.query = "select * from vis_countries";
            this.primaryKeyColumns = new string[1];
            this.primaryKeyColumns[0] = "f_cntr_id";
            this.autoPrimaryKey = true;
            this.tableName = "vis_countries";
        }
    }
}
