using SupClientConnectionLib.ServiceRef;

namespace SupRealClient.TabsSingleton
{
    class CountriesWrapper : TableWrapper
    {
        static CountriesWrapper currentTable;

        public static CountriesWrapper CurrentTable()
        {
            return currentTable = currentTable ?? new CountriesWrapper();
        }

        private CountriesWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisCountries);
            this.Subscribe();
        }
    }
}
