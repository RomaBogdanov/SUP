using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupClientConnectionLib.ServiceRef;

namespace SupRealClient
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
