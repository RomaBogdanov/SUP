using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using SupClientConnectionLib;
using System.IO;
using System.Windows.Media.Imaging;

namespace SUPClient
{
    class Visitors1Model1 : IVisitors1Model
    {
        private Dictionary<string, IClientConnector> tabConnectors;
        private DataTable tabVisitors;
        private DataTable tabOrganizations;
        private DataTable tabOrders;
        private DataTable tabOrderElements;

        private Visitors1ViewModel viewModel;

        public Visitors1Model1(Visitors1ViewModel viewModel)
        {
            this.viewModel = viewModel;

            this.tabConnectors = new Dictionary<string, IClientConnector>();
            VisitorsWrapper visitors = VisitorsWrapper.CurrentTable();
            this.tabVisitors = visitors.table;
            this.tabConnectors.Add(visitors.table.TableName, visitors.Connector);
            OrganizationsWrapper organizationsWrapper = OrganizationsWrapper.CurrentTable();
            this.tabOrganizations = organizationsWrapper.table;
            this.tabConnectors.Add(tabOrganizations.TableName, organizationsWrapper.Connector);
            OrdersWrapper ordersWrapper = OrdersWrapper.CurrentTable();
            this.tabOrders = ordersWrapper.table;
            this.tabConnectors.Add(tabOrders.TableName, ordersWrapper.Connector);
            OrderElementsWrapper orderElementsWrapper = OrderElementsWrapper.CurrentTable();
            this.tabOrderElements = orderElementsWrapper.table;
            this.tabConnectors.Add(tabOrderElements.TableName, orderElementsWrapper.Connector);
            this.tabVisitors.RowChanged += Table_RowChanged;
            this.tabOrganizations.RowChanged += Table_RowChanged;
            this.tabOrders.RowChanged += Table_RowChanged;
            this.tabOrders.RowDeleting += Table_RowDeleting;
            this.tabOrderElements.RowChanged += Table_RowChanged;
            this.Query();
        }

        /// <summary>
        /// Получает фотографию визитёра соответствующего заявке.
        /// </summary>
        /// <param name="fullOrder"></param>
        public void GetImage(FullOrder fullOrder)
        {
            IClientConnector connector = ClientConnectorFactory.CurrentConnector;
            byte[] b = new byte[0];// connector.GetImage((int)fullOrder.Visitor["f_visitor_id"]);
            MemoryStream memoryStream = new MemoryStream(b);
            BitmapImage im = new BitmapImage();
            im.BeginInit();
            im.StreamSource = memoryStream;
            im.EndInit();
            this.viewModel.Picture = im;
        }

        private void Table_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            Refresh();
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

        private void Table_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            /*
            DataTable dataTable = (DataTable)sender;
            int i = dataTable.Rows.IndexOf(e.Row);
            this.tabConnectors[dataTable.TableName].DeleteRow(i);
            */
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
            //this.viewModel.Orgs = organizations;

            var peoples = from p1 in this.tabVisitors.AsEnumerable()
                          select new People
                          {
                              Id = p1.Field<int>("f_visitor_id"),
                              FullName = p1.Field<string>("f_full_name")
                          };
            //this.viewModel.AllPeoples = peoples;

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
            this.viewModel.FullOrders = fullOrders;
            try
            {
                this.viewModel.CurrentItem = fullOrders.First(p => p.OrderID == this.viewModel.numOrd);
            }
            catch (Exception)
            {
                this.viewModel.CurrentItem = fullOrders.First();
            }
            this.GetImage(this.viewModel.CurrentItem);
        }

        private void Refresh()
        {
            this.Query();
        }
    }
}
