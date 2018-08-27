using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для VisitorsListWindView.xaml
    /// </summary>
    public partial class VisitorsListWindView
    {
		/// <summary>
		/// Автоматический конструктор, сам создает viewmodel и model
		/// </summary>
		/// <param name="okVisibility"></param>
		public VisitorsListWindView(Visibility okVisibility)
		{
			InitializeComponent();
			var viewModel = new Base4ViewModel<EnumerationClasses.Visitor>
			{
				OkCaption = "OK",
				Parent = this,
				Model = new VisitorsListModel<EnumerationClasses.Visitor>()
			};
			viewModel.Model.OnClose += Handling_OnClose;
			base4.DataContext = viewModel;
			AfterInitialize();

			// Пожалуйта, пишите общие настройки viewModel окна списка посетителей в функцию, указанную ниже -
			// это поможет поддерживать оба конструктура одновременно, без необходимости подтягивать один под другой при изменениях
			SetupSettings(base4.DataContext,okVisibility);
		}

		/// <summary>
		/// Стандартный конструктор
		/// </summary>
	    public VisitorsListWindView(object dataContext)
		{
			InitializeComponent();

			if (dataContext is Base4ViewModel<EnumerationClasses.Visitor> base4ViewModel)
			{
				SetupSettings(dataContext, Visibility.Visible);
			}

			base4.DataContext = dataContext;
			AfterInitialize();
		}

		/// <summary>
		/// Общие настройки 
		/// </summary>
		/// <param name="dataContext"></param>
		/// <param name="okVisibility"></param>
	    private void SetupSettings(object dataContext, Visibility okVisibility)
	    {
		    if(dataContext is Base4ViewModel<EnumerationClasses.Visitor> base4ViewModel)
		    {
			    base4ViewModel.OkVisibility = okVisibility;
			    base4ViewModel.Parent = this;
			    base4ViewModel.SynonymsVisibility = Visibility.Visible;
			    base4ViewModel.ScrollCurrentItem = base4ViewModel.ScrollCurrentItem;
		    }
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
