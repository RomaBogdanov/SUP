﻿using System;
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
            this.query = "select * from vis_organizations";
            this.primaryKeyColumns = new string[1];
            this.primaryKeyColumns[0] = "f_org_id";
            this.tableName = "vis_organizations";
        }
    }
}
