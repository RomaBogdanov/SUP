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

        #region Values

        public event PropertyChangedEventHandler PropertyChanged;

        private Dictionary<string, ClientConnector> tabConnectors;

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
        private IEnumerable<People> allPeoples;
        private FullOrder currentItem;
        private bool editingOrder = false;

        private string numOrd = "0";

        #endregion

        #region Properties

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

        public IEnumerable<People> AllPeoples
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
                    this.numOrd = currentItem.OrderID;
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
        
        /// <summary>
        /// Обработчик команды добавки новой заявки.
        /// </summary>
        public ICommand CreateOrder
        { get; set; }

        public ICommand SaveOrder
        { get; set; }

        public ICommand EditOrder
        { get; set; }

        public ICommand DeleteOrder
        { get; set; }

        #endregion

        public Orders1ViewModel()
        {
            this.CreateCommands();
            this.StartConditions();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }

        #region Private

        private void CreateCommands()
        {
            this.CreateOrder = new RelayCommand(arg => CreateOrderMeth());
            this.SaveOrder = new RelayCommand(arg => SaveOrderMeth());
            this.EditOrder = new RelayCommand(arg => EditOrderMethod());
            this.DeleteOrder = new RelayCommand(arg => DeleteOrderMethod());
        }

        private void StartConditions()
        {
            this.SelectedDate = DateTime.Now.ToString();
            this.tabConnectors = new Dictionary<string, ClientConnector>();
            ClientConnector sc = new ClientConnector();
            this.tabVisitors = sc.GetTable(TableName.VisVisitors);
            this.tabConnectors.Add(this.tabVisitors.TableName, sc);
            sc = new ClientConnector();
            this.tabOrganizations = sc.GetTable(TableName.VisOrganizations);
            this.tabConnectors.Add(this.tabOrganizations.TableName, sc);
            sc = new ClientConnector();
            this.tabOrders = sc.GetTable(TableName.VisOrders);
            this.tabConnectors.Add(this.tabOrders.TableName, sc);
            sc = new ClientConnector();
            this.tabOrderElements = sc.GetTable(TableName.VisOrderElements);
            this.tabConnectors.Add(this.tabOrderElements.TableName, sc);
            this.tabVisitors.RowChanged += Table_RowChanged;
            this.tabOrganizations.RowChanged += Table_RowChanged;
            this.tabOrders.RowChanged += Table_RowChanged;
            this.tabOrders.RowDeleting += Table_RowDeleting;
            this.tabOrderElements.RowChanged += Table_RowChanged;
            this.Query();
        }

        /// <summary>
        /// Формирует сложный запрос к таблицам DataTable.
        /// </summary>
        private void Query()
        {
            var organizations = from p in this.tabOrganizations.AsEnumerable()
                    select new Org
                    {
                        Id = p.Field<int>("f_org_id"),
                        FullNameOrganization = p.Field<string>("f_full_org_name")
                    };
            this.Orgs = organizations;

            var peoples = from p1 in this.tabVisitors.AsEnumerable()
                     select new People
                     {
                         Id = p1.Field<int>("f_visitor_id"),
                         FullName = p1.Field<string>("f_full_name")
                     };
            this.AllPeoples = peoples;

            var personAllInfo = from vis in this.tabVisitors.AsEnumerable()
                    join org in this.tabOrganizations.AsEnumerable()
                    on vis.Field<int>("f_org_id") equals org.Field<int>("f_org_id")
                    select new
                    {
                        Person = vis,
                        Organization = org
                    };
            var addPersonSigned = from or in this.tabOrders.AsEnumerable()
                    join per in personAllInfo
                    on or.Field<int>("f_signed_by") equals per.Person.Field<int>("f_visitor_id")
                    select new
                    {
                        Order = or,
                        PersonSigned = per.Person,
                        OrganizationSigned = per.Organization
                    };
            var addPersonAdjusted = from or in addPersonSigned
                    join per in personAllInfo
                    on or.Order.Field<int>("f_adjusted_with") equals per.Person.Field<int>("f_visitor_id")
                    select new
                    {
                        or.Order,
                        or.PersonSigned,
                        or.OrganizationSigned,
                        PersonAdjusted = per.Person,
                        OrganizationAdjusted = per.Organization
                    };
            var addOrderElements = from orel in this.tabOrderElements.AsEnumerable()
                    join or in addPersonAdjusted
                    on orel.Field<int>("f_ord_id") equals or.Order.Field<int>("f_ord_id")
                    select new
                    {
                        or.Order,
                        or.PersonSigned,
                        or.OrganizationSigned,
                        or.PersonAdjusted,
                        or.OrganizationAdjusted,
                        OrderElements = orel
                    };
            var fullOrders = from orel in addOrderElements
                             join vis in personAllInfo
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
            this.DView = fullOrders;
            this.CurrentItem = fullOrders.First(p => p.OrderID == numOrd);
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

        private void Table_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            DataTable dataTable = (DataTable)sender;
            int i = dataTable.Rows.IndexOf(e.Row);
            this.tabConnectors[dataTable.TableName].DeleteRow(i);
        }

        private void CreateOrderMeth()
        {
            FullOrder newOrder = new FullOrder(this.Refresh)
            {
                VisitorsTab = this.tabVisitors,
                OrganizationsTab = this.tabOrganizations,
                OrdersTab = this.tabOrders,
                OrderElementsTab = this.tabOrderElements
            };
            newOrder.Order = this.tabOrders.NewRow();
            DataRow dataRow = this.tabOrders.Select("f_ord_id=0")[0];
            object[] objs = dataRow.ItemArray;
            for (int i = 1; i < objs.Length; i++)
            {
                newOrder.Order[i] = dataRow[i];
            }
            this.tabOrders.Rows.Add(newOrder.Order);
            newOrder.OrderElements = this.tabOrderElements.NewRow();
            newOrder.OrderElements["f_ord_id"] = newOrder.Order["f_ord_id"];
            newOrder.OrderElements["f_visitor_id"] = 0;
            newOrder.OrderElements["f_passes"] = "";
            this.tabOrderElements.Rows.Add(newOrder.OrderElements);
            newOrder.PersonSigned = this.tabVisitors.Select("f_visitor_id='0'")[0];
            newOrder.OrganizationSigned = this.tabOrganizations.Select("f_org_id='0'")[0];
            newOrder.PersonAdjusted = this.tabVisitors.Select("f_visitor_id='0'")[0];
            newOrder.OrganizationAdjusted = this.tabOrganizations.Select("f_org_id='0'")[0];
            newOrder.Visitor = this.tabVisitors.Select("f_visitor_id='0'")[0];
            newOrder.VisitorOrganization = this.tabOrganizations.Select("f_org_id='0'")[0];
            this.CurrentItem = newOrder;
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

        private void DeleteOrderMethod()
        {
            FullOrder fullOrder = this.CurrentItem;
            this.tabOrderElements.Rows.Remove(fullOrder.OrderElements);
            this.tabOrders.Rows.Remove(fullOrder.Order);
            this.numOrd = "0";
            this.Refresh();
        }

        private void Refresh()
        {
            this.Query();
        }

        #endregion

    }
}
