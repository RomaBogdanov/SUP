using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupClientConnectionLib.ServiceRef;

namespace SupRealClient
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
