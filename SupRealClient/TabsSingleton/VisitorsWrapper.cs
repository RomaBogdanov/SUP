using SupClientConnectionLib.ServiceRef;

namespace SupRealClient.TabsSingleton
{
    class VisitorsWrapper : TableWrapper
    {
        static VisitorsWrapper currentTable;

        public static VisitorsWrapper CurrentTable() =>
            currentTable = currentTable ?? new VisitorsWrapper();

        private VisitorsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisVisitors);
            this.Subscribe();
        }
    }
}
