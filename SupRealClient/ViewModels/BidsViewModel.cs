using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using SupRealClient.EnumerationClasses;
using SupRealClient.Common;
using SupRealClient.Models;

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
        public ICommand SignerCommand { get; set; } // посписывающий заявку
        //public ICommand SignerTempCommand { get; set; } // посписывающий временную
        public  ICommand AgreerCommand { get; set; } // согласующий

        private bool _textEnable = false;
        /// <summary>
        /// Доступность редактирования полей.
        /// </summary>
        public bool TextEnable
        {
            get { return _textEnable; }
            set { _textEnable = value; OnPropertyChanged("TextEnable"); }
        }

        private bool _cceptButtonEnable = false;
        /// <summary>
        /// Доступность кнопок Применить и Отмена.
        /// </summary>
        public bool AcceptButtonEnable
        {
            get { return _cceptButtonEnable; }
            set {
                _cceptButtonEnable = value;
                NavigateButtonEnable = !value;
                OnPropertyChanged(); }
        }

        /// <summary>
        /// Доступность кнопок нижнего ряда, кроме кнопок Применить, Отмена.
        /// </summary>
        public bool NavigateButtonEnable
        {
            get { return !_cceptButtonEnable; }
            set
            {
                OnPropertyChanged();
            }
        }

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

            SignerCommand = new RelayCommand(arg => Signer());
            //SignerTempCommand = new RelayCommand(arg => SignerTemp());
            AgreerCommand = new RelayCommand(arg => Agreer());

            TextEnable = false; // При открытии окна поля недоступны.
            AcceptButtonEnable = false; // При открытии кнопки применить и отмена недоступны.
        }

        private void Agreer()
        {
            BidsModel.Agreer();
        }

        private void Signer()
        {
            BidsModel.Signer();
        }
        /*
        private void SignerTemp()
        {
            BidsModel.SignerTemp();
        }*/

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
            BidsModel = new NewBidsModel(CurrentOrderType);
            CurrentTemporaryOrder = BidsModel.CurrentTemporaryOrder;
            CurrentSingleOrder = BidsModel.CurrentSingleOrder;

            TextEnable = true; // При открытии окна поля недоступны.
            AcceptButtonEnable = true; // При открытии кнопки применить и отмена недоступны.
        }

        /// <summary>
        /// Редактирование заявки.
        /// </summary>
        private void Edit()
        {
            //BidsModel.Edit();
            BidsModel = new EditBidsModel(CurrentSingleOrder,
                CurrentTemporaryOrder, CurrentVirtueOrder, CurrentOrder);

            TextEnable = true; // При открытии окна поля недоступны.
            AcceptButtonEnable = true; // При открытии кнопки применить и отмена недоступны.
        }

        private void Ok()
        {
            BidsModel.Ok();
            int id = BidsModel.CurrentSingleOrder.Id;
            BidsModel = new BidsModel();
            CurrentSingleOrder = SingleOrdersSet.FirstOrDefault(arg => arg.Id == id);
            TextEnable = false;
            AcceptButtonEnable = false;
        }

        private void Cancel()
        {
            //BidsModel.Cancel();
            BidsModel = new BidsModel();

            TextEnable = false; // При открытии окна поля недоступны.
            AcceptButtonEnable = false; // При открытии кнопки применить и отмена недоступны.
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
}

