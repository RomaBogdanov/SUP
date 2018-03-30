using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class VisNewUserTableBehavior : VisitorsDBTableBehavior
    {
        public VisNewUserTableBehavior()
        {
            this.StandartSetup("vis_new_user", "f_user_id");
        }
    }
}
