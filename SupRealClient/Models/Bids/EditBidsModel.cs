using System;
using System.Collections.ObjectModel;
using System.Windows;
using SupContract;
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
			CurrentVirtueOrder = virtueOrder ?? new Order(true);
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
					{
						int? imageID = ImagesHelper.AddImage_ByImageID(CurrentSingleOrder.ImageId, CurrentSingleOrder.ImageGuid, ImageType.Document);
						CurrentSingleOrder.ImageId = imageID != null ? imageID.Value : -1;

						OrdersWrapper.CurrentTable().UpdateRow(CurrentSingleOrder);
					}
					break;
				case OrderType.Temp:
					{
						int? imageID = ImagesHelper.AddImage_ByImageID(CurrentTemporaryOrder.ImageId, CurrentTemporaryOrder.ImageGuid, ImageType.Document);
						CurrentTemporaryOrder.ImageId = imageID != null ? imageID.Value : -1;
						
						OrdersWrapper.CurrentTable().UpdateRow(CurrentTemporaryOrder);
					}
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