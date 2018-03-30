using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupClientConnectionLib.ServiceRef;

namespace SupRealClient
{
    class SprCardstatesWrapper : TableWrapper
    {
        static SprCardstatesWrapper currentTable;

        public static SprCardstatesWrapper CurrentTable()
        { return currentTable = currentTable ?? new SprCardstatesWrapper(); }

        private SprCardstatesWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisSprCardstates);
            this.Subscribe();
        }
    }
}
