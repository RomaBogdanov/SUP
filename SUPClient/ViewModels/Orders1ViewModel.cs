﻿#define Test

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;
using System.Windows.Input;
using SupClientConnectionLib;
using SupClientConnectionLib.ServiceRef;

namespace SUPClient
{
    class Orders1ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
        private IEnumerable<Org> orgs;
        private IEnumerable<Peoples> allPeoples;
        private FullOrder currentItem;
        private bool editingOrder = false;

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

        public IEnumerable<Org> Orgs
        {
            get { return this.orgs; }
            set
            {
                this.orgs = value;
                OnPropertyChanged("Orgs");
            }
        }

        public IEnumerable<Peoples> AllPeoples
        {
            get { return this.allPeoples; }
            set
            {
                this.allPeoples = value;
                OnPropertyChanged("AllPeoples");
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

        /// <summary>
        /// Редактировать заявку.
        /// </summary>
        public bool EditingOrder
        {
            get { return this.editingOrder; }
            set
            {
                if (this.editingOrder != value)
                {
                    this.editingOrder = value;
                    OnPropertyChanged("EditingOrder");
                }
            }
        }
        
        public ICommand CreateOrder
        { get; set; }

        public ICommand SaveOrder
        { get; set; }

        public ICommand EditOrder
        { get; set; }

        public Orders1ViewModel()
        {
            this.CreateOrder = new RelayCommand(arg => CreateOrderMeth());
            this.SaveOrder = new RelayCommand(arg => SaveOrderMeth());
            this.EditOrder = new RelayCommand(arg => EditOrderMethod());
            this.SelectedDate = DateTime.Now.ToString();
            this.tabConnectors = new Dictionary<string, ServerConnector>();
            ServerConnector sc = new ServerConnector();
            this.tabVisitors = sc.GetTable(TableName.VisVisitors);
            this.tabConnectors.Add(this.tabVisitors.TableName, sc);
            sc = new ServerConnector();
            this.tabOrganizations = sc.GetTable(TableName.VisOrganizations);
            this.tabConnectors.Add(this.tabOrganizations.TableName, sc);
            sc = new ServerConnector();
            this.tabOrders = sc.GetTable(TableName.VisOrders);
            this.tabConnectors.Add(this.tabOrders.TableName, sc);
            sc = new ServerConnector();
            this.tabOrderElements = sc.GetTable(TableName.VisOrderElements);
            this.tabConnectors.Add(this.tabOrderElements.TableName, sc);
            this.tabVisitors.RowChanged += Table_RowChanged;
            this.tabOrganizations.RowChanged += Table_RowChanged;
            this.tabOrders.RowChanged += Table_RowChanged;
            this.tabOrderElements.RowChanged += Table_RowChanged;
#if !Test
            var visitors = from vis in this.tabVisitors.AsEnumerable()
                           join org in this.tabOrganizations.AsEnumerable()
                           on vis.Field<int>("f_org_id") equals org.Field<int>("f_org_id")
                           select new Visitor
                           {
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
                               DocNumber = vis.Field<string>("f_doc_num"),
                               Phone = vis.Field<string>("f_phones")
                           };
            var ordersExt = from or in this.tabOrders.AsEnumerable()
                            join vis in visitors
                            on or.Field<int>("f_visitor_id") equals vis.VisID
                            select new
                            {
                                Order = or,
                                Person = vis
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
                                 DocNumber = visitor.DocNumber,
                                 Phone = visitor.Phone
                             };
            NewMethod();

        }

        private void NewMethod()
        {
            NewMethod();
        }

        private void NewMethod()
        {
#endif
            Query();
        }

        private void Query()
        {
            var t = from p in this.tabOrganizations.AsEnumerable()
                    select new Org
                    {
                        Id = p.Field<int>("f_org_id"),
                        FullNameOrganization = p.Field<string>("f_full_org_name")
                    };
            this.Orgs = t;

            var t1 = from p1 in this.tabVisitors.AsEnumerable()
                     select new Peoples
                     {
                         Id = p1.Field<int>("f_visitor_id"),
                         FullName = p1.Field<string>("f_full_name")
                     };
            this.AllPeoples = t1;

            var a = from vis in this.tabVisitors.AsEnumerable()
                    join org in this.tabOrganizations.AsEnumerable()
                    on vis.Field<int>("f_org_id") equals org.Field<int>("f_org_id")
                    select new
                    {
                        Person = vis,
                        Organization = org
                    };
            var b = from or in this.tabOrders.AsEnumerable()
                    join per in a
                    on or.Field<int>("f_signed_by") equals per.Person.Field<int>("f_visitor_id")
                    select new
                    {
                        Order = or,
                        PersonSigned = per.Person,
                        OrganizationSigned = per.Organization
                    };
            var c = from or in b
                    join per in a
                    on or.Order.Field<int>("f_adjusted_with") equals per.Person.Field<int>("f_visitor_id")
                    select new
                    {
                        Order = or.Order,
                        PersonSigned = or.PersonSigned,
                        OrganizationSigned = or.OrganizationSigned,
                        PersonAdjusted = per.Person,
                        OrganizationAdjusted = per.Organization
                    };
            var d = from orel in this.tabOrderElements.AsEnumerable()
                    join or in c
                    on orel.Field<int>("f_ord_id") equals or.Order.Field<int>("f_ord_id")
                    select new
                    {
                        Order = or.Order,
                        PersonSigned = or.PersonSigned,
                        OrganizationSigned = or.OrganizationSigned,
                        PersonAdjusted = or.PersonAdjusted,
                        OrganizationAdjusted = or.OrganizationAdjusted,
                        OrderElements = orel
                    };
            var fullOrders = from orel in d
                             join vis in a
                             on orel.OrderElements.Field<int>("f_visitor_id") equals vis.Person.Field<int>("f_visitor_id")
                             select new FullOrder(this.Refresh)
                             {
                                 VisitorsTab = this.tabVisitors,
                                 OrganizationsTab = this.tabOrganizations,
                                 OrdersTab = this.tabOrders,
                                 OrderElementsTab = this.tabOrderElements,
                                 Order = orel.Order,
                                 PersonSigned = orel.PersonSigned,
                                 OrganizationSigned = orel.OrganizationSigned,
                                 PersonAdjusted = orel.PersonAdjusted,
                                 OrganizationAdjusted = orel.OrganizationAdjusted,
                                 OrderElements = orel.OrderElements,
                                 Visitor = vis.Person,
                                 VisitorOrganization = vis.Organization
                             };
            //IEnumerable<Tst> tsten = new List<Tst>() { new Tst() };
            //c = c.Union(tsten);
            //IEnumerable<Tst> tsten2 = new List<Tst>() { c.ElementAt(0) };
            //c = c.Except(tsten2);
            this.DView = fullOrders;
            if (fullOrders.Count() > 0)
            {
                this.CurrentItem = fullOrders.ElementAt(0);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }

        private void Table_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            DataTable dt;
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
            }
        }

        private void CreateOrderMeth()
        {
#if !Test
            this.CurrentItem = new FullOrder();
            DataRow dr1, dr2, dr3, dr4;
            dr2 = this.tabOrganizations.NewRow();
            this.tabOrganizations.Rows.Add(dr2);
            this.CurrentItem.OrganizationInf = dr2;
            dr1 = this.tabVisitors.NewRow();
            dr1["f_org_id"] = dr2["f_org_id"];
            this.tabVisitors.Rows.Add(dr1);
            this.CurrentItem.VisitorInf = dr1;
            dr4 = this.tabOrders.NewRow();
            this.tabOrders.Rows.Add(dr4);
            this.CurrentItem.OrderInf = dr4;
            dr3 = this.tabOrderElements.NewRow();
            dr3["f_ord_id"] = dr4["f_ord_id"];
            dr3["f_visitor_id"] = dr1["f_visitor_id"];
            this.tabOrderElements.Rows.Add(dr3);
            this.CurrentItem.OrderElemInf = dr3;
#endif
        }

        private void SaveOrderMeth()
        {
            IEnumerable<FullOrder> unionFullOrd = 
                new List<FullOrder> { this.CurrentItem };
            this.DView.Union(unionFullOrd);
        }


        private void EditOrderMethod()
        {
            this.EditingOrder = !this.EditingOrder;
        }

        private void Refresh()
        {
            this.Query();
        }

    }

#if !Test

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
        public string Phone { get; set; }
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
        private string organization = "";
        private DateTime fromDate;
        private DateTime toDate;

