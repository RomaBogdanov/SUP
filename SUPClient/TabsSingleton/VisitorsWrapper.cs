using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SupClientConnectionLib;
using SupClientConnectionLib.ServiceRef;

namespace SUPClient
{
    class VisitorsWrapper  : TableWrapper
    {
        static VisitorsWrapper currentTable;

        public static VisitorsWrapper CurrentTable()
        {
            if (currentTable != null)
            {
                return currentTable;
            }
            currentTable = new VisitorsWrapper();
            return currentTable;
        }

        private VisitorsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisVisitors);
        }
    }
}
