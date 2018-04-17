using SupContract;

namespace SUPClient
{
    class VisitorsWrapper  : TableWrapper
    {
        static VisitorsWrapper currentTable;

        public static VisitorsWrapper CurrentTable()
        {
            if (currentTable != null)
            {
                return currentTable;
            }
            currentTable = new VisitorsWrapper();
            return currentTable;
        }

        private VisitorsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisVisitors);
        }
    }
}
