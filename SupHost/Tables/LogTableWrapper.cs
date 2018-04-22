using SupContract;
using SupHost.Data;
using System;
using System.Data;

namespace SupHost
{
    /// <summary>
    /// Это особенная таблица. Она не должна передаваться пользователям.
    /// Может использоваться только внутри сервера приложения.
    /// </summary>
    class LogTableWrapper : AbstractTableWrapper
    {
        private object lockDb = new object();

        public LogTableWrapper()
        {
            this.getTableBehavior = new LogTableBehavior();
        }

        public void Write(LogData logData)
        {
            lock (lockDb)
            {
                try
                {
                    // TODO - клиент вызывает TableService1.GetTable() и это работает. А тут проблемы с id
                    this.GetTable();
                    DataRow row = this.table.NewRow();
                    // TODO - пока что генерим id сами
                    row["f_log_id"] = this.table.Rows.Count > 0 ?
                        (long)this.table.Rows[this.table.Rows.Count - 1][0] + 1 : 1;
                    row["f_log_severety"] = logData.Severity;
                    row["f_log_message"] = logData.Message;
                    row["f_rec_date"] = logData.Date;
                    row["f_log_class"] = logData.Class;
                    row["f_rec_operator"] = logData.User >= 0 ? (object)logData.User : DBNull.Value;
                    row["f_machine"] = logData.Machine;
                    InsertRow(row.ItemArray, null);
                }
                catch (Exception err)
                {
                    this.logger.ErrorMessage(err.Message + err.StackTrace);
                }
            }
        }

        protected override void LogMessage(string message, OperationInfo info)
        {
            // В этом враппере не пишем лог, чтобы не было рекурсии
        }
    }
}
