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
                bidsModel.OrderType= CurrentOrderType;
                bidsModel.OnRefresh += BidsModel_OnRefresh;
            }
        }

        private void BidsModel_OnRefresh()
        {
            CurrentTemporaryOrder = BidsModel.CurrentTemporaryOrder;
            CurrentSingleOrder = BidsModel.CurrentSingleOrder;
            CurrentSingleOrder.OrderElements = BidsModel.CurrentSingleOrder.OrderElements;
            CurrentTemporaryOrder.OrderElements = BidsModel.CurrentTemporaryOrder.OrderElements;
            //UpdateVisitor = BidsModel.UpdateVisitor;
        }

        public OrderElement UpdateVisitor
        {
            get { return BidsModel?.UpdateVisitor;}
            set
            {
                BidsModel.UpdateVisitor = value;
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

        /// <summary>
        /// Свойство для определения текущей открытой вкладки.
        /// </summary>
        private OrderType CurrentOrderType
        {
            get
            {
                if (IsTempOrder)
                {
                    return OrderType.Temp;
                }
                else if (IsSingleOrder)
                {
                    return OrderType.Single;
                }

                return OrderType.Single;
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

        public ICommand AddPersonCommand { get; set; } // добавление человека в заявку
        public ICommand UpdatePersonCommand { get; set; } // редактирование человека в заявке
        public ICommand DeletePersonCommand { get; set; } // удаление человека из заявки
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
            UpdatePersonCommand =new RelayCommand(arg => UpdatePerson());
            DeletePersonCommand = new RelayCommand(arg => DeletePerson());

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

        /// <summary>
        /// Добавляет человека в заявку.
        /// </summary>
        private void AddPerson()
        {
            BidsModel.AddPerson();
        }

        /// <summary>
        /// Обновляет информацию о человеке в заявке.
        /// </summary>
        private void UpdatePerson()
        {
            BidsModel.UpdatePerson();
            UpdateVisitor = BidsModel.UpdateVisitor;
        }

        /// <summary>
        /// Удаляет информацию о человеке в заявке.
        /// </summary>
        private void DeletePerson()
        {
            BidsModel.DeletePerson();
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

        /// <summary>
        /// Создание новой заявки
        /// </summary>
        private void New()
        {
            //BidsModel.New();
            BidsModel = new NewBidsModel();
            CurrentTemporaryOrder = BidsModel.CurrentTemporaryOrder;
            CurrentSingleOrder = BidsModel.CurrentSingleOrder;
        }

        /// <summary>
        /// Редактирование заявки.
        /// </summary>
        private void Edit()
        {
            //BidsModel.Edit();
            BidsModel = new EditBidsModel(CurrentSingleOrder,
                CurrentTemporaryOrder, CurrentVirtueOrder, CurrentOrder);

        }

        private void Ok()
        {
            BidsModel.Ok();
            BidsModel = new BidsModel();
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
        OrderElement UpdateVisitor { get; set; }

        void AddPerson();
        void Begin();
        void Cancel();
        void Delay();
        void Edit();
        void End();
        void Further();
        void New();
        void Next();
        /// <summary>
        /// Обрабатывает нажатие кнопки Ок.
        /// </summary>
        void Ok();
        void Prev();
        void Reload();
        void Search();
        void Agreer();
        void SignerSingle();
        void SignerTemp();
        void UpdatePerson();
        void DeletePerson();
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
        public OrderElement UpdateVisitor { get; set; }

        public BidsModel()
        {
            Query();
        }

        public event Action OnRefresh;

        public void Cancel()
        {
            //throw new NotImplementedException();
        }

        public void Delay()
        {
            //throw new NotImplementedException();
        }

        public void Edit()
        {
            //throw new NotImplementedException();
        }

        public void Further()
        {
            //throw new NotImplementedException();
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
            //throw new NotImplementedException();
        }

        public void Reload()
        {
            //throw new NotImplementedException();
        }

        public void Search()
        {
            //throw new NotImplementedException();
        }

        public void Agreer()
        {
            //throw new NotImplementedException();
        }

        public void SignerSingle()
        {
            //throw new NotImplementedException();
        }

        private void Query()
        {
            OrdersSet = (ObservableCollection<Order>) OrdersWrapper.CurrentTable().StandartQuery();

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
            //throw new NotImplementedException();
        }

        public void SignerTemp()
        {
            //throw new NotImplementedException();
        }

        public void UpdatePerson()
        {
            throw new NotImplementedException();
        }

        public void DeletePerson()
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

        public Visibility IsAddUpdVisib { get; set; }// = Visibility.Visible;
        public OrderElement UpdateVisitor { get; set; }

        public NewBidsModel()
        {
            CurrentSingleOrder = new Order();
            CurrentTemporaryOrder = new Order();
            CurrentVirtueOrder = new Order();
            CurrentOrder = new Order();

            SingleOrdersSet = new ObservableCollection<Order> {CurrentSingleOrder};
            TemporaryOrdersSet = new ObservableCollection<Order> {CurrentTemporaryOrder};
            VirtueOrdersSet = new ObservableCollection<Order> {CurrentVirtueOrder};
            OrdersSet = new ObservableCollection<Order> {CurrentOrder};
            IsAddUpdVisib = Visibility.Visible;
        }

        public event Action OnRefresh;

        public void Begin()
        {
            //throw new NotImplementedException();
        }

        public void Cancel()
        {
            //throw new NotImplementedException();
        }

        public void Delay()
        {
            //throw new NotImplementedException();
        }

        public void Edit()
        {
            //throw new NotImplementedException();
        }

        public void End()
        {
            //throw new NotImplementedException();
        }

        public void Further()
        {
            //throw new NotImplementedException();
        }

        public void New()
        {
            //throw new NotImplementedException();
        }

        public void Next()
        {

        }

        /// <summary>
        /// Подтверждение добавления или редактирования заявки.
        /// </summary>
        public void Ok()
        {
            CurrentSingleOrder.TypeId = 1;
            CurrentSingleOrder.To = CurrentSingleOrder.From;
            CurrentSingleOrder.OrderDate= DateTime.Now;
            OrdersWrapper.CurrentTable().AddRow(CurrentSingleOrder);
        }

        public void Prev()
        {
            //throw new NotImplementedException();
        }

        public void Reload()
        {
            //throw new NotImplementedException();
        }

        public void Search()
        {
            //throw new NotImplementedException();
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

        public void UpdatePerson()
        {
            AddUpdateAbstrModel model = new UpdateBidModel(UpdateVisitor);
            AddUpdateBaseViewModel viewModel = new AddUpdateBidsViewModel
            {
                Model = model
            };
            AddUpdateBidWindView view = new AddUpdateBidWindView();
            view.DataContext = viewModel;
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            int i = CurrentSingleOrder.OrderElements.IndexOf(UpdateVisitor);
            CurrentSingleOrder.OrderElements[i] = (view.WindowResult as OrderElement);
            UpdateVisitor = CurrentSingleOrder.OrderElements[i];
            //UpdateVisitor = (VisitorOnOrder)(view.WindowResult as VisitorOnOrder).Clone();
            OnRefresh?.Invoke();
        }

        public void DeletePerson()
        {
            CurrentSingleOrder.OrderElements.Remove(UpdateVisitor);
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
            if (res==null)return;
            if (CurrentSingleOrder.OrderElements == null)
                CurrentSingleOrder.OrderElements = new ObservableCollection<OrderElement>();
            CurrentSingleOrder.OrderElements.Add((OrderElement) res);
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
            if (CurrentTemporaryOrder.OrderElements == null)
                CurrentTemporaryOrder.OrderElements = new ObservableCollection<OrderElement>();
            CurrentTemporaryOrder.OrderElements.Add((OrderElement)res);
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

    public class EditBidsModel : IBidsModel
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

        public Visibility IsAddUpdVisib { get; set; }// = Visibility.Visible;
        public OrderElement UpdateVisitor { get; set; }

        public EditBidsModel(Order singleOrder, Order temporaryOrder, 
            Order virtueOrder, Order order)
        {
            CurrentSingleOrder = singleOrder;
            CurrentTemporaryOrder = temporaryOrder;
            CurrentVirtueOrder = virtueOrder;
            CurrentOrder = order;

            SingleOrdersSet = new ObservableCollection<Order> { CurrentSingleOrder };
            TemporaryOrdersSet = new ObservableCollection<Order> { CurrentTemporaryOrder };
            VirtueOrdersSet = new ObservableCollection<Order> { CurrentVirtueOrder };
            OrdersSet = new ObservableCollection<Order> { CurrentOrder };
            IsAddUpdVisib = Visibility.Visible;
        }

        public event Action OnRefresh;

        public void Agreer()
        {
            VisitorsModelResult result = ViewManager.Instance.OpenWindowModal(
                "VisitorsListWindViewOk", null) as VisitorsModelResult;
            CurrentTemporaryOrder.AgreeId = result.Id;
            CurrentTemporaryOrder.Agree = result.Name;
            OnRefresh?.Invoke();
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
            OrdersWrapper.CurrentTable().UpdateRow(CurrentSingleOrder);
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

        public void UpdatePerson()
        {
            AddUpdateAbstrModel model = new UpdateBidModel(UpdateVisitor);
            AddUpdateBaseViewModel viewModel = new AddUpdateBidsViewModel
            {
                Model = model
            };
            AddUpdateBidWindView view = new AddUpdateBidWindView();
            view.DataContext = viewModel;
            model.OnClose += view.Handling_OnClose;
            view.ShowDialog();
            int i = CurrentSingleOrder.OrderElements.IndexOf(UpdateVisitor);
            CurrentSingleOrder.OrderElements[i] = (view.WindowResult as OrderElement);
            UpdateVisitor = CurrentSingleOrder.OrderElements[i];
            //UpdateVisitor = (VisitorOnOrder)(view.WindowResult as VisitorOnOrder).Clone();
            OnRefresh?.Invoke();
        }

        public void DeletePerson()
        {
            CurrentSingleOrder.OrderElements.Remove(UpdateVisitor);
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
            if (res == null) return;
            if (CurrentSingleOrder.OrderElements == null)
                CurrentSingleOrder.OrderElements = new ObservableCollection<OrderElement>();
            CurrentSingleOrder.OrderElements.Add((OrderElement)res);
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
            if (CurrentTemporaryOrder.OrderElements == null)
                CurrentTemporaryOrder.OrderElements = new ObservableCollection<OrderElement>();
            CurrentTemporaryOrder.OrderElements.Add((OrderElement)res);
            OnRefresh?.Invoke();
        }

    }
}

