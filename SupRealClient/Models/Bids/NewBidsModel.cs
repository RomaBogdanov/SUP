using System;
using System.Collections.ObjectModel;
using System.Windows;
using SupRealClient.EnumerationClasses;
using SupRealClient.Models;
using SupRealClient.Models.AddUpdateModel;
using SupRealClient.TabsSingleton;
using SupRealClient.ViewModels;
using SupRealClient.ViewModels.AddUpdateViewModel;
using SupRealClient.Views;

namespace SupRealClient.Models
{
    public class NewBidsModel : BidsModelBase
    {
        //public override event Action OnRefresh;

        public NewBidsModel(OrderType currentOrderType = OrderType.Single)
        {
            OrderType = currentOrderType;
            CurrentSingleOrder = new Order();
            CurrentTemporaryOrder = new Order();
            CurrentVirtueOrder = new Order();
            CurrentOrder = new Order();

            SingleOrdersSet = new ObservableCollection<Order> {CurrentSingleOrder};
            TemporaryOrdersSet = new ObservableCollection<Order> {CurrentTemporaryOrder};
            VirtueOrdersSet = new ObservableCollection<Order> {CurrentVirtueOrder};
            OrdersSet = new ObservableCollection<Order> {CurrentOrder};
            IsAddUpdVisib = Visibility.Visible;
            IsCanAddRows = true;
            switch (OrderType)
            {
                case OrderType.Temp:
                    CurrentTemporaryOrder.TypeId = 2;
                    CurrentTemporaryOrder.From = DateTime.Now;
                    CurrentTemporaryOrder.To=DateTime.Now;
                    CurrentTemporaryOrder.Number = ++maxOrderNumber;
                    break;
                case OrderType.Single:
                    CurrentSingleOrder.TypeId = 1;
                    CurrentSingleOrder.From = DateTime.Now;
                    CurrentSingleOrder.Number = ++maxOrderNumber;
                    break;
                default:
                    break;
            }
        }

        public override void Ok()
        {
            //CurrentSingleOrder.TypeId = 1;
            switch (OrderType)
            {
                case OrderType.Temp:
                    OrdersWrapper.CurrentTable().AddRow(CurrentTemporaryOrder);
                    break;
                case OrderType.Single:
                    CurrentSingleOrder.To = CurrentSingleOrder.From;
                    CurrentSingleOrder.OrderDate = DateTime.Now;
                    OrdersWrapper.CurrentTable().AddRow(CurrentSingleOrder);
                    break;
                default:
                    break;
            }

        }
    }
}