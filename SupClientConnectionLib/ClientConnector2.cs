using SupContract;
using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using System.Xml;

namespace SupClientConnectionLib
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    internal class ClientConnector2 : IClientConnector
    {
        private ITableService tableService;
        private CompositeType compositeType = new CompositeType();
        private Authorizer authorizer;

        private NewMessageHandler messageHandler;
        private InstanceContext instanceContext;

        private readonly object synchroObj = new object();

        public event Action<string, object[]> OnInsert;
        public event Action<string, int, object[]> OnUpdate;
        public event Action<string, object[]> OnDelete;

        public ClientConnector2()
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

        public int Authorize(string login, string pass)
        {
            lock (synchroObj)
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
                    catch (Exception err2)
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
                    catch (Exception err2)
                    {
                        Logger.Log.Error(err2.GetType() + err2.Message + err2.StackTrace);
                        authorizer.Id = -999; // Сюда приходим, если сервер указан неправильно.
                    }
                }

                return authorizer.Id;
            }
        }

        public bool Ping()
        {
            bool result = false;
            Execute(() =>
            {
                result = this.tableService.Ping(authorizer.GetInfo()) == "OK";
            });
            return result;
        }

        public bool ExitAuthorize()
        {
            bool result = false;
            Execute(() =>
            {
                result = this.tableService.ExitAuthorize(authorizer.GetInfo());
            });
            return result;
        }

        public int GetDocumentTypeId(string documentType, int operatorId)
        {
            int result = -1;
            Execute(() =>
            {
                result = this.tableService.GetDocumentTypeId(documentType, operatorId);
            });
            return result;
        }

        public bool ImportFromAndover(string tables)
        {
            bool result = false;
            Execute(() =>
            {
                result = this.tableService.ImportFromAndover(tables, authorizer.GetInfo());
            });
            return result;
        }

        public CExtraditionContract ExportToAndover(AndoverExportData data)
        {
            CExtraditionContract result = null;
            Execute(() =>
            {
                result = this.tableService.ExportToAndover(data, authorizer.GetInfo());
            });
            return result;
        }

        public DataTable GetTable(TableName tableName)
        {
            DataTable result = null;
            Execute(() =>
            {
                this.compositeType.TableName = tableName;
                result = this.tableService.GetTable(this.compositeType,
                    authorizer.GetInfo());
            });
            return result;
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

            bool result = false;
            Execute(() =>
            {
                while (!result)
                {
                    Logger.Log.Debug("Начало отправки данных по новой строке");
                    result = this.tableService.InsertRow(compositeType, rowValues,
                        authorizer.GetInfo());
                }
            });

            Logger.Log.Debug($"Окончание отправки данных по новой строке. Результат отправки: {result}");
            string st = "";
            foreach (var item in rowValues)
            {
                st += item + " ";
            }
            Logger.Log.Debug($"Добавление строки в таблицу " +
                $"{compositeType.TableName}: {result} Значение: {st} ");

            return result;
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

            bool result = false;
            Execute(() =>
            {
                while (!result)
                {
                    Logger.Log.Debug("Начало редактирования данных строки");
                    result = this.tableService.UpdateRow(compositeType, numRow, rowValues,
                        authorizer.GetInfo());
                }
            });

            Logger.Log.Debug($"Окончание редактирования данных по строке. Результат отправки: {result}");
            string st = "";
            foreach (var item in rowValues)
            {
                st += item + " ";
            }
            Logger.Log.Debug($"Редактирование строки в таблице " +
                $"{compositeType.TableName}: {result} Значение: {st} ");

            return result;
        }

        public bool DeleteRow(object[] objs)
        {
            Logger.Log.Debug("Начало процедуры DeleteRow");

            bool result = false;
            Execute(() =>
            {
                while (!result)
                {
                    result = this.tableService.DeleteRow(compositeType, objs,
                        authorizer.GetInfo());
                }
            });

            Logger.Log.Debug($"Окончание удаления строки. Результат отправки: {result}");
            string st = "";
            foreach (var item in objs)
            {
                st += item + " ";
            }
            Logger.Log.Debug($"Удаление строки в таблице " +
                $"{compositeType.TableName}: {result} Значение: {st} ");

            return result;
        }

        public byte[] GetImage(Guid alias)
        {
            lock (synchroObj)
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
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public byte[] GetImage(Guid alias, bool isDeletedParam)
        {
            lock (synchroObj)
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
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public bool SetImages(Dictionary<Guid, byte[]> images)
        {
            Logger.Log.Debug("Начало процедуры SetImages");

            bool result = Execute(() =>
            {
                Logger.Log.Debug("Начало попытки загрузить изображение в базу данных");
                this.tableService.SetImages(images, authorizer.GetInfo());

                Logger.Log.Debug("Успешное окончание попытки загрузить изображение в базу данных");
            });
            
            return result;
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
                instanceContext, binding, new EndpointAddress(ClientConnectorFactory.Uri));
            this.tableService = myChannelFactory.CreateChannel();
            //this.compositeType = new CompositeType();
        }

        private bool Execute(Action action)
        {
            bool repeat = false;
            lock (synchroObj)
            {
                do
                {
                    try
                    {
                        action();
                    }
                    catch (CommunicationObjectFaultedException err)
                    {
                        Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);

                        Logger.Log.Debug("Пытаемся перезапустить соединение");
                        ResetConnection();
                        repeat = true;
                    }
                    catch (Exception err)
                    {
                        Logger.Log.Error(err.GetType() + err.Message + err.StackTrace);
                        return false;
                    }
                }
                while (!repeat);
            }

            return true;
        }
    }
}
