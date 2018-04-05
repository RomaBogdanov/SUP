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
        private Logger logger = Logger.CurrentLogger;
        private object lockDb = new object();

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

        public void Write(LogData logData)
        {
            lock (lockDb)
            {
                try
                {
                    this.GetTable();
                    DataRow row = this.table.NewRow();
                    row["f_log_severety"] = logData.Severity;
                    row["f_log_message"] = logData.Message;
                    row["f_rec_date"] = logData.Date;
                    row["f_log_class"] = logData.Class;
                    row["f_rec_operator"] = logData.User >= 0 ? (object)logData.User : DBNull.Value;
                    this.table.Rows.Add(row);
                    this.getTableBehavior.InsertRow();
                }
                catch (Exception err)
                {
                    this.logger.ErrorMessage(err.Message + err.StackTrace);
                }
            }
        }
    }
}
