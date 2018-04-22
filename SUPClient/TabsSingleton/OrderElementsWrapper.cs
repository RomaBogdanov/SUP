using SupContract;

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
