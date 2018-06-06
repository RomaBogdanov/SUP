using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;
using SupRealClient.Common;
using SupRealClient.Models.AddUpdateModel;
using SupRealClient.ViewModels.AddUpdateViewModel;
using SupRealClient.Views;

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
                TemporaryOrdersSet = bidsModel.TemporaryOrdersSet;
                OrdersSet = bidsModel.OrdersSet;
                SingleOrdersSet = bidsModel.SingleOrdersSet;
                VirtueOrdersSet = bidsModel.VirtueOrdersSet;
                CurrentTemporaryOrder = bidsModel.CurrentTemporaryOrder;
                CurrentOrder = bidsModel.CurrentOrder;
                CurrentSingleOrder = bidsModel.CurrentSingleOrder;
                CurrentVirtueOrder = bidsModel.CurrentVirtueOrder;
                IsCanAddRows = bidsModel.IsCanAddRows;
                AddUpdVisib = bidsModel.IsAddUpdVisib;
                bidsModel.OnRefresh += BidsModel_OnRefresh;
            }
        }

        private void BidsModel_OnRefresh()
        {
            CurrentTemporaryOrder = BidsModel.CurrentTemporaryOrder;
            CurrentSingleOrder = BidsModel.CurrentSingleOrder;
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
            {
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

        bool isTempOrder;

        public bool IsTempOrder
        {
            get
            {
                return isTempOrder;
            }
            set
            {
                isTempOrder = value;
                if (isTempOrder)
                {
                    BidsModel.OrderType = OrderType.Temp;
                }
                OnPropertyChanged();
            }
        }

        bool isSingleOrder;
        

        public bool IsSingleOrder
        {
            get
            {
                return isSingleOrder;
            }
            set
            {
                isSingleOrder = value;
                if (isSingleOrder)
                {
                    BidsModel.OrderType = OrderType.Single;
                }

                OnPropertyChanged();
            }
        }
        
        public bool IsCanAddRows
        {
            get
            {
                if (BidsModel == null)
                {
                    return false;
                }
                return BidsModel.IsCanAddRows;
            }
            set
            {
                BidsModel.IsCanAddRows = value;
                OnPropertyChanged();
            }
        }

        public Visibility AddUpdVisib
        {
            get { return BidsModel.IsAddUpdVisib; }
            set
            {
                BidsModel.IsAddUpdVisib = value;
                OnPropertyChanged();
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

        public ICommand AddPersonCommand { get; set; }
        public ICommand SignerSingleCommand { get; set; } // посписывающий разовую
        public ICommand SignerTempCommand { get; set; } // посписывающий временную
        public  ICommand AgreerCommand { get; set; } // согласующий

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
            AddPersonCommand = new RelayCommand(arg => AddPerson());
            SignerSingleCommand = new RelayCommand(arg => SignerSingle());
            SignerTempCommand = new RelayCommand(arg => SignerTemp());
            AgreerCommand = new RelayCommand(arg => Agreer());
        }

        private void Agreer()
        {
            BidsModel.Agreer();
        }

        private void SignerSingle()
        {
            BidsModel.SignerSingle();
        }

        private void SignerTemp()
        {
            BidsModel.SignerTemp();
        }

        private void AddPerson()
        {
            BidsModel.AddPerson();
        }

        private void Begin()
        {
            BidsModel.Begin();
            CurrentTemporaryOrder = BidsModel.CurrentTemporaryOrder;
            CurrentSingleOrder = BidsModel.CurrentSingleOrder;
        }

        private void End()
        {
            BidsModel.End();
            CurrentTemporaryOrder = BidsModel.CurrentTemporaryOrder;
            CurrentSingleOrder = BidsModel.CurrentSingleOrder;
        }

        private void Next()
        {
            BidsModel.Next();
            CurrentTemporaryOrder = BidsModel.CurrentTemporaryOrder;
            CurrentSingleOrder = BidsModel.CurrentSingleOrder;
        }

        private void Prev()
        {
            BidsModel.Prev();
            CurrentTemporaryOrder = BidsModel.CurrentTemporaryOrder;
            CurrentSingleOrder = BidsModel.CurrentSingleOrder;
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
            //BidsModel.New();
            BidsModel = new NewBidsModel();
            CurrentTemporaryOrder = BidsModel.CurrentTemporaryOrder;
            CurrentSingleOrder = BidsModel.CurrentSingleOrder;
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
            //BidsModel.Cancel();
            BidsModel = new BidsModel();
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

    public enum OrderType
    {
        Temp, // временная заявка
        Single // разовая заявка
    }

    public interface IBidsModel
    {
        /// <summary>
        /// Срабатывает при обновлении данных.
        /// </summary>
        event Action OnRefresh;

        ObservableCollection<Order> SingleOrdersSet { get; set; }
        ObservableCollection<Order> TemporaryOrdersSet { get; set; }
        ObservableCollection<Order> VirtueOrdersSet { get; set; }
        ObservableCollection<Order> OrdersSet { get; set; }
        Order CurrentSingleOrder { get; set; }
        Order CurrentTemporaryOrder { get; set; }
        Order CurrentVirtueOrder { get; set; }
        Order CurrentOrder { get; set; }
        OrderType OrderType { get; set; }
        bool IsCanAddRows { get; set; }
        Visibility IsAddUpdVisib { get; set; }

        void AddPerson();
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
        void Agreer();
        void SignerSingle();
        void SignerTemp();
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
        public OrderType OrderType { get; set; }
        public bool IsCanAddRows { get; set; } = false;

        public Visibility IsAddUpdVisib { get; set; } = Visibility.Hidden;

        public BidsModel()
        {
            Query();
        }

        public event Action OnRefresh;

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

        public void Further()
        {
            throw new NotImplementedException();
        }

        public void New()
        {

        }

        public void Begin()
        {
            switch (OrderType)
            {
                case OrderType.Temp:
                {
                    if (TemporaryOrdersSet.Count > 0)
                    {
                        CurrentTemporaryOrder = TemporaryOrdersSet[0];
                    }
                    break;
                }
                case OrderType.Single:
                {
                    if (SingleOrdersSet.Count > 0)
                    {
                        CurrentSingleOrder = SingleOrdersSet[0];
                    }

                    break;
                }

                default:
                    throw new Exception("Unexpected Case");
            }
        }

        public void End()
        {
            switch (OrderType)
            {
                case OrderType.Temp:
                {
                    if (TemporaryOrdersSet.Count > 0)
                    {
                        CurrentTemporaryOrder = TemporaryOrdersSet[
                            TemporaryOrdersSet.Count - 1];
                    }
                    break;
                }
                case OrderType.Single:
                {
                    if (SingleOrdersSet.Count > 0)
                    {
                        CurrentSingleOrder = SingleOrdersSet[
                            SingleOrdersSet.Count - 1];
                    }
                    break;
                }

                default:
                    throw new Exception("Unexpected Case");
            }
        }

        public void Prev()
        {
            switch (OrderType)
            {
                case OrderType.Temp:
                {
                    if (TemporaryOrdersSet.Count > 0 && TemporaryOrdersSet
                            .IndexOf(CurrentTemporaryOrder) > 0)
                    {
                        CurrentTemporaryOrder = TemporaryOrdersSet[
                            TemporaryOrdersSet.IndexOf(CurrentTemporaryOrder) - 1];
                    }
                    break;
                }
                case OrderType.Single:
                {
                    if (SingleOrdersSet.Count > 0 &&
                        SingleOrdersSet.IndexOf(CurrentSingleOrder) > 0)
                    {
                        CurrentSingleOrder = SingleOrdersSet[
                            SingleOrdersSet.IndexOf(CurrentSingleOrder) - 1];
                    }

                    break;
                }

                default:
                    throw new Exception("Unexpected Case");
            }
        }

        public void Next()
        {
            switch (OrderType)
            {
                case OrderType.Temp:
                    {
                        if (TemporaryOrdersSet.Count > 0 && TemporaryOrdersSet
                               .IndexOf(CurrentTemporaryOrder) <
                           TemporaryOrdersSet.Count - 1)
                        {
                            CurrentTemporaryOrder = TemporaryOrdersSet[
                                TemporaryOrdersSet.IndexOf(CurrentTemporaryOrder) + 1];
                        }
                        break;
                    }
                case OrderType.Single:
                    {
                        if (SingleOrdersSet.Count > 0 &&
                            SingleOrdersSet.IndexOf(CurrentSingleOrder) < 
                            SingleOrdersSet.Count - 1)
                        {
                            CurrentSingleOrder = SingleOrdersSet[
                                SingleOrdersSet.IndexOf(CurrentSingleOrder) + 1];
                        }

                        break;
                    }

                default:
                    throw new Exception("Unexpected Case");
            }
        }

        public void Ok()
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

        public void Agreer()
        {
            throw new NotImplementedException();
        }

        public void SignerSingle()
        {
            throw new NotImplementedException();
        }

        private void Query()
        {
            OrdersSet = new ObservableCollection<Order>(
                from orders in OrdersWrapper.CurrentTable().Table.AsEnumerable()/*
                join elems in OrderElementsWrapper.CurrentTable().Table.AsEnumerable()
                on orders.Field<int>("f_ord_id") equals elems.Field<int>("f_ord_id")*/
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
                    To = orders.Field<DateTime>("f_date_to"),/*
                    CatcherId = elems.Field<int>("f_catcher_id"),
                    Catcher = VisitorsWrapper.CurrentTable().Table.AsEnumerable()
                        .FirstOrDefault(arg => arg.Field<int>("f_visitor_id") ==
                        elems.Field<int>("f_catcher_id"))["f_full_name"].ToString(),*/
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
                    Note = orders.Field<string>("f_notes"),/*
                    OrganizationId = (int)VisitorsWrapper.CurrentTable().Table
                        .AsEnumerable().FirstOrDefault(arg => arg.Field<int>
                        ("f_visitor_id") == elems.Field<int>("f_visitor_id"))
                        ["f_org_id"],
                    Passes = elems.Field<string>("f_passes"),*/
                    VisitorsList = new ObservableCollection<VisitorOnOrder>(
                        from row in OrderElementsWrapper.CurrentTable().Table.AsEnumerable() 
                        where row.Field<int>("f_ord_id") == orders.Field<int>("f_ord_id")
                        select new VisitorOnOrder
                        {
                            VisitorId = row.Field<int>("f_visitor_id"),
                            CatcherId = row.Field<int>("f_catcher_id"),
                            From = row.Field<DateTime>("f_time_from"),
                            To = row.Field<DateTime>("f_time_to"),
                            Passes = row.Field<string>("f_passes")
                        })
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

        public void AddPerson()
        {
            throw new NotImplementedException();
        }

        public void SignerTemp()
        {
            throw new NotImplementedException();
        }
    }

    public class NewBidsModel : IBidsModel
    {
        public ObservableCollection<Order> SingleOrdersSet { get; set; }
        public ObservableCollection<Order> TemporaryOrdersSet { get; set; }
        public ObservableCollection<Order> VirtueOrdersSet { get; set; }
        public ObservableCollection<Order> OrdersSet { get; set; }
        public Order CurrentSingleOrder { get; set; }
        public Order CurrentTemporaryOrder { get; set; }
        public Order CurrentVirtueOrder { get; set; }
        public Order CurrentOrder { get; set; }
        public OrderType OrderType { get; set; }
        public bool IsCanAddRows { get; set; } = true;

        public Visibility IsAddUpdVisib { get; set; } = Visibility.Visible;

        public NewBidsModel()
        {
            CurrentSingleOrder = new Order();
            CurrentTemporaryOrder = new Order();
            CurrentVirtueOrder = new Order();
            CurrentOrder = new Order()
            {
                VisitorsList = new ObservableCollection<VisitorOnOrder>()
            };

            SingleOrdersSet = new ObservableCollection<Order> {CurrentSingleOrder};
            TemporaryOrdersSet = new ObservableCollection<Order> {CurrentTemporaryOrder};
            VirtueOrdersSet = new ObservableCollection<Order> {CurrentVirtueOrder};
            OrdersSet = new ObservableCollection<Order> {CurrentOrder};
            IsAddUpdVisib = Visibility.Visible;
        }

        public event Action OnRefresh;

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

        public void AddPerson()
        {
            switch (OrderType)
            {
                case OrderType.Temp:
                    AddPersonInTempOrder();
                    break;
                case OrderType.Single:
                    AddPersonInSingleOrder();
                    break;
                default:
                    break;
            }
        }

        private void AddPersonInSingleOrder()
        {
            AddUpdateAbstrModel model = new AddSingleBidModel();
            AddUpdateBaseViewModel viewModel = new AddUpdateBidsViewModel
            {
                Model = model
            };
            AddUpdateBidWindView view = new AddUpdateBidWindView();
            view.DataContext = viewModel;
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            object res = view.WindowResult;
            if (CurrentSingleOrder.VisitorsList == null)
                CurrentSingleOrder.VisitorsList = new ObservableCollection<VisitorOnOrder>();
            CurrentSingleOrder.VisitorsList.Add((VisitorOnOrder) res);
            OnRefresh?.Invoke();
        }

        private void AddPersonInTempOrder()
        {
            AddUpdateAbstrModel model = new AddSingleBidModel();
            AddUpdateBaseViewModel viewModel = new AddUpdateBidsViewModel
            {
                Model = model
            };
            AddUpdateBidWindView view = new AddUpdateBidWindView();
            view.DataContext = viewModel;
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            object res = view.WindowResult;
            if (CurrentTemporaryOrder.VisitorsList == null)
                CurrentTemporaryOrder.VisitorsList = new ObservableCollection<VisitorOnOrder>();
            CurrentTemporaryOrder.VisitorsList.Add((VisitorOnOrder)res);
            OnRefresh?.Invoke();
        }

        public void Agreer()
        {
            VisitorsModelResult result = ViewManager.Instance.OpenWindowModal(
                "VisitorsListWindViewOk", null) as VisitorsModelResult;
            CurrentTemporaryOrder.AgreeId = result.Id;
            CurrentTemporaryOrder.Agree = result.Name;
            OnRefresh?.Invoke();
        }

        public void SignerSingle()
        {
            VisitorsModelResult result = ViewManager.Instance.OpenWindowModal(
                "VisitorsListWindViewOk", null) as VisitorsModelResult;
            CurrentSingleOrder.SignedId = result.Id;
            CurrentSingleOrder.Signed = result.Name;
            OnRefresh?.Invoke();
        }

        public void SignerTemp()
        {
            VisitorsModelResult result = ViewManager.Instance.OpenWindowModal(
                "VisitorsListWindViewOk", null) as VisitorsModelResult;
            CurrentTemporaryOrder.SignedId = result.Id;
            CurrentTemporaryOrder.Signed = result.Name;
            OnRefresh?.Invoke();
        }
    }
}

