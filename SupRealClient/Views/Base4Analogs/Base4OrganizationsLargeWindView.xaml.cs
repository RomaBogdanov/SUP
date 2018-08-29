using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Base4OrganizationsLargeWindView.xaml
    /// </summary>
    public partial class Base4OrganizationsLargeWindView
    {
        public Base4OrganizationsLargeWindView(Visibility OkVisibility)
        {
            InitializeComponent();
            base4.tbxSearch.Focus();
            AfterInitialize();
            (base4.DataContext as Base4ViewModel<EnumerationClasses.Organization>).OkVisibility = OkVisibility;
            (base4.DataContext as Base4ViewModel<EnumerationClasses.Organization>).ScrollCurrentItem = base4.ScrollIntoViewCurrentItem;
            base4.Focus();
        }

        private void CreateColumns()
        {
			Style style = (Style)App.Current.Resources["OrganizationDataGrid_TextColumn_TextBlock"];
			byte[] data = null;
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
