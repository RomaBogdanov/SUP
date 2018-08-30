using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using SupClientConnectionLib;

namespace SUPClient
{
    class Order1Model1 : IOrders1Model
    {
        private Dictionary<string, ClientConnector> tabConnectors;
        private DataTable tabVisitors;
        private DataTable tabOrganizations;
        private DataTable tabOrders;
        private DataTable tabOrderElements;
        
        private Orders1ViewModel viewModel;

        public void CreateOrder()
        {
            this.CreateOrderMeth();
        }

        public void DeleteOrder()
        {
            this.DeleteOrderMeth();
        }

        public void EditOrder()
        {
            this.EditOrderMeth();
        }

        public void SaveOrder()
        {
            this.SaveOrderMeth();
        }

        public Order1Model1(Orders1ViewModel viewModel)
        {
            this.viewModel = viewModel;

            this.tabConnectors = new Dictionary<string, ClientConnector>();
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
            //this.tabConnectors[dataTable.TableName].DeleteRow(i);             // Все равно не собирается в 2017 студии!!!
            this.tabConnectors[dataTable.TableName].DeleteRow(new object[] { e.Row });
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
            this.viewModel.Orgs = organizations;

            var peoples = from p1 in this.tabVisitors.AsEnumerable()
                          select new People
                          {
                              Id = p1.Field<int>("f_visitor_id"),
                              FullName = p1.Field<string>("f_full_name")
                          };
            this.viewModel.AllPeoples = peoples;

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
        }

        private void Refresh()
        {
            this.Query();
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
            newOrder.OrderElements["f_catcher_id"] = 0;
            newOrder.OrderElements["f_time_from"] = DateTime.Now;
            newOrder.OrderElements["f_time_to"] = DateTime.Now;
            newOrder.OrderElements["f_rec_date"] = DateTime.Now;
            newOrder.OrderElements["f_rec_operator"] = 0;
            this.tabOrderElements.Rows.Add(newOrder.OrderElements);
            newOrder.PersonSigned = this.tabVisitors.Select("f_visitor_id='0'")[0];
            newOrder.OrganizationSigned = this.tabOrganizations.Select("f_org_id='0'")[0];
            newOrder.PersonAdjusted = this.tabVisitors.Select("f_visitor_id='0'")[0];
            newOrder.OrganizationAdjusted = this.tabOrganizations.Select("f_org_id='0'")[0];
            newOrder.Visitor = this.tabVisitors.Select("f_visitor_id='0'")[0];
            newOrder.VisitorOrganization = this.tabOrganizations.Select("f_org_id='0'")[0];
            this.viewModel.CurrentItem = newOrder;
        }

        private void SaveOrderMeth()
        {
            IEnumerable<FullOrder> unionFullOrd =
                new List<FullOrder> { this.viewModel.CurrentItem };
            this.viewModel.FullOrders.Union(unionFullOrd);
        }


        private void EditOrderMeth()
        {
            this.viewModel.EditingOrder = !this.viewModel.EditingOrder;
        }

        private void DeleteOrderMeth()
        {
            FullOrder fullOrder = this.viewModel.CurrentItem;
            this.tabOrderElements.Rows.Remove(fullOrder.OrderElements);
            this.tabOrders.Rows.Remove(fullOrder.Order);
            this.viewModel.numOrd = "0";
            this.Refresh();
        }
    }
}
