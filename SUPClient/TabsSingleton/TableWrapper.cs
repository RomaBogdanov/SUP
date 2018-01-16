using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SupClientConnectionLib;
using SupClientConnectionLib.ServiceRef;

namespace SUPClient
{
    class TableWrapper
    {
        protected DataTable table;
        protected ClientConnector connector;

        public DataTable Table { get { return this.table; } }

        public ClientConnector Connector { get { return this.connector; } }

    }
}
