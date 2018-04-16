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
        public event Action<string, object[]> OnAddRow;
        public event Action<string, int, object[]> OnUpdateRow;
        public event Action<string, object[]> OnDeleteRow;

        private static Dictionary<string, AbstractTableWrapper> wrappers;
        protected Logger logger = Logger.CurrentLogger;
        protected DataTable table;

        protected ITableBehavior getTableBehavior;

        static AbstractTableWrapper()
        {
            //TODO: сделать со всем этим колхозом что-нибуть. Например: 
            //вынести в конфиг и создавать через рефлексию и фабрику.
            wrappers = new Dictionary<string, AbstractTableWrapper>
            {
                { TableName.TestTable1.ToString(), new TestTable1Wrapper() },
                { TableName.TestTable2Ado.ToString(), new TestTable2AdoWrapper() },
                { TableName.VisClientUsers.ToString(), new VisClientUsersTableWrapper() },
                { TableName.VisOrders.ToString(), new VisOrdersTableWrapper() },
                { TableName.VisOrderElements.ToString(), new VisOrderElementsTableWrapper() },
                { TableName.VisOrganizations.ToString(), new VisOrganizationsTableWrapper() },
                { TableName.VisVisitors.ToString(), new VisVisitorsTableWrapper() },
                { TableName.VisDocuments.ToString(), new VisDocumentsTableWrapper() },
                { TableName.VisCountries.ToString(), new VisCountriesTableWrapper() },
                { TableName.VisCards.ToString(), new VisCardsTableWrapper() },
                { TableName.VisSprCardstates.ToString(), new VisSprCardstatesTableWrapper() },
                { TableName.VisVisits.ToString(), new VisVisitsTableWrapper() },
                { TableName.VisCabinets.ToString(), new VisCabinetsTableWrapper() },
                { TableName.VisZones.ToString(), new VisZonesTableWrapper() },
                { TableName.VisCabinetsZones.ToString(), new VisCabinetsZonesTableWrapper() },
                { TableName.VisZoneTypes.ToString(), new VisZoneTypesTableWrapper() },
                { TableName.VisClientLogs.ToString(), new LogTableWrapper() },
                { TableName.VisDepartment.ToString(), new VisDepartmentTableWrapper() },
                {TableName.VisDepartmentSection.ToString(), new VisDepartmentSectionTableWrapper() }
            };
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
            // TODO - для лога он нулевой
            if (this.OnAddRow != null)
            {
                this.OnAddRow(this.table.TableName, values);
            }
            LogMessage($"В таблице {this.table.TableName} добавлена строка", dr);
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
            this.OnUpdateRow(this.table.TableName, numRow, values);
            LogMessage($"В таблице {this.table.TableName} отредактирована строка", dr);
            return true;
        }

        public virtual bool DeleteRow(object[] objs)
        {
            if (table.Rows.Contains(objs[0]))
            {
                this.table.Rows.Remove(table.Rows.Find(objs[0]));
                //this.table.Rows[numRow].Delete();
                this.getTableBehavior.DeleteRow();
                this.OnDeleteRow(this.table.TableName, objs);
                // TODO - добавить пользователя в логирование
                this.logger.Debug($"В таблице {this.table.TableName} удалена строка");
            }
            return true;
        }

        protected virtual void LogMessage(string message, DataRow dr)
        {
            this.logger.Debug(message, GetUserId(dr));
        }

        private int GetUserId(DataRow dr)
        {
            return this.table.Columns.Contains("f_rec_operator") ? (int)dr["f_rec_operator"] : -1;
        }
    }
}
