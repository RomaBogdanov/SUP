using System;
using System.Data;
using System.ServiceModel;
using System.Xml;
using SupContract;

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
        CompositeType compositeType;
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
                catch
                {
                }
            }
            catch (CommunicationObjectFaultedException)
            {
                ResetConnection();
                try
                {
                    authorizer.Id = this.tableService.Authorize(
                        authorizer.GetInfo(), pass ?? "");
                }
                catch
                {
                }
            }
            catch (EndpointNotFoundException)
            {
                ResetConnection();
                try
                {
                    authorizer.Id = this.tableService.Authorize(
                        authorizer.GetInfo(), pass ?? "");
                }
                catch
                {
                }
            }

            return authorizer.Id;
        }
        
        public bool Ping()
        {
            try
            {
                return this.tableService.Ping(authorizer.GetInfo()) == "OK";
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ExitAuthorize()
        {
            return this.tableService.ExitAuthorize(authorizer.GetInfo());
        }

        public DataTable GetTable(TableName tableName)
        {
            this.compositeType.TableName = tableName;
            return this.tableService.GetTable(this.compositeType,
                authorizer.GetInfo());
        }

        public bool InsertRow(object[] rowValues)
        {
            for (int i = 0; i < rowValues.Length; i++)
            {
                if (rowValues[i] as DBNull != null)
                {
                    rowValues[i] = "";
                }
            }
            bool b;
            lock (this.tableService)
            {
                b = this.tableService.InsertRow(compositeType, rowValues,
                    authorizer.GetInfo());
            }
            return b;
        }

        public bool UpdateRow(object[] rowValues, int numRow)
        {
            for (int i = 0; i < rowValues.Length; i++)
            {
                if (rowValues[i] as DBNull != null)
                {
                    rowValues[i] = "";
                }
            }
            bool b;
            lock (this.tableService)
            {
                b = this.tableService.UpdateRow(compositeType, numRow, rowValues,
                    authorizer.GetInfo());
            }
            return b;
        }

        public bool DeleteRow(object[] objs)
        {
            return this.tableService.DeleteRow(compositeType, objs,
                authorizer.GetInfo());
        }

        public byte[] GetImage(int id)
        {
            return this.tableService.GetImage(id,
                authorizer.GetInfo());
        }

        NewMessageHandler messageHandler;
        InstanceContext instanceContext;

        public ClientConnector()
        {
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
                    MaxStringContentLength = 2147483647,
                    MaxBytesPerRead = 2147483647
                }
            };
            var myChannelFactory = new DuplexChannelFactory<ITableService>(
                instanceContext, binding, new EndpointAddress(ClientConnector.uri));
            this.tableService = myChannelFactory.CreateChannel();
            this.compositeType = new CompositeType();
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