        public DataRow VisitorInf { get; set; }
        public DataRow OrganizationInf { get; set; }
        public DataRow OrderInf { get; set; }
        public DataRow OrderElemInf { get; set; }

        public string OrderID { get; set; }
        public string FullName { get; set; }
        public string Organization
        {
            get { return this.organization; }
            set
            {
                if (this.organization != value)
                {
                    this.organization = value;
                    // TODO: фрагмент кода исключительно для демонстрационных целей!!!
                    // При первой возможности заменить на нормальный с подключением
                    // списка организаций и изменением в ссылках на организации
                    // в таблице визитёров, а не организаций.
                    if ((string)this.OrganizationInf["f_full_org_name"] != value)
                    {
                        this.OrganizationInf["f_full_org_name"] = value;
                    }
                }
            }
        }
        public DateTime From
        {
            get { return this.fromDate; }
            set
            {
                if (this.fromDate != value)
                {
                    this.fromDate = value;
                    if ((DateTime)this.OrderInf["f_date_from"] != value)
                    {
                        this.OrderInf["f_date_from"] = value;
                    }
                }
            }
        }
        public DateTime To
        {
            get { return this.toDate; }
            set
            {
                if (this.toDate!=value)
                {
                    this.toDate = value;
                    if ((DateTime)this.OrderInf["f_date_to"] != value)
                    {
                        this.OrderInf["f_date_to"] = value;
                    }
                }
            }
        }
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
                    if (this.VisitorInf["f_family"] is DBNull || 
                        (string)this.VisitorInf["f_family"] != value)
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
                    if (this.VisitorInf["f_fst_name"] is DBNull || 
                        (string)this.VisitorInf["f_fst_name"] != value)
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
        public string Phone { get; set; }
        public string DocSeria { get; set; }
        public string DocNumber { get; set; }
        public string Job { get; set; }
    }
#endif

    public class Org
    {
        public int Id { get; set; }
        public string FullNameOrganization { get; set; }
    }

    public class Peoples
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
