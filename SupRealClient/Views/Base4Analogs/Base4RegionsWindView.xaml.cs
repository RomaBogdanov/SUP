using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Base4NationsWindView.xaml
    /// </summary>
    public partial class Base4RegionsWindView
    {
        public Base4RegionsWindView()
        {
            InitializeComponent();
            base4.tbxSearch.Focus();
            AfterInitialize();
            base4.Focus();
        }

        public void SetCountry(int countryId)
        {
            ((base4.DataContext as Base4ViewModel<EnumerationClasses.Region>).Model
                as RegionsListModel<EnumerationClasses.Region>).SetCountry(countryId);
        }

        private void CreateColumns()
        {
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название",
                Binding = new Binding("Name")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Страна",
                Binding = new Binding("Country")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
        }

        private void SetDefaultColumn()
        {
            base4.baseTab.CurrentColumn = base4.baseTab.Columns[0];
        }
    }
}
