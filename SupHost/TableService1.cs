using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupContract;

namespace SupHost
{
    class TableService1 : ITableService
    {
        public string GetData(int value)
        {
            return "This method don't use!";
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            return new CompositeType();
        }

        public DataTable GetTable(CompositeType composite)
        {
            IGetTableBehavior tableBehavior = this.CreateTableBehavior(composite.TableName);
            if (tableBehavior != null)
            {
                DataTable dt = tableBehavior.GetTable();
                return dt;
            }
            return null;
        }

        IGetTableBehavior CreateTableBehavior(TableName table)
        {
            if (table == TableName.TestTable1)
            {
                return new GetTestTable1Behavior();
            }
            else if (table == TableName.VisOrders)
            {
                return new GetVisOrdersTableBehavior();
            }
            else if (table == TableName.TestTable2Ado)
            {
                return new GetTestTable2AdoBehavior();
            }
            return null;
        }
    }
}
