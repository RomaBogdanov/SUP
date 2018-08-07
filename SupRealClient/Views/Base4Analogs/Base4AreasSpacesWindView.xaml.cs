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
    /// Логика взаимодействия для Base4AreasSpacesWindView.xaml
    /// </summary>
    public partial class Base4AreasSpacesWindView
    {
        public Base4AreasSpacesWindView(Visibility okVisibility)
        {
            InitializeComponent();
            base4.tbxSearch.Focus();
            AfterInitialize();

            ((Base4ViewModel<EnumerationClasses.AreaSpace>)base4.DataContext)
                .OkVisibility = okVisibility;
            ((Base4ViewModel<EnumerationClasses.AreaSpace>)base4.DataContext)
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
                Header = "Помещение",
                Binding = new Binding("Space")
            };
            base4.BaseTab.Columns.Add(dataGridTextColumn);
            
        }

        private void SetDefaultColumn()
        {
            base4.baseTab.CurrentColumn = base4.baseTab.Columns[0];
        }
    }
}
