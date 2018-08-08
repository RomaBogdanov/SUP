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
			DataGridTextColumn dataGridTextColumn = null;
			DataGridCheckBoxColumn dataGridCheckBoxColumn = null;
			dataGridTextColumn = new DataGridTextColumn
			{
				Header = "№",
				Binding = new Binding("RegNumber")
			};
			base4.baseTab.Columns.Add(dataGridTextColumn);
			dataGridCheckBoxColumn = new DataGridCheckBoxColumn
			{
				Header = "Неактивна",
				Binding = new Binding("IsDisable")
			};
			base4.baseTab.Columns.Add(dataGridCheckBoxColumn);
			dataGridTextColumn = new DataGridTextColumn
			{
				Header = "Дата начала",
				Binding = new Binding("From")
			};
			dataGridTextColumn.Binding.StringFormat = "dd.MM.yyyy";
			base4.baseTab.Columns.Add(dataGridTextColumn);
			dataGridTextColumn = new DataGridTextColumn
			{
				Header = "Дата окончания",
				Binding = new Binding("To")
			};
			dataGridTextColumn.Binding.StringFormat = "dd.MM.yyyy";
			base4.baseTab.Columns.Add(dataGridTextColumn);
			dataGridTextColumn = new DataGridTextColumn
			{
				Header = "Подписано",
				Binding = new Binding("Signed")
			};
			base4.baseTab.Columns.Add(dataGridTextColumn);
			dataGridTextColumn = new DataGridTextColumn
			{
				Header = "Согласовано",
				Binding = new Binding("Agree")
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