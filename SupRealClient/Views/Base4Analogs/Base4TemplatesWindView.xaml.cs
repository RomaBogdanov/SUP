using System.Windows.Controls;
using System.Windows.Data;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Base4TemplatesWindView.xaml
    /// </summary>
    public partial class Base4TemplatesWindView
    {
        public Base4TemplatesWindView()
        {
            InitializeComponent();
            base4.tbxSearch.Focus();
            AfterInitialize();

            ((Base4ViewModel<EnumerationClasses.Template>)base4.DataContext)
              .ScrollCurrentItem = base4.ScrollIntoViewCurrentItem;
            base4.Focus();
        }

        private void CreateColumns()
        {
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название",
                Binding = new Binding("Name")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Тип",
                Binding = new Binding("Type")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);
            dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Описание",
                Binding = new Binding("Descript")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);

        }

        private void SetDefaultColumn()
        {
            base4.baseTab.CurrentColumn = base4.baseTab.Columns[0];
        }
    }
}
