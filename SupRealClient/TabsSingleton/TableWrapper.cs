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

        public virtual void Dispose()
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

        public static void Reset()
        {
            foreach (var wrapper in wrappers)
            {
                wrapper.Dispose();
            }
            wrappers.Clear();
        }

        /// <summary>
        /// Добавляет строку в таблицу, при этом дописывает
        /// обязательные для заполнения поля. Желательно использовать
        /// данную конструкцию, а не стандарт
        /// OrdersWrapper.CurrentTable().Table.Rows.Add(row);
        /// </summary>
        /// <param name="row"></param>
        public virtual void AddRow(DataRow row)
        {
            if (table != null)
            {
                row["f_deleted"] = 'N';
                row["f_rec_operator"] = Authorizer.AppAuthorizer.Id;
                table.Rows.Add(row);
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
                    this.OnChanged?.Invoke();
                    this.Connector.UpdateRow(e.Row.ItemArray, i);
                    break;
                case DataRowAction.Rollback:
                    break;
                case DataRowAction.Commit:
                    break;
                case DataRowAction.Add:
                    dt = (DataTable)sender;
                    // добавляем правильные значения, если нулевое значение.
                    /*if (e.Row != null)
                    {
                        int l = e.Row.ItemArray.Length;
                        for (int p = 0; p < l; p++)
                        {
                            if (e.Row.Table.Columns[p].DataType == typeof(int)
                                && e.Row[p].GetType() != typeof(int))
                            {
                                e.Row.ItemArray[p] = 0;
                            }
                            if (e.Row.Table.Columns[p].DataType == typeof(string)
                                && e.Row[p].GetType() != typeof(string))
                            {
                                e.Row.ItemArray[p] = "";
                            }
                            if (e.Row.Table.Columns[p].DataType == typeof(DateTime)
                                && e.Row[p].GetType() != typeof(DateTime))
                            {
                                e.Row.ItemArray[p] = DateTime.MinValue;
                                
                            }
                        }
                    }*/
                    this.OnChanged?.Invoke();
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
