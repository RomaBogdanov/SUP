using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class VisOrganizationsTableBehavior : VisitorsDBTableBehavior
    {
        public VisOrganizationsTableBehavior()
        {
            this.query = "select * from vis_organizations where f_org_id<>0";
            this.primaryKeyColumns = new string[1];
            this.primaryKeyColumns[0] = "f_org_id";
            this.autoPrimaryKey = true;
            this.tableName = "vis_organizations";
        }
    }
}
