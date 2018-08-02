using System;
using System.Collections.ObjectModel;
using System.Windows;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Models
{
	public class NewBidsModel : BidsModelBase
	{
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
					CurrentTemporaryOrder.To = DateTime.Now;
					CurrentSingleOrder.RecDate = DateTime.Now;
					CurrentSingleOrder.NewRecDate = DateTime.Now;
					CurrentTemporaryOrder.Number = ++maxOrderNumber;
					break;
				case OrderType.Single:
					CurrentSingleOrder.TypeId = 1;
					CurrentSingleOrder.From = DateTime.Now;
					CurrentSingleOrder.RecDate = DateTime.Now;
					CurrentSingleOrder.NewRecDate = DateTime.Now;
					CurrentSingleOrder.Number = ++maxOrderNumber;
					break;
			}
		}

		public override void Ok()
		{
			switch (OrderType)
			{
				case OrderType.Temp:
					OrdersWrapper.CurrentTable().AddRow(CurrentTemporaryOrder);
					CurrentSingleOrder.RecDate = DateTime.Now;
					CurrentSingleOrder.NewRecDate = DateTime.Now;
					break;
				case OrderType.Single:
					CurrentSingleOrder.To = CurrentSingleOrder.From;
					CurrentSingleOrder.OrderDate = DateTime.Now;
					CurrentSingleOrder.RecDate = DateTime.Now;
					CurrentSingleOrder.NewRecDate = DateTime.Now;
					OrdersWrapper.CurrentTable().AddRow(CurrentSingleOrder);
					break;
			}
		}
	}
}