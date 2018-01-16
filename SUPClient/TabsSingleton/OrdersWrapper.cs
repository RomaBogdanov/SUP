using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupClientConnectionLib;
using SupClientConnectionLib.ServiceRef;

namespace SUPClient
{
    class OrdersWrapper : TableWrapper
    {
        static OrdersWrapper currentTable;

        public static OrdersWrapper CurrentTable()
        {
            if (currentTable!=null)
            {
                return currentTable;
            }
            currentTable = new OrdersWrapper();
            return currentTable;
        }

        private OrdersWrapper()
        {
            this.connector = new ClientConnector();
            this.table = connector.GetTable(TableName.VisOrders);
        }
    }
}
