using System;
using System.Collections.ObjectModel;
using System.Windows;
using SupContract;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Models
{
	public class NewBidsModel : BidsModelBase
	{
		public NewBidsModel(OrderType currentOrderType = OrderType.Single)
		{
			OrderType = currentOrderType;
			CurrentTemporaryOrder = new Order();
			CurrentSingleOrder = new Order();
			CurrentVirtueOrder = new Order(true);
			CurrentOrder = new Order();

			SingleOrdersSet = new ObservableCollection<Order> {CurrentSingleOrder};
			TemporaryOrdersSet = new ObservableCollection<Order> {CurrentTemporaryOrder};
			VirtueOrdersSet = new ObservableCollection<Order> {CurrentVirtueOrder};
			OrdersSet = new ObservableCollection<Order> {CurrentOrder};
			IsAddUpdVisib = Visibility.Visible;
			IsCanAddRows = true;
			switch (OrderType)
			{
				case OrderType.Single:
					CurrentSingleOrder.TypeId = 1;
					CurrentSingleOrder.From = DateTime.Now;
					CurrentSingleOrder.RecDate = DateTime.Now;
					CurrentSingleOrder.NewRecDate = DateTime.Now;
					CurrentSingleOrder.Number = ++maxOrderNumber;
					break;
				case OrderType.Temp:
					CurrentTemporaryOrder.TypeId = 2;
					CurrentTemporaryOrder.From = DateTime.Now;
					CurrentTemporaryOrder.To = DateTime.Now;
					CurrentTemporaryOrder.RecDate = DateTime.Now;
					CurrentTemporaryOrder.NewRecDate = DateTime.Now;
					CurrentTemporaryOrder.Number = ++maxOrderNumber;
					break;
				case OrderType.Virtue:
					CurrentVirtueOrder.TypeId = 3;
					CurrentVirtueOrder.From = DateTime.Now;
					CurrentVirtueOrder.To = DateTime.Now;
					CurrentVirtueOrder.RecDate = DateTime.Now;
					CurrentVirtueOrder.NewRecDate = DateTime.Now;
					CurrentVirtueOrder.Number = ++maxOrderNumber;
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
					CurrentTemporaryOrder.RecDate = DateTime.Now;

					int? tempImageID = ImagesHelper.AddImage_ByImageID(-1, CurrentTemporaryOrder.ImageGuid, ImageType.Document);
					CurrentTemporaryOrder.ImageId = tempImageID != null ? tempImageID.Value : -1;

					OrdersWrapper.CurrentTable().AddRow(CurrentTemporaryOrder);
					break;
				case OrderType.Single:
					foreach (OrderElement currentOrderOrderElement in CurrentOrder.OrderElements)
					{
						currentOrderOrderElement.From = new DateTime(CurrentSingleOrder.From.Year, CurrentSingleOrder.From.Year,
							CurrentSingleOrder.From.Year,
							currentOrderOrderElement.From.Hour, currentOrderOrderElement.From.Minute, currentOrderOrderElement.From.Second);
						currentOrderOrderElement.To = new DateTime(CurrentSingleOrder.To.Year, CurrentSingleOrder.To.Year,
							CurrentSingleOrder.To.Year,
							currentOrderOrderElement.To.Hour, currentOrderOrderElement.To.Minute, currentOrderOrderElement.To.Second);
					}

					CurrentSingleOrder.RecDate = DateTime.Now;
					CurrentSingleOrder.To = CurrentSingleOrder.From;
					CurrentSingleOrder.OrderDate = DateTime.Now;

					int? singleImageID = ImagesHelper.AddImage_ByImageID(-1, CurrentSingleOrder.ImageGuid, ImageType.Document);
					CurrentSingleOrder.ImageId = singleImageID != null ? singleImageID.Value : -1;

					OrdersWrapper.CurrentTable().AddRow(CurrentSingleOrder);
					break;
				case OrderType.Virtue:
					foreach (OrderElement currentOrderOrderElement in CurrentVirtueOrder.OrderElements)
					{
						currentOrderOrderElement.From = CurrentVirtueOrder.From;
						currentOrderOrderElement.To = CurrentVirtueOrder.To;
					}
					CurrentVirtueOrder.OrderDate = DateTime.Now;
					CurrentVirtueOrder.RecDate = DateTime.Now;

					OrdersWrapper.CurrentTable().AddRow(CurrentVirtueOrder);
					break;
				default:
					break;
			}
		}
	}
}