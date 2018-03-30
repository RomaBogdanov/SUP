using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SupClientConnectionLib;
using SupClientConnectionLib.ServiceRef;

namespace SupRealClient
{
    class TableWrapper
    {
        protected DataTable table;
        protected ClientConnector connector;
        public event Action OnChanged;

        public DataTable Table { get { return this.table; } }

        public ClientConnector Connector { get { return this.connector; } }

        protected TableWrapper()
        {
            this.connector = new ClientConnector();
            this.connector.OnInsert += Connector_OnInsert;
            this.connector.OnUpdate += Connector_OnUpdate;
            this.connector.OnDelete += Connector_OnDelete;
        }

        private void Connector_OnInsert(string tableName, object[] objs)
        {
            if (tableName == table.TableName && !this.table.Rows.Contains(objs[0]))
            {
                table.Rows.Add(objs);
                this.OnChanged();
            }
        }

        private void Connector_OnUpdate(string tableName, int rowNumber, object[] objs)
        {
            if (tableName == table.TableName)
            {
                DataRow row = table.Rows.Find(objs[0]);
                if (row != null)
                {
                    int l = row.ItemArray.Length;
                    for (int i = 0; i < l; i++)
                    {
                        if (row.Table.Columns[i].DataType == typeof(int))
                        {
                            objs[i] = objs[i].ToString() == "" ? 0 : objs[i];
                        }
                    }
                    row.ItemArray = objs;
                    table.AcceptChanges();
                    this.OnChanged();
                }
            }
        }

        private void Connector_OnDelete(string tableName, object[] objs)
        {
            if (tableName == table.TableName)
            {
                DataRow row = table.Rows.Find(objs[0]);
                if (row != null)
                {
                    table.Rows.Remove(row);
                    this.OnChanged();
                }
            }
        }

        protected void Subscribe()
        {
            this.table.RowChanged += Table_RowChanged;
            this.table.RowDeleting += Table_RowDeleting;
        }

        private void Table_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Table_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            // TODO: перевести взаимодействие таблиц с сервером в табличные обёртки. 
            DataTable dt;
            switch (e.Action)
            {
                case DataRowAction.Nothing:
                    break;
                case DataRowAction.Delete:
                    break;
                case DataRowAction.Change:
                    dt = (DataTable)sender;
                    int i = dt.Rows.IndexOf(e.Row);
                    this.OnChanged();
                    this.Connector.UpdateRow(e.Row.ItemArray, i);
                    break;
                case DataRowAction.Rollback:
                    break;
                case DataRowAction.Commit:
                    break;
                case DataRowAction.Add:
                    dt = (DataTable)sender;
                    this.OnChanged();
                    this.Connector.InsertRow(e.Row.ItemArray);
                    break;
                case DataRowAction.ChangeOriginal:
                    break;
                case DataRowAction.ChangeCurrentAndOriginal:
                    break;
                default:
                    break;
            }
        }
    }
}
