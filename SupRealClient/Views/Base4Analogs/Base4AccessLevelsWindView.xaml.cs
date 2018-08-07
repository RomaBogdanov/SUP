using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Base4AccessLevelsWindView.xaml
    /// </summary>
    public partial class Base4AccessLevelsWindView
    {
        public Base4AccessLevelsWindView(Visibility okVisibility)
        {
            InitializeComponent();
            base4.tbxSearch.Focus();
            AfterInitialize();

            ((Base4ViewModel<EnumerationClasses.AccessLevel>)base4.DataContext)
               .OkVisibility = okVisibility;
            ((Base4ViewModel<EnumerationClasses.AccessLevel>)base4.DataContext)
               .ScrollCurrentItem = base4.ScrollIntoViewCurrentItem;
            base4.Focus();
        }

        private void CreateColumns()
        {
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Область доступа",
                Binding = new Binding("Area")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название",
                Binding = new Binding("Name")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Расписание",
                Binding = new Binding("Schedule")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Описание уровня доступа",
                Binding = new Binding("AccessLevelNote")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);

        }

        private void SetDefaultColumn()
        {
            base4.baseTab.CurrentColumn = base4.baseTab.Columns[0];
        }
    }
}
