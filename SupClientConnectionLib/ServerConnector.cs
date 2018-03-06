using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ServiceModel;
using SupClientConnectionLib.ServiceRef;


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
        ITableService tableService;
        CompositeType compositeType;
        Authorizer authorizer;

        public event Action<string, object[]> OnInsert;
        public event Action<string, int, object[]> OnUpdate;
        public event Action<string, object[]> OnDelete;

        #region Public

        //TODO: убрать синглтон из данного класса, т.к. он не используется.
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

        public bool Authorize(string login, string pass)
        {
            authorizer.Login = login;
            return this.tableService.Authorize(authorizer.Login, pass);
        }

        public bool ExitAuthorize()
        {
            return this.tableService.ExitAuthorize(authorizer.Login);
        }

        public DataTable GetTable(TableName tableName)
        {
            this.compositeType.TableName = tableName;
            return this.tableService.GetTable(this.compositeType, 
                authorizer.Login);
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
                    authorizer.Login);
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
            return this.tableService.UpdateRow(compositeType, numRow, 
                rowValues, authorizer.Login);
        }

        public bool DeleteRow(object[] objs)
        {
            return this.tableService.DeleteRow(compositeType, objs, 
                authorizer.Login);
        }

        public byte[] GetImage(int id)
        {
            return this.tableService.GetImage(id, authorizer.Login);
        }
        
        public ClientConnector()
        {
            authorizer = Authorizer.AppAuthorizer;
            NewMessageHandler messageHandler = new NewMessageHandler();
            messageHandler.OnInsert += MessageHandler_OnInsert;
            messageHandler.OnUpdate += MessageHandler_OnUpdate;
            messageHandler.OnDelete += MessageHandler_OnDelete;
            InstanceContext instanceContext = new InstanceContext(messageHandler);
            this.tableService = new TableServiceClient(instanceContext);
            this.compositeType = new CompositeType();
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

        #endregion
    }

    public class NewMessageHandler : ITableServiceCallback
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
