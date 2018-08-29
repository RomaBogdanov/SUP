using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для OrganizationsWindView.xaml
    /// </summary>
    public partial class OrganizationsWindView
    {
        public static OrganizationsWindView CurrentWindow
        {
            get; set;
        }

        public OrganizationsWindView()
        {
            CurrentWindow = this;
            InitializeComponent();

            // TODO - потом убрать, когда все View на новой модели будут
            //base2.SetViewModel(null);
            base2.tbxSearch.Focus();
            // TODO - потом перенести в генерируемый код
            Base4ViewModel<EnumerationClasses.Organization> viewModel =
            new Base4ViewModel<EnumerationClasses.Organization>
            {
                OkCaption = "OK",
                OkVisibility = Visibility.Hidden,
                ZonesVisibility = Visibility.Hidden,
                Parent = this,
                Model = new OrganizationsListModel<EnumerationClasses.Organization>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base2.DataContext = viewModel;
            AfterInitialize();

            base2.Focus();
        }

        partial void CreateColumns()
        {
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Тип",
                Binding = new Binding("Type"),
				ElementStyle = (Style)App.Current.Resources["OrganizationDataGrid_TextColumn_ElementStyle"]
			};
            base2.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название организации",
                Binding = new Binding("Name"),
				ElementStyle = (Style)App.Current.Resources["OrganizationDataGrid_TextColumn_ElementStyle"]
			};
            base2.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Примечание",
                Binding = new Binding("Comment"),
				ElementStyle = (Style)App.Current.Resources["OrganizationDataGrid_TextColumn_ElementStyle"]
			};
            base2.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Основное название",
                Binding = new Binding("FullName"),
				ElementStyle = (Style)App.Current.Resources["OrganizationDataGrid_TextColumn_ElementStyle"]
			};
            base2.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Страна",
                Binding = new Binding("Country"),
				ElementStyle = (Style)App.Current.Resources["OrganizationDataGrid_TextColumn_ElementStyle"]
			};
            base2.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Регион",
                Binding = new Binding("Region"),
				ElementStyle = (Style)App.Current.Resources["OrganizationDataGrid_TextColumn_ElementStyle"]
			};
            base2.baseTab.Columns.Add(dataGridTextColumn);
            base2.SetDefaultColumn();
        }
        
        private void OrganizationsWindView_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
