using System;
using System.Collections.ObjectModel;
using System.Windows;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;

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

			SingleOrdersSet = new ObservableCollection<Order> {CurrentSingleOrder};
			TemporaryOrdersSet = new ObservableCollection<Order> {CurrentTemporaryOrder};
			VirtueOrdersSet = new ObservableCollection<Order> {CurrentVirtueOrder};
			OrdersSet = new ObservableCollection<Order> {CurrentOrder};
			IsAddUpdVisib = Visibility.Visible;
			IsCanAddRows = true;
		}

		public override void Ok()
		{
			switch (OrderType)
			{
				case OrderType.Single:
					OrdersWrapper.CurrentTable().UpdateRow(CurrentSingleOrder);
					break;
				case OrderType.Temp:
					OrdersWrapper.CurrentTable().UpdateRow(CurrentTemporaryOrder);
					break;
				case OrderType.Virtue:
					OrdersWrapper.CurrentTable().UpdateRow(CurrentVirtueOrder);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}