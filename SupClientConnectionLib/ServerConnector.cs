using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
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
    public class ClientConnector
    {
        private static ClientConnector connector;
        ITableService tableService;
        CompositeType compositeType;

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

        public DataTable GetTable(TableName tableName)
        {
            this.compositeType.TableName = tableName;
            return this.tableService.GetTable(this.compositeType);
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
            return this.tableService.InsertRow(compositeType, rowValues);
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
            return this.tableService.UpdateRow(compositeType, numRow, rowValues);
        }

        public bool DeleteRow(int numRow)
        {
            return this.tableService.DeleteRow(compositeType, numRow);
        }

        public byte[] GetImage(int id)
        {
            return this.tableService.GetImage(id);
        }
        
        public ClientConnector()
        {
            this.tableService = new TableServiceClient();
            this.compositeType = new CompositeType();
        }

        #endregion
    }
}
