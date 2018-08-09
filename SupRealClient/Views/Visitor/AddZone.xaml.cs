using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SupRealClient.EnumerationClasses;
using SupRealClient.TabsSingleton;

namespace SupRealClient.Views.Visitor
{
	/// <summary>
	/// Interaction logic for AddZone.xaml
	/// </summary>
	public partial class AddZone
	{
		public AddZone()
		{
			InitializeComponent();
		}

		public void Handling_OnClose(object result = null)
		{
			this.Close();
		}
	}

	public class AddZoneViewModel : Base4ViewModel<Order>
	{
		public ICommand Virtue { get; set; }

		public AddZoneViewModel()
		{
			Virtue = new RelayCommand(obj => VirtueCommand());
		}

		private void VirtueCommand()
		{
			(Model as AddZoneModel).Virtue();
		}
	}

	public class AddZoneModel : Base4ModelAbstr<Order>
	{
		private int visitorId;

		public AddZoneModel(ObservableCollection<Order> orders, int visitorId)
		{
			this.visitorId = visitorId;
			//Query();
			Set = orders;
		}

		protected override DataTable Table
		{
			get { return OrderElementsWrapper.CurrentTable().Table; }
		}

		public override void Add()
		{
			//ViewManager.Instance.OpenWindow("Base4CardsWindView", null);
			// todo: попытка обойти ограничение нашей реализации фабрики
			// todo: связанное с тем, что нужно текущей форме задать другой
			// todo: Model, по хорошему, надо придумать стандартный механизм.

			if (Set.Where(o => o.IsChecked).Count() == 0)
			{
				MessageBox.Show($"Количество выбранных заявок не больше быть равным нулю", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			else
			{
				this.Close();
				Base4CardsWindView wind = new Base4CardsWindView(Visibility.Visible);
				((Base4ViewModel<Card>)wind.base4.DataContext).Model =
					new CardsActiveListModel<Card>(visitorId,
						new ObservableCollection<Order>(Set.Where(o => o.IsChecked)));
				((Base4ViewModel<Card>)wind.base4.DataContext).Model.OnClose += wind.Handling_OnClose2;
				wind.Show();
			}
		}

		public override void Update()
		{

		}

		public void Virtue()
		{
			var wind = new BidsView();

			wind.Show();
			wind.SetToVirtue();
		}

		protected override void DoQuery()
		{
			/*Set = new ObservableCollection<OrderElement>((
			    (ObservableCollection<OrderElement>) OrderElementsWrapper
			    .CurrentTable().StandartQuery())
			    .Where(arg => arg.VisitorId == visitorId));*/
		}

		protected override BaseModelResult GetResult()
		{
			throw new NotImplementedException();
		}
	}
}