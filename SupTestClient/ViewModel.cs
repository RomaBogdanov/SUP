using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Windows.Input;
using SupTestClient.ClientServiceReference;

namespace SupTestClient
{
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private List<string> queriesList = new List<string>();
        private string queryName = "";
        private DataTable table = new DataTable();
        private DataView dv;
        private ClientConnector connector;
        private string testField = "";

        #region Public

        public List<string> QueriesList
        {
            get
            {
                
                this.queriesList.Add("VisOrders");
                this.queriesList.Add("TestTable1");
                this.queriesList.Add("TestTable2Ado");
                return queriesList;
            }
            set
            {
                this.queriesList = value;
            }
        }

        public string QueryName
        {
            get { return this.queryName; }
            set
            {
                if (this.queryName != value)
                {
                    this.queryName = value;
                    OnPropertyChanged("QueryName");
                }
            }
        }

        public DataTable Table
        {
            get
            {
                return table;
            }
            set
            {
                if (this.table !=value)
                {
                    this.table = value;
                    OnPropertyChanged("Table");
                }
            }
        }

        public DataView DV
        {
            get { return this.dv; }
            set
            {
                    this.dv = value;
                    OnPropertyChanged("DV");
             
            }
        }

        public string TestField
        {
            get { return this.testField; }
            set
            {
                if (this.testField != value)
                {
                    this.testField = value;
                    OnPropertyChanged("TestField");
                }
            }
        }

        public ICommand ReceiveTable
        { get; set; }

        public ViewModel()
        {
            this.connector = ClientConnector.CurrentConnector;
            this.DV = this.Table.AsDataView();
            this.DV.ListChanged += DV_ListChanged;
            ReceiveTable = new RelayCommand(arg => GetTable());
        }

        #endregion

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            { PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
        }

        #region Private

        private void GetTable()
        {
            if (this.QueryName == "VisOrders")
            {
                this.Table = this.connector.GetTable(TableName.VisOrders);
            }
            else if (this.QueryName == "TestTable1")
            {
                this.Table = this.connector.GetTable(TableName.TestTable1);
            }
            else if (this.QueryName == "TestTable2Ado")
            {
                this.Table = this.connector.GetTable(TableName.TestTable2Ado);
            }
            this.DV = this.Table.AsDataView();
            this.DV.ListChanged += DV_ListChanged;
            this.TestField = this.DV.AllowNew.ToString();
            
        }

        private void DV_ListChanged(object sender, ListChangedEventArgs e)
        {
           if (e.ListChangedType == ListChangedType.ItemAdded)
           {
                object[] cols = this.Table.Rows[e.NewIndex].ItemArray;
                this.connector.InsertRow(cols);
           }
        }
        

        #endregion

    }
}
