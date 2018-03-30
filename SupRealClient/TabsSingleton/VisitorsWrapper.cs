using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupClientConnectionLib.ServiceRef;

namespace SupRealClient
{
    class VisitorsWrapper : TableWrapper
    {
        static VisitorsWrapper currentTable;

        public static VisitorsWrapper CurrentTable() =>
            currentTable = currentTable ?? new VisitorsWrapper();

        private VisitorsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisVisitors);
            this.Subscribe();
        }
    }
}
