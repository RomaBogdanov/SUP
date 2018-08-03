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
            switch (OrderType)
            {
                case OrderType.Temp:
	                foreach (OrderElement currentOrderOrderElement in CurrentOrder.OrderElements)
	                {
		                currentOrderOrderElement.From = CurrentTemporaryOrder.From;
		                currentOrderOrderElement.To = CurrentTemporaryOrder.To;
	                }
                    OrdersWrapper.CurrentTable().AddRow(CurrentTemporaryOrder);
                    break;
                case OrderType.Single:
	                foreach (OrderElement currentOrderOrderElement in CurrentOrder.OrderElements)
	                {
		                currentOrderOrderElement.From = new DateTime(CurrentSingleOrder.From.Year, CurrentSingleOrder.From.Year, CurrentSingleOrder.From.Year,
							currentOrderOrderElement.From.Hour,currentOrderOrderElement.From.Minute,currentOrderOrderElement.From.Second);
		                currentOrderOrderElement.To = new DateTime(CurrentSingleOrder.To.Year, CurrentSingleOrder.To.Year, CurrentSingleOrder.To.Year,
			                currentOrderOrderElement.To.Hour, currentOrderOrderElement.To.Minute, currentOrderOrderElement.To.Second);
					}
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