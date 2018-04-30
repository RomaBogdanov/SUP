using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для OrganizationsWindView.xaml
    /// </summary>
    public partial class OrganizationsWindView : Window
    {
        public OrganizationsWindView()
        {
            InitializeComponent();

            // TODO - потом убрать, когда все View на новой модели будут
            base2.SetViewModel(null);

            // TODO - потом перенести в генерируемый код
            Base4ViewModel<EnumerationClasses.Organization> viewModel =
            new Base4ViewModel<EnumerationClasses.Organization>
            {
                OkCaption = "OK",
                ZonesVisibility = Visibility.Hidden,
                Parent = this,
                Model = new OrganizationsListModel<EnumerationClasses.Organization>(),
            };
            viewModel.Model.OnClose += Handling_OnClose;
            base2.DataContext = viewModel;
            AfterInitialize();
        }

        partial void CreateColumns()
        {
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Тип",
                Binding = new Binding("Type")
            };
            base2.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название организации",
                Binding = new Binding("Name")
            };
            base2.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Примечание",
                Binding = new Binding("Comment")
            };
            base2.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Основное название",
                Binding = new Binding("FullName")
            };
            base2.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Страна",
                Binding = new Binding("Country")
            };
            base2.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Регион",
                Binding = new Binding("Region")
            };
            base2.baseTab.Columns.Add(dataGridTextColumn);
            base2.SetDefaultColumn();
        }
    }
}
