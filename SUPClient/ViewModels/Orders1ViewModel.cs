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
        private Dictionary<string, ServerConnector> tabConnectors;

        private string selectedDate;
        private int countVisitors;
        private int countCars;
        private DataTable tabVisitors = new DataTable();
        private DataTable tabOrganizations = new DataTable();
        private DataTable tabOrders = new DataTable();
        private DataTable tabOrderElements = new DataTable();
        private DataTable tabComplex = new DataTable();
        private IEnumerable<FullOrder> dView;
        private FullOrder currentItem;
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

        public IEnumerable<FullOrder> DView
        {
            get { return this.dView; }
            set
            {
                this.dView = value;
                OnPropertyChanged("DView");
            }
        }

        public FullOrder CurrentItem
        {
            get { return this.currentItem; }
            set
            {
                if (this.currentItem != value & value != null)
                {
                    this.currentItem = value;
                    OnPropertyChanged("CurrentItem");
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
            this.tabConnectors = new Dictionary<string, ServerConnector>();
            ServerConnector a = new ServerConnector();
            this.tabVisitors = a.GetTable(TableName.VisVisitors);
            this.tabConnectors.Add(this.tabVisitors.TableName, a);
            a = new ServerConnector();
            this.tabOrganizations = a.GetTable(TableName.VisOrganizations);
            this.tabConnectors.Add(this.tabOrganizations.TableName, new ServerConnector());
            a = new ServerConnector();
            this.tabOrders = a.GetTable(TableName.VisOrders);
            this.tabConnectors.Add(this.tabOrders.TableName, new ServerConnector());
            a = new ServerConnector();
            this.tabOrderElements = a.GetTable(TableName.VisOrderElements);
            this.tabConnectors.Add(this.tabOrderElements.TableName, new ServerConnector());
            //this.connector = ServerConnector.CurrentConnector;
            /*this.tabVisitors = this.tabConnectors[tabVisitors]
                .GetTable(TableName.VisVisitors);*/
            this.tabVisitors.ColumnChanged += TabVisitors_ColumnChanged;
            this.tabVisitors.RowChanged += TabVisitors_RowChanged;
            /*this.tabOrganizations = this.tabConnectors[this.tabOrganizations]
                .GetTable(TableName.VisOrganizations);
            this.tabOrders = this.tabConnectors[this.tabOrders]
                .GetTable(TableName.VisOrders);
            this.tabOrderElements = this.tabConnectors[this.tabOrderElements]
                .GetTable(TableName.VisOrderElements);*/
            var visitors = from vis in this.tabVisitors.AsEnumerable()
                    join org in this.tabOrganizations.AsEnumerable()
                    on vis.Field<int>("f_org_id") equals org.Field<int>("f_org_id")
                    select new Visitor {
                        VisitorInf = vis,
                        OrganizationInf = org,
                        VisID = vis.Field<int>("f_visitor_id"),
                        FullName = vis.Field<string>("f_full_name"),
                        Organization = org.Field<string>("f_full_org_name"),
                        Family = vis.Field<string>("f_family"),
                        FirstName = vis.Field<string>("f_fst_name"),
                        LastName = vis.Field<string>("f_sec_name"),
                        Job = vis.Field<string>("f_job"),
                        DocSeria = vis.Field<string>("f_doc_seria"),
                        DocNumber = vis.Field<string>("f_doc_num")
                    };
            var orders = from orel in this.tabOrderElements.AsEnumerable()
                    join or in this.tabOrders.AsEnumerable()
                    on orel.Field<int>("f_ord_id") equals or.Field<int>("f_ord_id")
                    select new Order
                    {
                        OrderInf = or,
                        OrderElemInf = orel,
                        OrderID = orel.Field<int>("f_ord_id"),
                        VisID = orel.Field<int>("f_visitor_id"),
                        From = or.Field<DateTime>("f_date_from"),
                        To = or.Field<DateTime>("f_date_to"),
                        Status = or.Field<string>("f_notes"),
                        Signed = or.Field<int>("f_signed_by"),
                        Adjusted = or.Field<int>("f_adjusted_with"),
                        OrderDate = or.Field<DateTime>("f_ord_date")
                    };
             var fullOrders = from visitor in visitors
                    join order in orders
                    on visitor.VisID equals order.VisID
                    select new FullOrder
                    {
                        VisitorInf = visitor.VisitorInf,
                        OrganizationInf = visitor.OrganizationInf,
                        OrderInf = order.OrderInf,
                        OrderElemInf = order.OrderElemInf,
                        OrderID = order.OrderID.ToString(),
                        FullName = visitor.FullName,
                        Organization = visitor.Organization,
                        From = order.From,
                        To = order.To,
                        Status = order.Status,
                        Signed = order.Signed,
                        Adjusted = order.Adjusted,
                        OrderDate = order.OrderDate,
                        Family = visitor.Family,
                        FirstName = visitor.FirstName,
                        LastName = visitor.LastName,
                        Job = visitor.Job,
                        DocSeria = visitor.DocSeria,
                        DocNumber = visitor.DocNumber
                    };
            //IEnumerable<Tst> tsten = new List<Tst>() { new Tst() };
            //c = c.Union(tsten);
            //IEnumerable<Tst> tsten2 = new List<Tst>() { c.ElementAt(0) };
            //c = c.Except(tsten2);
            this.DView = fullOrders;
        }

        private void TabVisitors_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Change)
            {
                DataTable dt = (DataTable)sender;
                int i = dt.Rows.IndexOf(e.Row);
                this.tabConnectors[dt.TableName].UpdateRow(e.Row.ItemArray, i);
                //this.connector.UpdateRow(e.Row.ItemArray, i);
            }
        }

        private void TabVisitors_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, 
                new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Visitor
    {
        public DataRow VisitorInf { get; set; }
        public DataRow OrganizationInf { get; set; }

        public int VisID { get; set; }
        public string FullName { get; set; }
        public string Organization { get; set; }
        public string Family { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Job { get; set; }
        public string DocSeria { get; set; }
        public string DocNumber { get; set; }
    }

    public class Order
    {
        public DataRow OrderInf { get; set; }
        public DataRow OrderElemInf { get; set; }
        
        public int OrderID { get; set; }
        public int VisID { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Status { get; set; }
        public int Signed { get; set; }
        public int Adjusted { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class FullOrder
    {
        private string family = "";
        private string firstName = "";
        private string lastName = "";

        public DataRow VisitorInf { get; set; }
        public DataRow OrganizationInf { get; set; }
        public DataRow OrderInf { get; set; }
        public DataRow OrderElemInf { get; set; }

        public string OrderID { get; set; }
        public string FullName { get; set; }
        public string Organization { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Status { get; set; }
        public int Signed { get; set; }
        public int Adjusted { get; set; }
        public DateTime OrderDate { get; set; }
        public string Family
        {
            get { return this.family; }
            set
            {
                if (this.family != value)
                {
                    this.family = value;
                    if ((string)this.VisitorInf["f_family"] != value)
                    {
                        this.VisitorInf["f_family"] = value;
                    }
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
                    if ((string)this.VisitorInf["f_fst_name"] != value)
                    {
                        this.VisitorInf["f_fst_name"] = value;
                    }
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
                    if ((string)this.VisitorInf["f_sec_name"] != value)
                    {
                        this.VisitorInf["f_sec_name"] = value;
                    }
                }
            }
        }
        public string DocSeria { get; set; }
        public string DocNumber { get; set; }
        public string Job { get; set; }
    }

}
