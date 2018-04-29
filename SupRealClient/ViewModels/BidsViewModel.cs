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

        public ObservableCollection<Order> CurrentSingleOrder
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

        public ObservableCollection<Order> CurrentTemporaryOrder
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

        public ObservableCollection<Order> CurrentVirtueOrder
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

        public ObservableCollection<Order> CurrentOrder
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
        ObservableCollection<Order> CurrentSingleOrder { get; set; }
        ObservableCollection<Order> CurrentTemporaryOrder { get; set; }
        ObservableCollection<Order> CurrentVirtueOrder { get; set; }
        ObservableCollection<Order> CurrentOrder { get; set; }

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
        public ObservableCollection<Order> SingleOrdersSet { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ObservableCollection<Order> TemporaryOrdersSet { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ObservableCollection<Order> VirtueOrdersSet { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ObservableCollection<Order> OrdersSet { get; set; }
        public ObservableCollection<Order> CurrentSingleOrder { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ObservableCollection<Order> CurrentTemporaryOrder { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ObservableCollection<Order> CurrentVirtueOrder
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public ObservableCollection<Order> CurrentOrder { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
                where orders.Field<int>("f_ord_id") != 0
                select new Order
                {
                    Id = orders.Field<int>("f_ord_id"),
                    From = orders.Field<DateTime>("f_date_from"),
                    To = orders.Field<DateTime>("f_date_to"),
                    RegNumber = orders.Field<int>("f_reg_number").ToString()
                });
        }
    }
}
