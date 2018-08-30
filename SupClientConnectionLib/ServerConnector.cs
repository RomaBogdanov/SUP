using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.Xml;
using SupContract;
using log4net;

namespace SupClientConnectionLib
{
    /// <summary>
    /// Класс инкапсюлирующий подключение со стороны клиента к серверу.
    /// </summary>
    /// <remarks>
    /// Реализует паттерн одиночка для организации единого подключения
    /// из всех точек приложения.
    /// </remarks>
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class ClientConnector
    {
        private static ClientConnector connector;
        private static Uri uri;
        ITableService tableService;
        CompositeType compositeType = new CompositeType();
        Authorizer authorizer;

        public event Action<string, object[]> OnInsert;
        public event Action<string, int, object[]> OnUpdate;
        public event Action<string, object[]> OnDelete;

        #region Public

        public static ClientConnector CurrentConnector
        {
            get
            {
                if (connector == null)
                {
                    connector = new ClientConnector();
                    return connector;
                }
                return connector;
            }
        }

        public static ClientConnector ResetConnector(Uri uri)
        {
            ClientConnector.uri = uri;
            connector = new ClientConnector();
            return connector;
        }

        public int Authorize(string login, string pass)
        {
            authorizer.Login = login ?? "";
            authorizer.Id = -1;
            try
            {
                authorizer.Id = this.tableService.Authorize(
                    authorizer.GetInfo(), pass ?? "");
            }
            catch (ProtocolException)
            {
                ResetConnection();
                try
                {
                    authorizer.Id = this.tableService.Authorize(
                        authorizer.GetInfo(), pass ?? "");
                }
                catch (Exception err)
                {
                    Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);
                }
            }
            catch (CommunicationObjectFaultedException err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);
                ResetConnection();
                try
                {
                    authorizer.Id = this.tableService.Authorize(
                        authorizer.GetInfo(), pass ?? "");
                }
                catch(Exception err2)
                {
                    Logger.Log.Error(err2.GetType() + err2.Message + err2.StackTrace);
                }
            }
            catch (EndpointNotFoundException err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);
                ResetConnection();
                try
                {
                    authorizer.Id = this.tableService.Authorize(
                        authorizer.GetInfo(), pass ?? "");
                }
                catch(Exception err2)
                {
                    Logger.Log.Error(err2.GetType() + err2.Message + err2.StackTrace);
                    authorizer.Id = -999; // Сюда приходим, если сервер указан неправильно.
                }
            }

            return authorizer.Id;
        }

        public bool Ping()
        {
            Attempt:
            try
            {
                bool b = this.tableService.Ping(authorizer.GetInfo()) == "OK";
                return b;
            }
            catch (CommunicationObjectFaultedException err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);
                Logger.Log.Debug("Пытаемся перезапустить соединение");
                ResetConnection();
                goto Attempt;
            }
            catch (Exception err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);
                return false;
            }
        }

        public bool ExitAuthorize()
        {
            Attempt:
            try
            {
                bool b = this.tableService.ExitAuthorize(authorizer.GetInfo());
                return b;
            }
            catch (CommunicationObjectFaultedException err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);
                Logger.Log.Debug("Пытаемся перезапустить соединение");
                ResetConnection();
                goto Attempt;
            }
            catch (Exception err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);
                return false;
            }
        }

	    public int GetDocumentTypeId(string documentType, int operatorId)
	    {
            Attempt:
            try
            {
                int b = this.tableService.GetDocumentTypeId(documentType, operatorId);
                return b;
            }
            catch (CommunicationObjectFaultedException err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);
                Logger.Log.Debug("Пытаемся перезапустить соединение");
                ResetConnection();
                goto Attempt;
            }
            catch (Exception err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);
                return -1;
            }
	    }

		public bool ImportFromAndover(string tables)
        {
            Attempt:
            try
            {
                bool b = this.tableService.ImportFromAndover(tables, authorizer.GetInfo());
                return b;
            }
            catch (CommunicationObjectFaultedException err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);
                Logger.Log.Debug("Пытаемся перезапустить соединение");
                ResetConnection();
                goto Attempt;
            }
            catch (Exception err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);
                return false;
            }
        }

        public CExtraditionContract ExportToAndover(AndoverExportData data)
        {
            Attempt:
            try
            {
                CExtraditionContract b = this.tableService.ExportToAndover(data, authorizer.GetInfo());
                return b;
            }
            catch (CommunicationObjectFaultedException err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);
                Logger.Log.Debug("Пытаемся перезапустить соединение");
                ResetConnection();
                goto Attempt;
            }
            catch (Exception err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);
                return null;
            }
        }

        public DataTable GetTable(TableName tableName)
        {
            Attempt:
            try
            {
                this.compositeType.TableName = tableName;
                DataTable dataTable = this.tableService.GetTable(this.compositeType,
                    authorizer.GetInfo());
                return dataTable;
            }
            catch (CommunicationObjectFaultedException err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);
                Logger.Log.Debug("Пытаемся перезапустить соединение");
                ResetConnection();
                goto Attempt;
            }
            catch (Exception err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);
                return null;
            }
        }

        public bool InsertRow(object[] rowValues)
        {
            Logger.Log.Debug("Начало процедуры InsertRow");
            for (int i = 0; i < rowValues.Length; i++)
            {
                if (rowValues[i] is DBNull)
                {
                    rowValues[i] = "";
                }
            }
            bool b=false;
            /*lock (this.tableService)
            {*/
            Attempt:
            Logger.Log.Debug("Начало отправки данных по новой строке");
            try
            {
                b = this.tableService.InsertRow(compositeType, rowValues,
                    authorizer.GetInfo());
                if (!b)
                {
                    goto Attempt;
                }
            }
            catch (CommunicationObjectFaultedException err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);

                Logger.Log.Debug("Пытаемся перезапустить соединение");
                ResetConnection();
                goto Attempt;
            }
            catch (InvalidOperationException err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);
            }
            catch (Exception err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);
            }
                Logger.Log.Debug($"Окончание отправки данных по новой строке. Результат отправки: {b}");
                string st = "";
                foreach (var item in rowValues)
                {
                    st += item + " ";
                }
                Logger.Log.Debug($"Добавление строки в таблицу " +
                    $"{compositeType.TableName}: {b} Значение: {st} ");
            //}
            return b;
        }

        public bool UpdateRow(object[] rowValues, int numRow)
		{
            Logger.Log.Debug("Начало процедуры UpdateRow");
            for (int i = 0; i < rowValues.Length; i++)
            {
                if (rowValues[i] is DBNull)
                {
                    rowValues[i] = "";
                }
            }
            bool b=false;
            /*lock (this.tableService)
            {*/
                Attempt:
                Logger.Log.Debug("Начало редактирования данных строки");
                try
                {
                    b = this.tableService.UpdateRow(compositeType, numRow, rowValues,
                        authorizer.GetInfo());
                if (!b)
                {
                    goto Attempt;
                }
            }
            catch(CommunicationObjectFaultedException err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);

                Logger.Log.Debug("Пытаемся перезапустить соединение");
                ResetConnection();
                goto Attempt;
            }
            catch(InvalidOperationException err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);

            }
            catch (Exception err)
                {
                    Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);
                }
                Logger.Log.Debug($"Окончание редактирования данных по строке. Результат отправки: {b}");

            string st = "";
                foreach (var item in rowValues)
                {
                    st += item + " ";
                }
                Logger.Log.Debug($"Редактирование строки в таблице " +
                    $"{compositeType.TableName}: {b} Значение: {st} ");
            //}
            return b;
        }

        public bool DeleteRow(object[] objs)
        {
            Logger.Log.Debug("Начало процедуры DeleteRow");
            bool b = false;
            Attempt:
            try
            {
                b = this.tableService.DeleteRow(compositeType, objs,
                    authorizer.GetInfo());
                if (!b)
                {
                    goto Attempt;
                }
            }
            catch (CommunicationObjectFaultedException err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);

                Logger.Log.Debug("Пытаемся перезапустить соединение");
                ResetConnection();
                goto Attempt;
            }
            catch (Exception err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);
            }
            Logger.Log.Debug($"Окончание удаления строки. Результат отправки: {b}");
            string st = "";
            foreach (var item in objs)
            {
                st += item + " ";
            }
            Logger.Log.Debug($"Удаление строки в таблице " +
                $"{compositeType.TableName}: {b} Значение: {st} ");
            return b;
        }

        public byte[] GetImage(Guid alias)
        {
			try
			{
				object result = this.tableService.GetImage(alias,
	authorizer.GetInfo());

				if (result != DBNull.Value)
				{
					return (byte[])result;
				}

				return null;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public byte[] GetImage(Guid alias, bool isDeletedParam )
		{
			try
			{
				object result = this.tableService.GetImageUsingParametr(alias,
	authorizer.GetInfo(), isDeletedParam);

				if (result != DBNull.Value)
				{
					return (byte[])result;
				}

				return null;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public bool SetImages(Dictionary<Guid, byte[]> images)
        {
            Logger.Log.Debug("Начало процедуры SetImages");
            Attempt:
            try
            {
                Logger.Log.Debug("Начало попытки загрузить изображение в базу данных");
                this.tableService.SetImages(images, authorizer.GetInfo());

                Logger.Log.Debug("Успешное окончание попытки загрузить изображение в базу данных");
            }
            catch (CommunicationObjectFaultedException err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);

                Logger.Log.Debug("Пытаемся перезапустить соединение");
                ResetConnection();
                goto Attempt;
            }
            catch (TimeoutException err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);
            }
            catch (Exception err)
            {
                Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);
                return false;
            }
            return true;
        }

        NewMessageHandler messageHandler;
        InstanceContext instanceContext;

        public ClientConnector()
        {
            Logger.InitLogger();
            authorizer = Authorizer.AppAuthorizer;
            messageHandler = new NewMessageHandler();
            messageHandler.OnInsert += MessageHandler_OnInsert;
            messageHandler.OnUpdate += MessageHandler_OnUpdate;
            messageHandler.OnDelete += MessageHandler_OnDelete;
            instanceContext = new InstanceContext(messageHandler);
            ResetConnection();
        }

        private void MessageHandler_OnInsert(string tableName, object[] objs)
        {
            this.OnInsert?.Invoke(tableName, objs);
        }

        private void MessageHandler_OnUpdate(string tableName, int rowNumber, 
            object[] objs)
        {
            this.OnUpdate?.Invoke(tableName, rowNumber, objs);
        }

        private void MessageHandler_OnDelete(string tableName, object[] objs)
        {
            this.OnDelete?.Invoke(tableName, objs);
        }

        private void ResetConnection()
        {
            var binding = new WSDualHttpBinding()
            {
                MaxReceivedMessageSize = 2147483647,
                MaxBufferPoolSize = 2147483647,
                ReaderQuotas = new XmlDictionaryReaderQuotas
                {
                    MaxArrayLength = 2147483647,
                    MaxStringContentLength = 2147483647
                },
                ReceiveTimeout = TimeSpan.FromMinutes(5),
                SendTimeout = TimeSpan.FromMinutes(5)
            };
            var myChannelFactory = new DuplexChannelFactory<ITableService>(
                instanceContext, binding, new EndpointAddress(ClientConnector.uri));
            this.tableService = myChannelFactory.CreateChannel();
            //this.compositeType = new CompositeType();
        }

        #endregion
    }

    public class NewMessageHandler : ITableCallback
    {
        public event Action<string, object[]> OnInsert;
        public event Action<string, int, object[]> OnUpdate;
        public event Action<string, object[]> OnDelete;

        public void InsRow(string tableName, object[] objs)
        {
            this.OnInsert?.Invoke(tableName, objs);
        }

        public void UpdRow(string tableName, int rowNumber, object[] objs)
        {
            this.OnUpdate?.Invoke(tableName, rowNumber, objs);
        }

        public void DelRow(string tableName, object[] objs)
        {
            this.OnDelete?.Invoke(tableName, objs);
        }

	}
}
 