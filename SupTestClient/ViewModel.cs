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
        private ClientConnector connector;

        #region Public

        public List<string> QueriesList
        {
            get
            {
                
                this.queriesList.Add("Hi");
                this.queriesList.Add("Hello");
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

        public ICommand ReceiveTable
        { get; set; }

        public ViewModel()
        {
            this.connector = ClientConnector.CurrentConnector;
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
            this.Table = this.connector.GetTable(TableName.VisOrders);
        }

        #endregion

    }
}
