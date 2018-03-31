using SupClientConnectionLib.ServiceRef;

namespace SupRealClient.TabsSingleton
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
