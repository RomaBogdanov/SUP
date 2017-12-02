using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;

using SupClientConnectionLib;
using SupClientConnectionLib.ServiceRef;

namespace SUPClient
{
    class Orders1ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ServerConnector connector;

        private string selectedDate;
        private int countVisitors;
        private int countCars;
        private DataTable tabVisitors = new DataTable();
        private DataTable tabOrganizations = new DataTable();
        private DataTable tabOrders = new DataTable();
        private DataTable tabOrderElements = new DataTable();
        private DataTable tabComplex = new DataTable();
        private IEnumerable<Tst> dView;
        private Tst currentItem;
        private string lastName;
        private string firstName;
        private string patronName;

        public string SelectedDate
        {
            get { return this.selectedDate; }
            set
            {
                if (this.selectedDate != value && value != null)
                {
                    this.selectedDate = value;
                    OnPropertyChanged("SelectedDate");
                }
            }
        }

        public int CountVisitors
        {
            get { return this.countVisitors; }
            set
            {
                if (this.countVisitors != value)
                {
                    this.countVisitors = value;
                    OnPropertyChanged("CountVisitors");
                }
            }
        }

        public int CountCars
        {
            get { return this.countCars; }
            set
            {
                if (this.countCars != value)
                {
                    this.countCars = value;
                    OnPropertyChanged("CountCars");
                }
            }
        }

        public IEnumerable<Tst> DView
        {
            get { return this.dView; }
            set
            {
                this.dView = value;
                OnPropertyChanged("DView");
            }
        }

        public Tst CurrentItem
        {
            get { return this.currentItem; }
            set
            {
                if (this.currentItem != value & value != null)
                {
                    this.currentItem = value;
                    OnPropertyChanged("CurrentItem");
                    /*if (this.currentItem is DataRowView)
                    {
                        DataRow dr = ((DataRowView)this.currentItem).Row;
                        this.LastName = dr["f_family"].ToString();
                        this.FirstName = dr["f_fst_name"].ToString();
                        this.PatronName = dr["f_sec_name"].ToString();
                    }*/
                }
            }
        }

        public string LastName
        {
            get { return this.lastName; }
            set
            {
                if (this.lastName != value)
                {
                    this.lastName = value;
                    OnPropertyChanged("LastName");
                }
            }
        }

        public string FirstName
        {
            get { return this.firstName; }
            set
            {
                if (this.firstName != value)
                {
                    this.firstName = value;
                    OnPropertyChanged("FirstName");
                }
            }
        }

        public string PatronName
        {
            get { return this.patronName; }
            set
            {
                if (this.patronName != value)
                {
                    this.patronName = value;
                    OnPropertyChanged("PatronName");
                }
            }
        }

        public Orders1ViewModel()
        {
            this.SelectedDate = DateTime.Now.ToString();
            this.connector = ServerConnector.CurrentConnector;
            this.tabVisitors = this.connector.GetTable(TableName.VisVisitors);
            this.tabOrganizations = this.connector.GetTable(TableName.VisOrganizations);
            this.tabOrders = this.connector.GetTable(TableName.VisOrders);
            this.tabOrderElements = this.connector.GetTable(TableName.VisOrderElements);
            var a = from vis in this.tabVisitors.AsEnumerable()
                    join org in this.tabOrganizations.AsEnumerable()
                    on vis.Field<int>("f_org_id") equals org.Field<int>("f_org_id")
                    select new { VisID = vis.Field<int>("f_visitor_id"),
                        FullName = vis.Field<string>("f_full_name"),
                        Organization = org.Field<string>("f_full_org_name"),
                        Family = vis.Field<string>("f_family"),
                        FirstName = vis.Field<string>("f_fst_name"),
                        LastName = vis.Field<string>("f_sec_name"),
                        Job = vis.Field<string>("f_job"),
                        DocSeria = vis.Field<string>("f_doc_seria"),
                        DocNumber = vis.Field<string>("f_doc_num")
                    };
            var b = from orel in this.tabOrderElements.AsEnumerable()
                    join or in this.tabOrders.AsEnumerable()
                    on orel.Field<int>("f_ord_id") equals or.Field<int>("f_ord_id")
                    select new
                    {
                        OrderID = orel.Field<int>("f_ord_id"),
                        VisID = orel.Field<int>("f_visitor_id"),
                        From = or.Field<DateTime>("f_date_from"),
                        To = or.Field<DateTime>("f_date_to"),
                        Status = or.Field<string>("f_notes"),
                        Signed = or.Field<int>("f_signed_by"),
                        Adjusted = or.Field<int>("f_adjusted_with"),
                        OrderDate = or.Field<DateTime>("f_ord_date")
                    };
            var c = from aa in a
                    join bb in b
                    on aa.VisID equals bb.VisID
                    select new Tst
                    {
                        OrderID = bb.OrderID.ToString(),
                        FullName = aa.FullName,
                        Organization = aa.Organization,
                        From = bb.From,
                        To = bb.To,
                        Status = bb.Status,
                        Signed = bb.Signed,
                        Adjusted = bb.Adjusted,
                        OrderDate = bb.OrderDate,
                        Family = aa.Family,
                        FirstName = aa.FirstName,
                        LastName = aa.LastName,
                        Job = aa.Job,
                        DocSeria = aa.DocSeria,
                        DocNumber = aa.DocNumber
                    };
            this.DView = c;
            //this.DView = this.tabVisitors.AsDataView();
        }

        public class Tst
        {
            public string OrderID { get; set; }
            public string FullName { get; set; }
            public string Organization { get; set; }
            public DateTime From { get; set; }
            public DateTime To { get; set; }
            public string Status { get; set; }
            public int Signed { get; set; }
            public int Adjusted { get; set; }
            public DateTime OrderDate { get; set; }
            public string Family { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string DocSeria { get; set; }
            public string DocNumber { get; set; }
            public string Job { get; set; }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, 
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
