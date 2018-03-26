using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupContract;
using System.Data.Sql;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Threading;

namespace SupHost
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class TableService1 : ITableService
    {
        ITableCallback callback;
        readonly int timeOut = 400;
        Accounts users = Accounts.GetAccounts();
        private Logger logger = Logger.CurrentLogger;

        ITableCallback Callback
        {
            get
            {
                return OperationContext
                    .Current
                    .GetCallbackChannel<ITableCallback>();
            }
        }

        public TableService1()
        {

        }

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
        public DataTable GetTable(CompositeType composite, string login)
        {
            if (!this.users.IsExist(login))
            {
                this.logger.Warn($@"Попытка запроса к серверу 
                    незарегистрированным аккаунтом {login}");
                return null;
            }
            AbstractTableWrapper tableWrapper =
                AbstractTableWrapper.GetTableWrapper(composite.TableName);
            if (tableWrapper != null)
            {
                callback = Callback;
                tableWrapper.OnAddRow += TableWrapper_OnAddRow;
                tableWrapper.OnUpdateRow += TableWrapper_OnUpdateRow;
                tableWrapper.OnDeleteRow += TableWrapper_OnDeleteRow;
                DataTable dt = tableWrapper.GetTable();
                return dt;
            }
            return null;
        }

        private void TableWrapper_OnAddRow(string tableName, object[] objs)
        {
            Task.Run(() =>
            {
                Thread.Sleep(timeOut);
                this.callback.InsRow(tableName, objs);
            });
        }

        private void TableWrapper_OnUpdateRow(
            string tableName, int rowNumber, object[] objs)
        {
            Task.Run(() =>
            {
                Thread.Sleep(timeOut);
                this.callback.UpdRow(tableName, rowNumber, objs);
            });
        }

        private void TableWrapper_OnDeleteRow(string tableName, object[] objs)
        {
            Task.Run(() =>
            {
                Thread.Sleep(timeOut);
                this.callback.DelRow(tableName, objs);
            });
        }

        /// <summary>
        /// Запись в таблицу строк.
        /// </summary>
        /// <param name="composite"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public bool InsertRow(CompositeType composite, object[] objs, string login)
        {
            if (!this.users.IsExist(login))
            {
                this.logger.Warn($@"Попытка запроса к серверу 
                    незарегистрированным аккаунтом {login}");
                return false;
            }
            AbstractTableWrapper tableWrapper =
                AbstractTableWrapper.GetTableWrapper(composite.TableName);
            /*if (tableWrapper != null)
            {
                return tableWrapper.InsertRow(objs);
            }
            return false;*/
            return tableWrapper?.InsertRow(objs) ?? false;
        }

        /// <summary>
        /// Обновление строки в таблице.
        /// </summary>
        /// <param name="composite"></param>
        /// <param name="rowNumber"></param>
        /// <param name="objs"></param>
        /// <returns></returns>
        public bool UpdateRow(CompositeType composite, int rowNumber, object[] objs, string login)
        {
            if (!this.users.IsExist(login))
            {
                this.logger.Warn($@"Попытка запроса к серверу 
                    незарегистрированным аккаунтом {login}");
                return false;
            }
            AbstractTableWrapper tableWrapper =
                AbstractTableWrapper.GetTableWrapper(composite.TableName);
            return tableWrapper?.UpdateRow(objs, rowNumber) ?? false;
        }

        /// <summary>
        /// Удаление строки из таблицы.
        /// </summary>
        /// <param name="composite"></param>
        /// <param name="objs"></param>
        /// <returns></returns>
        public bool DeleteRow(CompositeType composite, object[] objs, string login)
        {
            if (!this.users.IsExist(login))
            {
                this.logger.Warn($@"Попытка запроса к серверу 
                    незарегистрированным аккаунтом {login}");
                return false;
            }
            AbstractTableWrapper tableWrapper =
                AbstractTableWrapper.GetTableWrapper(composite.TableName);
            return tableWrapper?.DeleteRow(objs) ?? false;
        }

        /// <summary>
        /// Процедура получения изображения.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public byte[] GetImage(int id, string login)
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

        public bool Authorize(string login, string pass)
        {
            //TODO: обязательно переработать. Данный вариант выступает как заглушка.
            // Проверка на существование уже зарегистрированного пользователя.
            if (this.users.IsExist(login))
            {
                logger.Warn($@"Попытка зарегистрироваться под уже 
                    зарегистрированным аккаунтом {login}");
                return false;
            }
            VisUsersTableWrapper visUsersTableWrapper =
                VisUsersTableWrapper.GetVisUsersTableWrapper();
            if (visUsersTableWrapper.ExistingLogin(login, pass))
            {
                this.users.AddAccount(login);
                logger.Info($"Зарегистрировался аккаунт {login}");
                return true;
            }
            return false;
        }

        public bool CheckAuthorize(string login)
        {
            return users.CheckAccount(login);
        }

        public bool ExitAuthorize(string login)
        {
            if (this.users.IsExist(login))
            {
                this.users.RemoveAccount(login);
                this.logger.Info($"Аккаунт {login} вышел из системы");
            }
            else
            {
                this.logger.Error($@"Незарегистрированный аккаунт {login} вышел 
                    из системы");
            }
            return true;
        }
    }
}
