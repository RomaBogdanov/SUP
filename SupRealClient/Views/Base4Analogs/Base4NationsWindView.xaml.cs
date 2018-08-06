using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SupRealClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Base4NationsWindView.xaml
    /// </summary>
    public partial class Base4NationsWindView
    {
        public Base4NationsWindView(Visibility okVisibility)
        {
            InitializeComponent();
            base4.tbxSearch.Focus();
            AfterInitialize();
            ((Base4ViewModel<EnumerationClasses.Nation>)base4.DataContext)
                .OkVisibility = okVisibility;//Visibility.Hidden;
            ((Base4ViewModel<EnumerationClasses.Nation>)base4.DataContext)
               .ScrollCurrentItem = base4.ScrollIntoViewCurrentItem;
            base4.btnRemove.Visibility = Visibility.Collapsed;
            base4.Focus();
        }

        private void CreateColumns()
        {
            DataGridTextColumn dataGridTextColumn = new DataGridTextColumn
            {
                Header = "Название",
                Binding = new Binding("CountryName")
            };
            base4.baseTab.Columns.Add(dataGridTextColumn);
        }

        private void SetDefaultColumn()
        {
            base4.baseTab.CurrentColumn = base4.baseTab.Columns[0];
        }

        private void MetroWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Title = @"Страны";
            if (Visibility == Visibility.Visible)
            {
                if (ParentWindow is AddUpdateOrgsView)
                {
                    Title = @"Выбор страны";
                }
            }           
        }
    }
}
