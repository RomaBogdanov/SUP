using SupClientConnectionLib.ServiceRef;

namespace SupRealClient.TabsSingleton
{
    class NewUserWrapper : TableWrapper
    {
        static NewUserWrapper currentTable;

        public static NewUserWrapper CurrentTable()
        { return currentTable = currentTable ?? new NewUserWrapper(); }

        private NewUserWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisNewUser);
            this.Subscribe();
        }
    }
}
