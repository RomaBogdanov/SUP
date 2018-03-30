using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class VisVisitsTableBehavior : VisitorsDBTableBehavior
    {
        public VisVisitsTableBehavior()
        {
            this.StandartSetup("vis_visits", "f_visit_id");
        }
    }
}
