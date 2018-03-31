using SupClientConnectionLib.ServiceRef;

namespace SupRealClient.TabsSingleton
{
    class DocumentsWrapper : TableWrapper
    {
        static DocumentsWrapper currentTable;

        public static DocumentsWrapper CurrentTable()
        {
            return currentTable = currentTable ?? new DocumentsWrapper();
        }

        private DocumentsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisDocuments);
            this.Subscribe();
        }
    }
}
