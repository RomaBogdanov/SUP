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
        public Base4RegionsWindView(Visibility visibility)
        {
            InitializeComponent();
            base4.tbxSearch.Focus();
            AfterInitialize();

            ((Base4ViewModel<EnumerationClasses.Region>)base4.DataContext)
                .OkVisibility = visibility;
            ((Base4ViewModel<EnumerationClasses.Region>)base4.DataContext)
                .ScrollCurrentItem = base4.ScrollIntoViewCurrentItem;
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

        private void MetroWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Title = @"Регионы";
            if (Visibility == Visibility.Visible)
            {
                if (ParentWindow is AddUpdateOrgsView)
                {
                    Title = @"Выбор региона";
                }
            }
        }
    }
}
