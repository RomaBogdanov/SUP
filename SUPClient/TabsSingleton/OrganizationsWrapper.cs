﻿using SupContract;

namespace SUPClient
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
