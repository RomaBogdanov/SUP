using System.Data;
using System.Threading.Tasks;
using SupContract;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Threading;
using SupHost.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using SupHost.Andover;

namespace SupHost
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class TableService1 : ITableService
    {
        ITableCallback callback;
        readonly int timeOut = 3000;
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
		/// Процедура получения изображения в виде набора байтов.
		/// </summary>
		/// <returns></returns>
		public byte[] GetImageUsingParametr(Guid alias, OperationInfo info, bool? isNotDeleted)
		{
			if (isNotDeleted == null)
				return GetImage(alias, info);
			else
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
							"select f_data, f_deleted from vis_image WHERE f_image_alias=@alias";
						sqlCommand.Parameters.AddWithValue("@alias", alias);
						using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
						{
							while (sqlReader.Read())
							{
								if (Test_DeletedParam(sqlReader, isNotDeleted.Value))
								{
									object value = sqlReader["f_data"];
									if (value != null && value != DBNull.Value && value is byte[])
									{
										bytes = (byte[])sqlReader["f_data"];
										break;
									}
								}
							}
						}
					}
					cn.Close();
				}
				//string.Equals(docRow?.Field<string>("f_deleted").Trim().ToLower(), "y")

				return bytes;
			}
		}

		/// <summary>
		/// Процедура загрузки изображения в базу.
		/// </summary>
		/// <returns></returns>
		public void SetImages(Dictionary<Guid, byte[]> images, OperationInfo info)
        {
            var connector = new VisServerImagesTableWrapper().GetConnector();
            using (SqlConnection cn = new SqlConnection(connector.ToString()))
            {
                cn.Open();
                foreach (var image in images)
                {
                    using (SqlCommand sqlCommand = cn.CreateCommand())
                    {
                        sqlCommand.CommandText =
                            "update vis_image set f_data=@data WHERE f_image_alias=@alias";
                        sqlCommand.Parameters.AddWithValue("@data", image.Value.Clone());
                        sqlCommand.Parameters.AddWithValue("@alias", image.Key);
                        int rows = sqlCommand.ExecuteNonQuery();

                        if (rows == 1)
                        {
                            logger.Debug($"Добавлено изображение", info);
                        }
                    }
                }
                cn.Close();
            }
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

        public bool ImportFromAndover(string tables,OperationInfo info)
        {
            logger.Info($"Импорт данных из Andover", info);

            var andoverManager = new AndoverManager(info);
            return andoverManager.Import(tables);
        }

        public CExtraditionContract ExportToAndover(AndoverExportData data, OperationInfo info)
        {
            logger.Info($"Экспорт данных в Andover", info);

            var andoverManager = new AndoverManager(info);
            var result = andoverManager.Export(data);

		return new CExtraditionContract
		{
			Success = result.Success,
			IsExtradition = result.IsExtradition,
			ExtraditionSuccess = result.ExtraditionSuccess
		};
        }

	    public int GetDocumentTypeId(string documentType, int operatorId)
	    {
		    if (string.IsNullOrEmpty(documentType))
		    {
			    return 0;
		    }

		    VisDocumentsTableWrapper documentsTableWrapper =
			    (VisDocumentsTableWrapper) VisDocumentsTableWrapper.GetTableWrapper(TableName.VisDocuments);
		    int typeId;
		    try
		    {
			    typeId = (from row in documentsTableWrapper.GetTable().AsEnumerable()
				    where string.Equals(row.Field<string>("f_doc_name").Trim().ToLower(), documentType.ToLower())
				    select row.Field<int>("f_doc_id")).FirstOrDefault();
		    }
		    catch (Exception)
		    {
			    return 0;
		    }

		    if (typeId == 0)
		    {
			    DataRow newRow;
			    try
			    {
				    newRow = documentsTableWrapper.GetTable().NewRow();

				    var maxId = GetMaxId(documentsTableWrapper, "f_doc_id");
				    newRow["f_doc_id"] = ++maxId;
				    newRow["f_doc_name"] = documentType;
				    newRow["f_deleted"] = "N";
				    newRow["f_rec_date"] = DateTime.Now;
				    newRow["f_rec_operator"] = operatorId;
				    documentsTableWrapper.InsertRow(newRow.ItemArray, null);
				    return maxId;

			    }
			    catch (Exception)
			    {
				    return 0;
			    }
		    }

		    var docRow = (from row in documentsTableWrapper.GetTable().AsEnumerable()
			    where string.Equals(row.Field<string>("f_doc_name").Trim().ToLower(), documentType.ToLower())
			    select row).FirstOrDefault();

		    
		    if (string.Equals(docRow?.Field<string>("f_deleted").Trim().ToLower(),"y"))
		    {
				try
				{
					if (docRow != null)
					{
						docRow["f_deleted"] = "N";
						var numRow = documentsTableWrapper.GetTable().Rows.IndexOf(docRow);
						documentsTableWrapper.UpdateRow(docRow.ItemArray, numRow, null);
					}
				}
				catch (Exception)
				{
					// ignored
				}
			}
		    
		    return typeId;
	    }

	    private static int GetMaxId(AbstractTableWrapper wrapper, string idField)
	    {
		    var maxId = 0;
		    foreach (DataRow row in wrapper.GetTable().Rows)
		    {
			    if ((int)row[idField] > maxId)
			    {
				    maxId = (int)row[idField];
			    }
		    }
		    return maxId;
	    }

		private bool Test_DeletedParam(SqlDataReader sqlDataReader, bool deletedParam)
		{
			object value1 = sqlDataReader["f_data"];
			object value= sqlDataReader["f_deleted"];
			return Test_DeletedParam(value, deletedParam);
		}

		private bool Test_DeletedParam(DataRow row, bool deletedParam)
		{
			object value = row?.Field<string>("f_deleted");
			return Test_DeletedParam(value, deletedParam);
		}

		private bool Test_DeletedParam(object value, bool deletedParam)
		{
			if (value != null && value != DBNull.Value && value is string)
			{
				if (deletedParam)
					return !string.Equals((value as string).Trim().ToLower(), "y");
				else
					return string.Equals((value as string).Trim().ToLower(), "y");
			}
			else
			{
				// Считаем по умолчанию N, если поле является пустым или DBNull
				if (deletedParam)
					return true;
				else
					return false;
			}
		}
	}
}
