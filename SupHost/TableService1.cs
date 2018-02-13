using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupContract;
using System.Data.Sql;
using System.Data.SqlClient;

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
        public bool InsertRow(CompositeType composite, object[] objs)
        {
            AbstractTableWrapper tableWrapper =
                AbstractTableWrapper.GetTableWrapper(composite.TableName);
            if (tableWrapper != null)
            {
                return tableWrapper.InsertRow(objs);
            }
            return false;
        }

        /// <summary>
        /// Обновление строки в таблице.
        /// </summary>
        /// <param name="composite"></param>
        /// <param name="rowNumber"></param>
        /// <param name="objs"></param>
        /// <returns></returns>
        public bool UpdateRow(CompositeType composite, int rowNumber, object[] objs)
        {
            AbstractTableWrapper tableWrapper =
                AbstractTableWrapper.GetTableWrapper(composite.TableName);
            if (tableWrapper != null)
            {
                return tableWrapper.UpdateRow(objs, rowNumber);
            }
            return false;
        }

        /// <summary>
        /// Удаление строки из таблицы.
        /// </summary>
        /// <param name="composite"></param>
        /// <param name="rowNumber"></param>
        /// <returns></returns>
        public bool DeleteRow(CompositeType composite, int rowNumber)
        {
            AbstractTableWrapper tableWrapper =
                AbstractTableWrapper.GetTableWrapper(composite.TableName);
            if (tableWrapper != null)
            {
                return tableWrapper.DeleteRow(rowNumber);
            }
            return false;
        }

        /// <summary>
        /// Процедура получения изображения.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public byte[] GetImage(int id)
        {
            //TODO: обязательно переработать. Данный вариант выступает как заглушка. 
            string conSt = "Server = MISTEROWL; Database = dbTest; Trusted_Connection = True; ";
            byte[] bytes = null;
            using (SqlConnection cn = new SqlConnection(conSt))
            {
                cn.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = cn;
                sqlCommand.CommandText = $"select id, screen from Images where id = {id}";
                SqlDataReader sqlReader = sqlCommand.ExecuteReader();
                while (sqlReader.Read())
                {
                    bytes = (byte[])sqlReader["screen"];
                }
                if (bytes == null)
                {
                    cn.Close();
                    cn.Open();
                    sqlCommand = new SqlCommand();
                    sqlCommand.Connection = cn;
                    sqlCommand.CommandText = $"select id, screen from Images where id = 0";
                    sqlReader = sqlCommand.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        bytes = (byte[])sqlReader["screen"];
                    }
                }
                cn.Close();
            }
            return bytes;
        }
    }
}
