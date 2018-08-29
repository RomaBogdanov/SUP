using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Base4OrganizationsWindView.xaml
    /// </summary>
    public partial class Base4OrganizationsWindView
    {
        public Base4OrganizationsWindView()
        {
            InitializeComponent();
            base4.tbxSearch.Focus();
            AfterInitialize();
            base4.Focus();
        }

        private void CreateColumns()
        {

			DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Тип",
                Binding = new Binding("Type"),
				ElementStyle = (Style)App.Current.Resources["OrganizationDataGrid_TextColumn_ElementStyle"]
			};
            base4.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название организации",
                Binding = new Binding("Name"),
				ElementStyle = (Style)App.Current.Resources["OrganizationDataGrid_TextColumn_ElementStyle"]
			};
            base4.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Примечание",
                Binding = new Binding("Comment"),
				ElementStyle = (Style)App.Current.Resources["OrganizationDataGrid_TextColumn_ElementStyle"]
			};
            base4.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Основное название",
                Binding = new Binding("FullName"),
				ElementStyle = (Style)App.Current.Resources["OrganizationDataGrid_TextColumn_ElementStyle"]
			};
            base4.baseTab.Columns.Add(dataGridTextColumn);    
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Страна",
                Binding = new Binding("Country"),
				ElementStyle = (Style)App.Current.Resources["OrganizationDataGrid_TextColumn_ElementStyle"]
			};
            base4.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Регион",
                Binding = new Binding("Region"),
				ElementStyle = (Style)App.Current.Resources["OrganizationDataGrid_TextColumn_ElementStyle"]
			};
            base4.baseTab.Columns.Add(dataGridTextColumn);
        }

        private void SetDefaultColumn()
        {
            base4.baseTab.CurrentColumn = base4.baseTab.Columns[0];
        }
    }
}
