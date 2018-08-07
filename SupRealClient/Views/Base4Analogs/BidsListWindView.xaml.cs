using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using SupRealClient.EnumerationClasses;

namespace SupRealClient.Views
{
	/// <summary>
	/// Логика взаимодействия для VisitorsListWindView.xaml
	/// </summary>
	public partial class BidsListWindView
	{
		public BidsListWindView(Visibility okVisibility)
		{
			InitializeComponent();
			var viewModel = new Base4ViewModel<Order>
			{
				OkCaption = "OK",
				Parent = this,
				Model = new OrdersListModel<Order>()
			};
			viewModel.Model.OnClose += Handling_OnClose;
			base4.DataContext = viewModel;
			AfterInitialize();
			((Base4ViewModel<Order>) base4.DataContext)
				.OkVisibility = okVisibility;
			((Base4ViewModel<Order>) base4.DataContext)
				.ScrollCurrentItem = base4.ScrollIntoViewCurrentItem;
		}

		partial void CreateColumns()
		{
			DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
			{
				Header = "Номер заявки",
				Binding = new Binding("RegNumber")
			};
			base4.baseTab.Columns.Add(dataGridTextColumn);
			dataGridTextColumn = new DataGridTextColumn
			{
				Header = "Дата начала",
				Binding = new Binding("From")
			};
			base4.baseTab.Columns.Add(dataGridTextColumn);
			dataGridTextColumn = new DataGridTextColumn
			{
				Header = "Дата окончания",
				Binding = new Binding("To")
			};
			base4.baseTab.Columns.Add(dataGridTextColumn);
			dataGridTextColumn = new DataGridTextColumn
			{
				Header = "Примечание",
				Binding = new Binding("Note")
			};
			base4.baseTab.Columns.Add(dataGridTextColumn);
		}
	}
}