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
			var viewModel = new Base4ViewModel<EnumerationClasses.Order>
			{
				OkCaption = "OK",
				Parent = this,
				//Model = new OrdersListModel<Order>()<EnumerationClasses.Order>()
			};
			viewModel.Model.OnClose += Handling_OnClose;
			base4.DataContext = viewModel;
			AfterInitialize();
			((Base4ViewModel<EnumerationClasses.Order>) base4.DataContext)
				.OkVisibility = okVisibility;
			((Base4ViewModel<EnumerationClasses.Order>) base4.DataContext)
				.ScrollCurrentItem = base4.ScrollIntoViewCurrentItem;
		}

		partial void CreateColumns()
		{
			DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
			{
				Header = "ФИО",
				Binding = new Binding("FullName")
			};
			base4.baseTab.Columns.Add(dataGridTextColumn);
			dataGridTextColumn = new DataGridTextColumn
			{
				Header = "Организация",
				Binding = new Binding("Organization")
			};
			base4.baseTab.Columns.Add(dataGridTextColumn);
			dataGridTextColumn = new DataGridTextColumn
			{
				Header = "Примечание",
				Binding = new Binding("Comment")
			};
			base4.baseTab.Columns.Add(dataGridTextColumn);
		}
	}
}