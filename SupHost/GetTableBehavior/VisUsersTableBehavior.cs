using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class VisUsersTableBehavior : VisitorsDBTableBehavior
    {
        public VisUsersTableBehavior()
        {
            this.query = "select * from vis_users";
            this.primaryKeyColumns = new string[1];
            this.primaryKeyColumns[0] = "f_user_id";
            this.autoPrimaryKey = true;
            this.tableName = "vis_users";
        }
    }
}
