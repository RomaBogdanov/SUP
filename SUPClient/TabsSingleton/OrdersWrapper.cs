using SupContract;

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

        private OrdersWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisOrders);
        }
    }
}
