﻿using System.Data;
using SupClientConnectionLib;

namespace SUPClient
{
    class TableWrapper
    {
        private DataTable _table;
        protected IClientConnector connector;

	    public DataTable table
	    {
		    get { return this._table ?? new DataTable(); }
		    protected set { _table = value; }
	    }

        public IClientConnector Connector { get { return this.connector; } }

        protected TableWrapper()
        {
            this.connector = ClientConnectorFactory.CreateClientConnector();
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
            /*DataTable dt;
            switch (e.Action)
            {
                case DataRowAction.Nothing:
                    break;
                case DataRowAction.Delete:
                    break;
                case DataRowAction.Change:
                    dt = (DataTable)sender;
                    int i = dt.Rows.IndexOf(e.Row);
                    this.tabConnectors[dt.TableName].UpdateRow(e.Row.ItemArray, i);
                    break;
                case DataRowAction.Rollback:
                    break;
                case DataRowAction.Commit:
                    break;
                case DataRowAction.Add:
                    dt = (DataTable)sender;
                    this.tabConnectors[dt.TableName].InsertRow(e.Row.ItemArray);
                    break;
                case DataRowAction.ChangeOriginal:
                    break;
                case DataRowAction.ChangeCurrentAndOriginal:
                    break;
                default:
                    break;
            }*/
        }
    }
}
