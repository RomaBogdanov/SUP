using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupClientConnectionLib.ServiceRef;

namespace SupRealClient
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
