using SupClientConnectionLib.ServiceRef;

namespace SupRealClient.TabsSingleton
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
