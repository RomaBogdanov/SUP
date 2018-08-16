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
            ((Base4ViewModel<EnumerationClasses.Visitor>)base4.DataContext)
                .OkVisibility = okVisibility;
            ((Base4ViewModel<EnumerationClasses.Visitor>)base4.DataContext)
                .ScrollCurrentItem = base4.ScrollIntoViewCurrentItem;
        }

		/// <summary>
		/// Стандартный конструктор
		/// </summary>
	    public VisitorsListWindView(object dataContext)
		{
		    InitializeComponent();
			base4.DataContext = dataContext;
			AfterInitialize();
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
