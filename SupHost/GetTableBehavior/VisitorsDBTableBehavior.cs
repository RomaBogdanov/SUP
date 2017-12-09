﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace SupHost
{
    /// <summary>
    /// Класс с обобщённым кодом для таблиц получаемых из базы данных Visitors.
    /// </summary>
    abstract class VisitorsDBTableBehavior : ITableBehavior
    {
        protected Connector connector = Connector.CurrentConnector;
        protected DataTable table = null;
        protected DbDataAdapter adapter = null;

        protected string query = "";
        protected string[] primaryKeyColumns;
        protected bool autoPrimaryKey = false;
        protected string tableName = "";

        public DataTable GetTable()
        {
            ConnectionToDataBaseSetup setup = this.connector.GetDataTable(this.query);
            this.table = setup.Table;
            DataColumn[] dcs = new DataColumn[primaryKeyColumns.Length];
            for (int i = 0; i < dcs.Length; i++)
            {
                dcs[i] = this.table.Columns[this.primaryKeyColumns[i]];
                if (this.autoPrimaryKey)
                {
                    this.table.Columns[this.primaryKeyColumns[i]].AutoIncrement = true;
                }
            }
            this.table.PrimaryKey = dcs;
            this.adapter = setup.DataAdapter;
            this.table.TableName = this.tableName;
            return this.table;
        }
        
        public void InsertRow()
        {
            this.connector.UpdateTable(this.table, this.adapter);
        }

        public void UpdateRow()
        {
            this.connector.UpdateTable(this.table, this.adapter);
        }

        public void DeleteRow()
        {
            this.connector.UpdateTable(this.table, this.adapter);
        }

    }
}
