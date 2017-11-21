using System;
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

        protected ITableBehavior getTableBehavior;

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

        public virtual bool InsertRow(object[] values)
        {
            DataRow dr = this.table.NewRow();
            for (int i = 0; i < this.table.Columns.Count; i++)
            {
                try
                {
                    dr[this.table.Columns[i]] = values[i];
                }
                catch (Exception)
                {
                    
                }
            }
            /*foreach (var column in this.table.Columns)
            {
                try
                {
                    //((DataColumn)column)
                }
                catch (Exception err)
                {
                    
                }
            }*/
            //dr.ItemArray = values;
            this.table.Rows.Add(dr);
            this.getTableBehavior.InsertRow();
            return true;
        }

        public virtual bool UpdateRow(object[] values, int numRow)
        {
            //this.table.Rows[numRow].ItemArray = values;
            DataRow dr = this.table.Rows[numRow];
            for (int i = 0; i < this.table.Columns.Count; i++)
            {
                try
                {
                    dr[this.table.Columns[i]] = values[i];
                }
                catch (Exception)
                {

                }
            }
            this.getTableBehavior.UpdateRow();
            return true;
        }

        public virtual bool DeleteRow(int numRow)
        {
            this.table.Rows[numRow].Delete();
            this.getTableBehavior.DeleteRow();
            return true;
        }
    }
}
