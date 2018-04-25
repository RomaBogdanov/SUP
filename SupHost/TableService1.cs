using System.Data;
using System.Threading.Tasks;
using SupContract;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Threading;
using SupHost.Data;
using System;

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
        /// Процедура получения изображения в виде набора байтов.
        /// </summary>
        /// <returns></returns>
        public byte[] GetImage(Guid alias, OperationInfo info)
        {
            byte[] bytes = null;
            var connector = new VisServerImagesTableWrapper().GetConnector();
            using (SqlConnection cn = new SqlConnection(connector.ToString()))
            {
                cn.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = cn;
                    sqlCommand.CommandText =
                        "select f_data from vis_image WHERE f_image_alias=@alias";
                    sqlCommand.Parameters.AddWithValue("@alias", alias);
                    using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlReader.Read())
                        {
                            bytes = (byte[])sqlReader["f_data"];
                        }
                    }
                }
                cn.Close();
            }

            return bytes;
        }

        /// <summary>
        /// Процедура загрузки изображения в базу.
        /// </summary>
        /// <returns></returns>
        public bool SetImage(Guid alias, byte[] data, OperationInfo info)
        {
            int rows = 1;
            var connector = new VisServerImagesTableWrapper().GetConnector();
            //using (SqlConnection cn = new SqlConnection(connector.ToString()))
            SqlConnection cn = new SqlConnection(connector.ToString());
            {
                cn.Open();
                //using (SqlCommand sqlCommand = cn.CreateCommand())
                SqlCommand sqlCommand = cn.CreateCommand();
                {
                    sqlCommand.CommandText =
                        "update vis_image set f_data=@data WHERE f_image_alias=@alias";
                    sqlCommand.Parameters.AddWithValue("@data", data);
                    sqlCommand.Parameters.AddWithValue("@alias", alias);
                    //rows = sqlCommand.BeginExecuteNonQuery();
                    AsyncCallback callback = new AsyncCallback(SetImageCallback);
                    IAsyncResult result = sqlCommand.BeginExecuteNonQuery(callback, sqlCommand);
                }
                //cn.Close();
            }

            if (rows > 0)
            {
                logger.Debug($"Добавлено изображение", info);
            }
            return rows > 0;
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
        {
            users.CheckAccount(new UserData { Id = info.Id });
            return "OK";
        }

        public bool ExitAuthorize(OperationInfo info)
        {
            if (this.users.IsExist(info.User))
            {
                this.users.RemoveAccount(new UserData { Id = info.Id });
                this.logger.Info($"Аккаунт {info.User} вышел из системы", info);
            }
            else
            {
                this.logger.Error($@"Незарегистрированный аккаунт {info.User} вышел 
                    из системы");
            }
            return true;
        }

        private void SetImageCallback(IAsyncResult result)
        {
            SqlCommand sqlCommand = (SqlCommand)result.AsyncState;
            try
            {
                int rowCount = sqlCommand.EndExecuteNonQuery(result);
                if (rowCount == 1)
                {
                    // TODO
                }
            }
            catch (Exception ex)
            {
                // TODO
            }
            finally
            {
                if (sqlCommand.Connection != null)
                {
                    sqlCommand.Connection.Close();
                    sqlCommand.Connection.Dispose();
                    sqlCommand.Dispose();
                }
            }
        }
    }
}
