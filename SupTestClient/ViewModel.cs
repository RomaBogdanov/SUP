using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Windows.Input;
using SupClientConnectionLib;
using SupClientConnectionLib.ServiceRef;

namespace SupTestClient
{
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private List<string> queriesList = new List<string>();
        private Dictionary<string, TableName> queriesDict = 
            new Dictionary<string, TableName>();
        private string queryName = "";
        private DataTable table = new DataTable();
        private DataView dv;
        private ServerConnector connector;
        private string testField = "";
        private object currentItem;

        #region Public

        public object CurrentItem
        {
            get
            { return this.currentItem; }
            set
            {
                if (this.currentItem != value & value != null)
                {
                    this.currentItem = value;
                    OnPropertyChanged("CurrentItem");
                }
            }
        }

        public List<string> QueriesList
        {
            get
            {    
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

        public ICommand DeleteRow
        { get; set; }

        public ViewModel()
        {
            for (int i = 0; i < 100; i++)
            {
                TableName t = (TableName)i;
                if (!(t.ToString() == "" | t.ToString() == i.ToString()))
                {
                    this.queriesList.Add(t.ToString());
                    this.queriesDict.Add(t.ToString(), t);
                }
            }
            this.connector = ServerConnector.CurrentConnector;
            this.DV = this.Table.AsDataView();
            this.DV.ListChanged += DV_ListChanged;
            ReceiveTable = new RelayCommand(arg => GetTable());
            DeleteRow = new RelayCommand(arg => DelRow(arg));
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
            this.Table = this.connector.GetTable(this.queriesDict[this.QueryName]);
            this.DV = this.Table.AsDataView();
            this.DV.ListChanged += DV_ListChanged;
            this.TestField = this.DV.AllowNew.ToString();    
        }

        private void DelRow(object arg)
        {
            DataRowView dv = arg as DataRowView;
            if (dv == null) return;
            dv.Delete();
        }

        private void DV_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded && 
                this.Table.Rows.Count - 1 == e.NewIndex)
            {
                object[] cols = this.Table.Rows[e.NewIndex].ItemArray;
                this.connector.InsertRow(cols);
            }
            else if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                object[] cols = this.Table.Rows[e.OldIndex].ItemArray;
                this.connector.UpdateRow(cols, e.OldIndex);
            }
            else if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                this.connector.DeleteRow(e.NewIndex);
            }
        }
        
        #endregion

    }
}
