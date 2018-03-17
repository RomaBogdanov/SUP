using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupClientConnectionLib;
using SupClientConnectionLib.ServiceRef;

namespace SupRealClient
{
    class OrganizationsWrapper : TableWrapper
    {
        static OrganizationsWrapper currentTable;

        public static OrganizationsWrapper CurrentTable()
        {
            if (currentTable != null)
            {
                return currentTable;
            }
            currentTable = new OrganizationsWrapper();
            return currentTable;
        }

        private OrganizationsWrapper() : base()
        {
            this.table = connector.GetTable(TableName.VisOrganizations);
        }
    }
}
