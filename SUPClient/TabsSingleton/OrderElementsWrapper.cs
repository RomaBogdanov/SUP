using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupClientConnectionLib;
using SupClientConnectionLib.ServiceRef;

namespace SUPClient
{
    class OrderElementsWrapper : TableWrapper
    {
        static OrderElementsWrapper currentTable;

        public static OrderElementsWrapper CurrentTable()
        {
            if (currentTable != null)
            {
                return currentTable;
            }
            currentTable = new OrderElementsWrapper();
            return currentTable;
        }

        private OrderElementsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisOrderElements);
        }
    }
}
