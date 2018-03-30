using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupClientConnectionLib.ServiceRef;

namespace SupRealClient
{
    class CardsWrapper : TableWrapper
    {
        static CardsWrapper currentTable;

        public static CardsWrapper CurrentTable()
        {
            return currentTable = currentTable ?? new CardsWrapper();
        }

        private CardsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisCards);
            this.Subscribe();
        }
    }
}
