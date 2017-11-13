using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SupTestClient.ClientServiceReference;

namespace SupTestClient
{
    /// <summary>
    /// Класс инкапсюлирующий подключение со стороны клиента к серверу.
    /// </summary>
    /// <remarks>
    /// Реализует паттерн одиночка для организации единого подключения
    /// из всех точек приложения.
    /// </remarks>
    class ClientConnector
    {
        private static ClientConnector connector;
        ITableService tableService;
        CompositeType compositeType;

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

        #endregion

        #region Private

        private ClientConnector()
        {
            this.tableService = new TableServiceClient();
            this.compositeType = new CompositeType();
        }

        #endregion

    }
}
