using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class VisSprCardstatesTableBehavior : VisitorsDBTableBehavior
    {
        public VisSprCardstatesTableBehavior()
        {
            this.StandartSetup("vis_spr_cardstates", "f_state_id");
        }
    }
}
