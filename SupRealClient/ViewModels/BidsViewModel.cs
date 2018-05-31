using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Data;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;
using SupRealClient.Common;

namespace SupRealClient.ViewModels
{
    public class BidsViewModel : ViewModelBase
    {
        IBidsModel bidsModel;

        public IBidsModel BidsModel
        {
            get { return bidsModel; }
            set
            {
                bidsModel = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Order> SingleOrdersSet
        {
            get { return BidsModel?.SingleOrdersSet; }
            set
            {
                if (BidsModel != null)
                {
                    BidsModel.SingleOrdersSet = value;
                    OnPropertyChanged();
                }
            }
        }

        public Order CurrentSingleOrder
        {
            get { return BidsModel?.CurrentSingleOrder; }
            set
            {   //if (BidsModel != null & BidsModel.CurrentSingleOrder.From == null)
                { BidsModel.CurrentSingleOrder.From = DateTime.Now; }
                    if (BidsModel != null)
                {   
                    BidsModel.CurrentSingleOrder = value;                    
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Order> TemporaryOrdersSet
        {
            get { return BidsModel?.TemporaryOrdersSet; }
            set
            {
                if (BidsModel != null)
                {
                    BidsModel.TemporaryOrdersSet = value;
                    OnPropertyChanged();
                }
            }
        }

        public Order CurrentTemporaryOrder
        {
            get { return BidsModel?.CurrentTemporaryOrder; }
            set
            {
                if (BidsModel != null)
                {
                    BidsModel.CurrentTemporaryOrder = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Order> VirtueOrdersSet
        {
            get { return BidsModel?.VirtueOrdersSet; }
            set
            {
                if (BidsModel != null)
                {
                    BidsModel.VirtueOrdersSet = value;
                    OnPropertyChanged();
                }
            }
        }

        public Order CurrentVirtueOrder
        {
            get { return BidsModel?.CurrentVirtueOrder; }
            set
            {
                if (BidsModel != null)
                {
                    BidsModel.CurrentVirtueOrder = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Order> OrdersSet
        {
            get { return BidsModel?.OrdersSet; }
            set
            {
                if (BidsModel != null)
                {
                    BidsModel.OrdersSet = value;
                    OnPropertyChanged();
                }
            }
        }

        public Order CurrentOrder
        {
            get { return BidsModel?.CurrentOrder; }
            set
            {
                if (BidsModel != null)
                {
                    BidsModel.CurrentOrder = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand BeginCommand { get; set; }
        public ICommand PrevCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand EndCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand DelayCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand FurtherCommand { get; set; }
        public ICommand ReloadCommand { get; set; }

        public BidsViewModel()
        {
            BeginCommand = new RelayCommand(arg => Begin());
            PrevCommand = new RelayCommand(arg => Prev());
            NextCommand = new RelayCommand(arg => Next());
            EndCommand = new RelayCommand(arg => End());
            SearchCommand = new RelayCommand(arg => Search());
            DelayCommand = new RelayCommand(arg => Delay());
            NewCommand = new RelayCommand(arg => New());
            EditCommand = new RelayCommand(arg => Edit());
            OkCommand = new RelayCommand(arg => Ok());
            CancelCommand = new RelayCommand(arg => Cancel());
            FurtherCommand = new RelayCommand(arg => Further());
            ReloadCommand = new RelayCommand(arg => Reload());
        }

        private void Begin()
        {
            BidsModel.Begin();
        }

        private void Prev()
        {
            BidsModel.Prev();
        }

        private void Next()
        {
            BidsModel.Next();
        }

        private void End()
        {
            BidsModel.End();
        }

        private void Search()
        {
            BidsModel.Search();
        }

        private void Delay()
        {
            BidsModel.Delay();
        }

        private void New()
        {
            BidsModel.New();
        }

        private void Edit()
        {
            BidsModel.Edit();
        }

        private void Ok()
        {
            BidsModel.Ok();
        }

        private void Cancel()
        {
            BidsModel.Cancel();
        }

        private void Further()
        {
            BidsModel.Further();
        }

        private void Reload()
        {
            BidsModel.Reload();
        }

    }

    public interface IBidsModel
    {
        ObservableCollection<Order> SingleOrdersSet { get; set; }
        ObservableCollection<Order> TemporaryOrdersSet { get; set; }
        ObservableCollection<Order> VirtueOrdersSet { get; set; }
        ObservableCollection<Order> OrdersSet { get; set; }
        Order CurrentSingleOrder { get; set; }
        Order CurrentTemporaryOrder { get; set; }
        Order CurrentVirtueOrder { get; set; }
        Order CurrentOrder { get; set; }

        void Begin();
        void Cancel();
        void Delay();
        void Edit();
        void End();
        void Further();
        void New();
        void Next();
        void Ok();
        void Prev();
        void Reload();
        void Search();
    }

    public class BidsModel : IBidsModel
    {
        public ObservableCollection<Order> SingleOrdersSet { get; set; }
        public ObservableCollection<Order> TemporaryOrdersSet { get; set; }
        public ObservableCollection<Order> VirtueOrdersSet { get; set; }
        public ObservableCollection<Order> OrdersSet { get; set; }
        public Order CurrentSingleOrder { get; set; } 
        public Order CurrentTemporaryOrder { get; set; }
        public Order CurrentVirtueOrder
        {
            get;
            set;
        }
        public Order CurrentOrder
        {
            get;
            set;
        }

        public BidsModel()
        {
            Query();
        }

        public void Begin()
        {
            throw new NotImplementedException();
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public void Delay()
        {
            throw new NotImplementedException();
        }

        public void Edit()
        {
            throw new NotImplementedException();
        }

        public void End()
        {
            throw new NotImplementedException();
        }

        public void Further()
        {
            throw new NotImplementedException();
        }

        public void New()
        {
            throw new NotImplementedException();
        }

        public void Next()
        {
            throw new NotImplementedException();
        }

        public void Ok()
        {
            throw new NotImplementedException();
        }

        public void Prev()
        {
            throw new NotImplementedException();
        }

        public void Reload()
        {
            throw new NotImplementedException();
        }

        public void Search()
        {
            throw new NotImplementedException();
        }

        private void Query()
        {
            OrdersSet = new ObservableCollection<Order>(
                from orders in OrdersWrapper.CurrentTable().Table.AsEnumerable()
                join elems in OrderElementsWrapper.CurrentTable().Table.AsEnumerable()
                on orders.Field<int>("f_ord_id") equals elems.Field<int>("f_ord_id")
                where orders.Field<int>("f_ord_id") != 0 & orders.Field<int>("f_order_type_id") != 0 &&
                CommonHelper.NotDeleted(orders)
                select new Order
                {
                    Id = orders.Field<int>("f_ord_id"),
                    Number = orders.Field<int>("f_reg_number"),
                    TypeId = orders.Field<int>("f_order_type_id"),
                    Type = SprOrderTypesWrapper.CurrentTable().Table.AsEnumerable()
                        .FirstOrDefault(arg => arg.Field<int>("f_order_type_id") ==
                        orders.Field<int>("f_order_type_id"))["f_order_text"].ToString(),
                    From = orders.Field<DateTime>("f_date_from"),
                    To = orders.Field<DateTime>("f_date_to"),
                    CatcherId = elems.Field<int>("f_catcher_id"),
                    Catcher = VisitorsWrapper.CurrentTable().Table.AsEnumerable()
                        .FirstOrDefault(arg => arg.Field<int>("f_visitor_id") ==
                        elems.Field<int>("f_catcher_id"))["f_full_name"].ToString(),
                    RegNumber = orders.Field<int>("f_reg_number").ToString() + "-" +
                        SprOrderTypesWrapper.CurrentTable().Table.AsEnumerable()
                        .FirstOrDefault(arg => arg.Field<int>("f_order_type_id") ==
                        orders.Field<int>("f_order_type_id"))["f_order_text"]
                        .ToString().Substring(0, 1),
                    SignedId = orders.Field<int>("f_signed_by"),
                    Signed = VisitorsWrapper.CurrentTable().Table.AsEnumerable()
                        .FirstOrDefault(arg => arg.Field<int>("f_visitor_id") == 
                        orders.Field<int>("f_signed_by"))["f_full_name"].ToString(),
                    AgreeId = orders.Field<int>("f_adjusted_with"),
                    Note = orders.Field<string>("f_notes"),
                    OrganizationId = (int)VisitorsWrapper.CurrentTable().Table
                        .AsEnumerable().FirstOrDefault(arg => arg.Field<int>
                        ("f_visitor_id") == elems.Field<int>("f_visitor_id"))
                        ["f_org_id"],
                    Passes = elems.Field<string>("f_passes")
                });
            SingleOrdersSet = new ObservableCollection<Order>(OrdersSet.Where(
                arg => arg.Type == "Разовая"));
            if (SingleOrdersSet.Count > 0)
            {
                CurrentSingleOrder = SingleOrdersSet[0];
            }
            TemporaryOrdersSet = new ObservableCollection<Order>(OrdersSet.Where(
                arg => arg.Type == "Временная"));
            if (TemporaryOrdersSet.Count > 0)
            {
                CurrentTemporaryOrder = TemporaryOrdersSet[0];
            }
            VirtueOrdersSet = new ObservableCollection<Order>(OrdersSet.Where(
                arg => arg.Type == "На основании"));
            if (VirtueOrdersSet.Count > 0)
            {
                CurrentVirtueOrder = VirtueOrdersSet[0];
            }

        }
    }
}
