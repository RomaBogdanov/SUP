using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class VisCardsTableBehavior : VisitorsDBTableBehavior
    {
        public VisCardsTableBehavior()
        {
            this.StandartSetup("vis_cards", "f_card_id");
        }
    }
}
