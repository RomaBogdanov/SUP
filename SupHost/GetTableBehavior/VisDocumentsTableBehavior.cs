﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupHost
{
    class VisDocumentsTableBehavior: VisitorsDBTableBehavior
    {
        public VisDocumentsTableBehavior()
        {
            this.query = "select * from vis_documents";
            this.primaryKeyColumns = new string[1];
            this.primaryKeyColumns[0] = "f_doc_id";
            this.autoPrimaryKey = true;
            this.tableName = "vis_documents";
        }
    }
}