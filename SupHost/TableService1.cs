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

        /// <summary>
        /// Получение таблицы.
        /// </summary>
        /// <param name="composite"></param>
        /// <returns></returns>
        public DataTable GetTable(CompositeType composite)
        {
            AbstractTableWrapper tableWrapper = 
                AbstractTableWrapper.GetTableWrapper(composite.TableName);
            if (tableWrapper != null)
            {
                DataTable dt = tableWrapper.GetTable();
                return dt;
            }
            return null;
        }

        /// <summary>
        /// Запись в таблицу строк.
        /// </summary>
        /// <param name="composite"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public bool InsertRows(CompositeType composite, object[] objs)
        {
            AbstractTableWrapper tableWrapper =
                AbstractTableWrapper.GetTableWrapper(composite.TableName);
            if (tableWrapper != null)
            {
                tableWrapper.InsertRow(objs);
            }
            return true;
        }

    }
}
