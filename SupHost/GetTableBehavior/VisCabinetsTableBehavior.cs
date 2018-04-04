using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class VisCabinetsTableBehavior : VisitorsDBTableBehavior
    {
        public VisCabinetsTableBehavior()
        {
            this.StandartSetup("vis_cabinets", "f_cabinet_id");
        }
    }
}
