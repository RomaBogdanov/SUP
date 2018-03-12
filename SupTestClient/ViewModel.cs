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
using System.IO;
using System.Windows.Media.Imaging;
using System.Timers;

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
        private ClientConnector connector;
        private string testField = "";
        private object currentItem;
        private BitmapImage picture;
        private string msgs = "";

        private string login;
        private string password;
        bool IsAuthorization = false;
        private string enterButtonContent = "Войти";
        Timer timer;
        int timerInterval = 3000;

        #region Public

        public string EnterButtonContent
        {
            get { return this.enterButtonContent; }
            set
            {
                if (this.enterButtonContent != value)
                {
                    this.enterButtonContent = value;
                    OnPropertyChanged("EnterButtonContent");
                }
            }
        }

        public string Login
        {
            get { return this.login; }
            set
            {
                if (this.login != value)
                {
                    this.login = value;
                    OnPropertyChanged("Login");
                }
            }
        }

        public string Password
        {
            get { return this.password; }
            set
            {
                if (this.password != value)
                {
                    this.password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

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

        public BitmapImage Picture
        {
            get { return this.picture; }
            set
            {
                if (this.picture != value)
                {
                    this.picture = value;
                    OnPropertyChanged("Picture");
                }
            }
        }

        public string Msgs
        {
            get { return this.msgs; }
            set
            {
                if (this.msgs != value)
                {
                    this.msgs = value;
                    OnPropertyChanged("Msgs");
                }
            }
        }

        public ICommand ReceiveTable
        { get; set; }

        public ICommand DeleteRow
        { get; set; }

        public ICommand GetImage
        { get; set; }

        public ICommand Enter
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
            this.connector = ClientConnector.CurrentConnector;
            this.connector.OnInsert += Connector_OnInsert;
            this.connector.OnDelete += Connector_OnDelete;
            this.connector.OnUpdate += Connector_OnUpdate;
            this.DV = this.Table.AsDataView();
            this.DV.ListChanged += DV_ListChanged;
            ReceiveTable = new RelayCommand(arg => GetTable());
            DeleteRow = new RelayCommand(arg => DelRow(arg));
            GetImage = new RelayCommand(arg => GImage());
            Enter = new RelayCommand(arg => Entering());
            timer = new Timer(timerInterval);
            timer.Elapsed += Timer_Elapsed;
        }

        private void Connector_OnInsert(string tableName, object[] objs)
        {
            if (!this.table.Rows.Contains(objs[0]))
            {
                table.Rows.Add(objs);
            }
        }

        private void Connector_OnUpdate(string tableName, int rowNumber, object[] objs)
        {
            DataRow row = table.Rows.Find(objs[0]);
            if (row != null)
            {
                row.ItemArray = objs;
                table.AcceptChanges();
            }
        }

        private void Connector_OnDelete(string tableName, object[] objs)
        {
            DataRow row = table.Rows.Find(objs[0]);
            if (row != null)
            {
                table.Rows.Remove(row);
            }
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
            try
            {
                this.Table = this.connector.GetTable(this.queriesDict[this.QueryName]);
                this.Table.RowDeleting += Table_RowDeleting;
                this.DV = this.Table.AsDataView();
                this.DV.ListChanged += DV_ListChanged;
                this.TestField = this.DV.AllowNew.ToString();
            }
            catch (Exception err)
            {
                this.Msgs = $"{err.Message}: {err.StackTrace}";
                
            }
        }

        private void Table_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            //this.connector.DeleteRow(e.Row.ItemArray);
            //throw new NotImplementedException();
        }

        private void DelRow(object arg)
        {
            DataRowView dv = arg as DataRowView;
            if (dv == null) return;
            this.connector.DeleteRow(dv.Row.ItemArray);
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
                //this.connector.DeleteRow(this.Table.Rows[e.NewIndex].ItemArray);
            }
        }

        private void GImage()
        {
            byte[] b = this.connector.GetImage(3);
            MemoryStream memoryStream = new MemoryStream(b);
            //Picture = memoryStream;
            BitmapImage im = new BitmapImage();
            im.BeginInit();
            im.StreamSource = memoryStream;
            im.EndInit();
            Picture = im;
        }

        private void Entering()
        {
            if (IsAuthorization)
            {
                this.connector.ExitAuthorize();
                IsAuthorization = false;
                this.EnterButtonContent = "Войти";
                timer.Stop();
            }
            else
            {
                if (this.connector.Authorize(Login, Password))
                {
                    IsAuthorization = true;
                    this.EnterButtonContent = "Выйти";
                    timer.Start();
                }
                else
                {
                    IsAuthorization = false;
                    this.EnterButtonContent = "Войти";
                    this.Msgs = "Аутентификация не прошла";
                    timer.Stop();
                }
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            bool a = this.connector.CheckAuthorize();
            if (a)
            {
                IsAuthorization = true;
                this.EnterButtonContent = "Выйти";
            }
            else
            {
                IsAuthorization = false;
                this.EnterButtonContent = "Войти";
                timer.Stop();
            }
        }

        #endregion

    }
}
