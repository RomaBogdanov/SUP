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
        private Logger logger = Logger.CurrentLogger;
        protected DataTable table;

        protected ITableBehavior getTableBehavior;

        static AbstractTableWrapper()
        {
            //TODO: сделать со всем этим колхозом что-нибуть. Например: 
            //вынести в конфиг и создавать через рефлексию и фабрику.
            wrappers = new Dictionary<string, AbstractTableWrapper>();
            wrappers.Add(TableName.TestTable1.ToString(), new TestTable1Wrapper());
            wrappers.Add(TableName.TestTable2Ado.ToString(), new TestTable2AdoWrapper());
            wrappers.Add(TableName.VisOrders.ToString(), new VisOrdersTableWrapper());
            wrappers.Add(TableName.VisOrderElements.ToString(), new VisOrderElementsTableWrapper());
            wrappers.Add(TableName.VisOrganizations.ToString(), new VisOrganizationsTableWrapper());
            wrappers.Add(TableName.VisVisitors.ToString(), new VisVisitorsTableWrapper());
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
                catch (Exception err)
                {
                    this.logger.Error(err.Message + err.StackTrace);
                }
            }
            this.table.Rows.Add(dr);
            this.getTableBehavior.InsertRow();
            this.logger.Debug($"В таблице {this.table.TableName} добавлена строка");
            return true;
        }

        public virtual bool UpdateRow(object[] values, int numRow)
        {
            DataRow dr = this.table.Rows[numRow];
            for (int i = 0; i < this.table.Columns.Count; i++)
            {
                try
                {
                    dr[this.table.Columns[i]] = values[i];
                }
                catch (Exception err)
                {
                    this.logger.Error(err.Message + err.StackTrace);
                }
            }
            this.getTableBehavior.UpdateRow();
            this.logger.Debug($"В таблице {this.table.TableName} отредактирована строка");
            return true;
        }

        public virtual bool DeleteRow(int numRow)
        {
            this.table.Rows[numRow].Delete();
            this.getTableBehavior.DeleteRow();
            this.logger.Debug($"В таблице {this.table.TableName} удалена строка");
            return true;
        }
    }
}
