﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using SupContract;

namespace SupHost
{
    abstract class AbstractTableWrapper
    {
        private static Dictionary<string, AbstractTableWrapper> wrappers;

        protected DataTable table;

        protected IGetTableBehavior getTableBehavior;

        static AbstractTableWrapper()
        {
            wrappers = new Dictionary<string, AbstractTableWrapper>();
            wrappers.Add(TableName.TestTable1.ToString(), new TestTable1Wrapper());
            wrappers.Add(TableName.TestTable2Ado.ToString(), new TestTable2AdoWrapper());
            wrappers.Add(TableName.VisOrders.ToString(), new VisOrdersTableWrapper());
        }

        public static AbstractTableWrapper GetTableWrapper(TableName table)
        {
            return wrappers[table.ToString()];
        }

        public virtual DataTable GetTable()
        {
            if (table == null)
            {
                this.table = this.getTableBehavior.GetTable();
                return this.table;
            }
            return this.table;
        }
    }
}
