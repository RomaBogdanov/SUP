using System;
using System.Collections.Generic;
using System.Data;
using SupClientConnectionLib;

namespace SupRealClient.TabsSingleton
{
    class TableWrapper : IDisposable
    {
        protected DataTable table;
        protected ClientConnector connector;
        protected static List<TableWrapper> wrappers = new List<TableWrapper>();
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

        public void Dispose()
        {
            this.connector.OnInsert -= Connector_OnInsert;
            this.connector.OnUpdate -= Connector_OnUpdate;
            this.connector.OnDelete -= Connector_OnDelete;
            if (this.table != null)
            {
                this.table.RowChanged -= Table_RowChanged;
                this.table.RowDeleting -= Table_RowDeleting;
            }
        }

        public static void DisposeAll()
        {
            foreach (var wrapper in wrappers)
            {
                wrapper.Dispose();
            }
        }

        private void Connector_OnInsert(string tableName, object[] objs)
        {
            if (tableName == table.TableName && !this.table.Rows.Contains(objs[0]))
            {
                try
                {
                    table.Rows.Add(objs);
                    this.OnChanged();
                }
                catch 
                {
                    
                }
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
            this.table.RowDeleted += Table_RowDeleted;
        }

        private void Table_RowDeleted(object sender, DataRowChangeEventArgs e)
        {
            this.table.AcceptChanges();
            this.OnChanged();
        }

        private void Table_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            this.connector.DeleteRow(e.Row.ItemArray);
            //System.Threading.Thread.Sleep(300);
            /*DataTable dt = (DataTable)sender;
            this.connector.DeleteRow(e.Row.ItemArray);
            dt.AcceptChanges();*/
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
                    this.OnChanged();
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
