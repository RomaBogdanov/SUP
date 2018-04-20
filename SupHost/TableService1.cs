using System.Data;
using System.Threading.Tasks;
using SupContract;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Threading;
using SupHost.Data;

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
        public DataTable GetTable(CompositeType composite, OperationInfo info)
        {
            if (!this.users.IsExist(info.User))
            {
                this.logger.Warn($@"Попытка запроса к серверу 
                    незарегистрированным аккаунтом {info.User}");
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
        public bool InsertRow(CompositeType composite, object[] objs, OperationInfo info)
        {
            if (!this.users.IsExist(info.User))
            {
                this.logger.Warn($@"Попытка запроса к серверу 
                    незарегистрированным аккаунтом {info.User}");
                return false;
            }
            AbstractTableWrapper tableWrapper =
                AbstractTableWrapper.GetTableWrapper(composite.TableName);
            return tableWrapper?.InsertRow(objs, info) ?? false;
        }

        /// <summary>
        /// Обновление строки в таблице.
        /// </summary>
        /// <param name="composite"></param>
        /// <param name="rowNumber"></param>
        /// <param name="objs"></param>
        /// <returns></returns>
        public bool UpdateRow(CompositeType composite, int rowNumber, object[] objs, OperationInfo info)
        {
            if (!this.users.IsExist(info.User))
            {
                this.logger.Warn($@"Попытка запроса к серверу 
                    незарегистрированным аккаунтом {info.User}");
                return false;
            }
            AbstractTableWrapper tableWrapper =
                AbstractTableWrapper.GetTableWrapper(composite.TableName);
            return tableWrapper?.UpdateRow(objs, rowNumber, info) ?? false;
        }

        /// <summary>
        /// Удаление строки из таблицы.
        /// </summary>
        /// <param name="composite"></param>
        /// <param name="objs"></param>
        /// <returns></returns>
        public bool DeleteRow(CompositeType composite, object[] objs, OperationInfo info)
        {
            if (!this.users.IsExist(info.User))
            {
                this.logger.Warn($@"Попытка запроса к серверу 
                    незарегистрированным аккаунтом {info.User}");
                return false;
            }
            AbstractTableWrapper tableWrapper =
                AbstractTableWrapper.GetTableWrapper(composite.TableName);
            return tableWrapper?.DeleteRow(objs, info) ?? false;
        }

        /// <summary>
        /// Процедура получения изображения.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public byte[] GetImage(int id, OperationInfo info)
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

        public int Authorize(OperationInfo info, string pass)
        {
            //TODO: обязательно переработать. Данный вариант выступает как заглушка.
            // Проверка на существование уже зарегистрированного пользователя.
            if (this.users.IsExist(info.User))
            {
                logger.Warn($@"Попытка зарегистрироваться под уже 
                    зарегистрированным аккаунтом {info.User}");
                return -1;
            }
            VisUsersTableWrapper visUsersTableWrapper =
                VisUsersTableWrapper.GetVisUsersTableWrapper();
            int id = visUsersTableWrapper.ExistingLogin(info.User, pass);
            if (id > 0)
            {
                info.Id = id;
                this.users.AddAccount(visUsersTableWrapper.GetUserTimeoutData(id));
                logger.Info($"Зарегистрировался аккаунт {info.User}", info);
                return id;
            }
            return -1;
        }

        public string Ping(OperationInfo info)
        //public bool CheckAuthorize(OperationInfo info)
        {
            return "OK";
            //return users.CheckAccount(new UserTimeoutData { Id = info.Id });
        }

        public bool ExitAuthorize(OperationInfo info)
        {
            if (this.users.IsExist(info.User))
            {
                this.users.RemoveAccount(new UserTimeoutData { Id = info.Id });
                this.logger.Info($"Аккаунт {info.User} вышел из системы", info);
            }
            else
            {
                this.logger.Error($@"Незарегистрированный аккаунт {info.User} вышел 
                    из системы");
            }
            return true;
        }
    }
}
