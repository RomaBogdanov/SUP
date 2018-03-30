using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupClientConnectionLib.ServiceRef;

namespace SupRealClient
{
    class VisitsWrapper : TableWrapper
    {
        static VisitsWrapper currentTable;

        public static VisitsWrapper CurrentTable()
            => currentTable = currentTable ?? new VisitsWrapper();

        private VisitsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisVisits);
            this.Subscribe();
        }
    }
}
