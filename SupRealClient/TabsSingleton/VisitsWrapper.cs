using SupClientConnectionLib.ServiceRef;

namespace SupRealClient.TabsSingleton
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
