using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Models
{
	public class BidsModel : BidsModelBase
	{
		public override event Action OnRefresh;

		public BidsModel()
		{
			IsAddUpdVisib = Visibility.Hidden;
			Query();
		}

		private void Query()
		{
			OrdersSet = (ObservableCollection<Order>) OrdersWrapper.CurrentTable().StandartQuery();
			var currentYearOrders = OrdersSet.Where(arg =>
				(arg.NewRecDate != null) &&
				Convert.ToDateTime(arg.NewRecDate).Year == DateTime.Now.Year);
			maxOrderNumber = OrdersSet.Count > 0 ? currentYearOrders.Max(arg => arg.Number) : 1;
			SingleOrdersSet = new ObservableCollection<Order>(OrdersSet.Where(
				arg => arg.Type == "Разовая"));
			if (SingleOrdersSet.Count > 0)
			{
				CurrentSingleOrder = SingleOrdersSet[0];
			}

			TemporaryOrdersSet = new ObservableCollection<Order>(OrdersSet.Where(
				arg => arg.Type == "Временная"));
			if (TemporaryOrdersSet.Count > 0)
			{
				CurrentTemporaryOrder = TemporaryOrdersSet[0];
			}

			VirtueOrdersSet = new ObservableCollection<Order>(OrdersSet.Where(
				arg => arg.Type == "На основании" || arg.Type == "Бессрочная")); // todo: убрать костыль на проверку с бессрочностью, но убедившись, что типы заявок совпадают в базе и в коде
			if (VirtueOrdersSet.Count > 0)
			{
				CurrentVirtueOrder = VirtueOrdersSet[0];
			}

		}
	}
}