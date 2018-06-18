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
using MessageBox = System.Windows.Forms.MessageBox;

namespace SupRealClient.Models
{
    public class EditBidsModel : BidsModelBase
    {
        //public override event Action OnRefresh;

        public EditBidsModel(Order singleOrder, Order temporaryOrder, 
            Order virtueOrder, Order order)
        {
            CurrentSingleOrder = singleOrder ?? new Order();
            CurrentTemporaryOrder = temporaryOrder ?? new Order();
            CurrentVirtueOrder = virtueOrder ?? new Order();
            CurrentOrder = order ?? new Order();

            SingleOrdersSet = new ObservableCollection<Order> { CurrentSingleOrder };
            TemporaryOrdersSet = new ObservableCollection<Order> { CurrentTemporaryOrder };
            VirtueOrdersSet = new ObservableCollection<Order> { CurrentVirtueOrder };
            OrdersSet = new ObservableCollection<Order> { CurrentOrder };
            IsAddUpdVisib = Visibility.Visible;
            IsCanAddRows = true;
        }

        public override void Ok()
        {
            OrdersWrapper.CurrentTable().UpdateRow(CurrentSingleOrder);
        }
    }
}