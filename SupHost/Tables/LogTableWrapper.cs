using System;
using System.Data;

namespace SupHost
{
    /// <summary>
    /// Это особенная таблица. Она не должна передаваться пользователям.
    /// Может использоваться только внутри сервера приложения.
    /// </summary>
    class LogTableWrapper
    {
        static LogTableWrapper logTableWrapper;

        private ITableBehavior getTableBehavior;
        private DataTable table;

        public static LogTableWrapper GetLogTableWrapper()
        {
            if (logTableWrapper == null)
            {
                logTableWrapper = new LogTableWrapper();
            }
            return logTableWrapper;
        }

        private LogTableWrapper()
        {
            this.getTableBehavior = new LogTableBehavior();
        }

        public virtual DataTable GetTable()
        {
            if (table == null)
            {
                this.table = this.getTableBehavior.GetTable();
            }
            return this.table;
        }

        public void Write(string message, string severity)
        {
            try
            {
                this.GetTable();
                DataRow row = this.table.NewRow();
                row["f_log_severety"] = severity;
                row["f_log_message"] = message;
                row["f_rec_date"] = DateTime.Now;
                this.InsertRow(row.ItemArray);
            }
            catch (Exception)
            {
                // TODO
            }
        }

        private bool InsertRow(object[] values)
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
                    // TODO
                }
            }
            this.table.Rows.Add(dr);
            this.getTableBehavior.InsertRow();
            return true;
        }
    }
}
