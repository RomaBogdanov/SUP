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
            IsCanAddRows = true;
        }

        public override void Ok()
        {
            CurrentSingleOrder.TypeId = 1;
            CurrentSingleOrder.To = CurrentSingleOrder.From;
            CurrentSingleOrder.OrderDate= DateTime.Now;
            OrdersWrapper.CurrentTable().AddRow(CurrentSingleOrder);
        }
    }
}